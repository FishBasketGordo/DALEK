using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Creelio.Framework.Extensions;
using System.IO;
using System.Text.RegularExpressions;
using Creelio.Framework.DAL;

namespace DALEK.Generator
{
    public class DalekGen
    {
        #region Properties

        public List<string> GenerateOnce { get; set; }
        public List<string> GenerateDeferred { get; set; }

        #endregion

        #region Methods

        #region Generate

        public void Generate(string connectionString, string scriptsOutputDirectory, string entitiesOutputDirectory)
        {
            GenerateOnce = new List<string>();
            GenerateDeferred = new List<string>();

            DataTable tables, columns, primaryKeys;
            GetData(connectionString, out tables, out columns, out primaryKeys);

            var outputDirectories = new Dictionary<string, string>();
            outputDirectories.Add(".sql", scriptsOutputDirectory);
            outputDirectories.Add(".cs", entitiesOutputDirectory);

            foreach (DataRow row in tables.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                bool readOnly = tableName.EndsWith("History");

                if (tableName == "sysdiagrams")
                    continue;

                GenerateFiles(connectionString, tableName, readOnly, tables, columns, primaryKeys, outputDirectories, false);
            }

            GenerateFiles(connectionString, string.Empty, false, tables, columns, primaryKeys, outputDirectories, true);
        }

        private void GenerateFiles(
            string connectionString, 
            string tableName, 
            bool readOnly, 
            DataTable tables, 
            DataTable columns, 
            DataTable primaryKeys, 
            Dictionary<string, string> outputDirectories, 
            bool generateDeferred)
        {
            InterpolationArgs args = new InterpolationArgs 
            {
                TableName = tableName,
                ConnectionString = connectionString,
                AllTables = tables.Rows.ToList<DataRow>(),
                Columns = FilterByTableName(columns, tableName),
                PrimaryKeys = FilterByTableName(primaryKeys, tableName),
            };

            string[] templatePaths = generateDeferred
                                   ? GenerateDeferred.ToArray()
                                   : Directory.GetFiles("Templates", "*.tmpl", SearchOption.AllDirectories);

            foreach (string templatePath in templatePaths)
            {
                Interpolator interpolator = Interpolator.GetInterpolatorForTemplate(templatePath);

                // For read-only tables (e.g. history tables), only generate a 
                // SELECT stored procedure and the entity .cs file.
                if (readOnly && 
                    string.Compare(interpolator.TemplateName, "SELECT", true) != 0 &&
                    string.Compare(interpolator.TemplateName, "COUNT", true) != 0 &&
                    string.Compare(interpolator.TemplateName, "Entity", true) != 0)
                {
                    continue;
                }

                if (!outputDirectories.ContainsKey(interpolator.TemplateExtension) ||
                    string.IsNullOrWhiteSpace(outputDirectories[interpolator.TemplateExtension]))
                {
                    continue;
                }

                args.TemplateName = interpolator.TemplateName;
                args.OutputFilePath= GetOutputFilePath(args, outputDirectories, interpolator);

                GenerateFile(args, templatePath, interpolator, generateDeferred);
            }
        }

        private static string GetOutputFilePath(InterpolationArgs args, Dictionary<string, string> outputDirectories, Interpolator interpolator)
        {
            string outputDirectory = outputDirectories[interpolator.TemplateExtension];
            string fileName = interpolator.GetOutputFileName(args);
            string outputFilePath = Path.Combine(outputDirectory, fileName);

            if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
                outputFilePath = Path.ChangeExtension(outputFilePath, interpolator.TemplateExtension);

            return outputFilePath;
        }

        private void GenerateFile(InterpolationArgs args, string templatePath, Interpolator interpolator, bool generateDeferred)
        {
            string template = File.ReadAllText(templatePath);

            if (ShouldGenerateFile(args, ref template, templatePath, generateDeferred))
            {
                while (true)
                {
                    string placeHolder, replacement;
                    if (!interpolator.TryGetNextReplacement(template, args, out placeHolder, out replacement))
                        break;

                    template = template.Replace(placeHolder, replacement);
                }

                BackupExistingFile(args.OutputFilePath);
                File.WriteAllText(args.OutputFilePath, template);
            }
        }

        private bool ShouldGenerateFile(InterpolationArgs args, ref string template, string templatePath, bool generateDeferred)
        {
            string firstLine = template.Split(new string[] { Environment.NewLine }, 2, StringSplitOptions.None)[0];

            if (firstLine.StartsWith("#"))
            {
                if (firstLine.Contains("ONCE") && !GenerateOnce.Contains(templatePath))
                {
                    BackupExistingFile(args.OutputFilePath);
                    GenerateOnce.Add(templatePath);                    
                }

                if (firstLine.Contains("ONCE") && File.Exists(args.OutputFilePath))
                    return false;

                if (firstLine.Contains("DEFER") && !GenerateDeferred.Contains(templatePath))
                {
                    GenerateDeferred.Add(templatePath);
                }

                if (firstLine.Contains("DEFER") && !generateDeferred)
                    return false;

                template = template.Remove(0, firstLine.Length).TrimStart(Environment.NewLine.ToCharArray());
            }

            return true;
        }

        #endregion

        #region Helpers

        private static void GetData(string connectionString, out DataTable tables, out DataTable columns, out DataTable primaryKeys)
        {
            using (var info = new InformationSchema(connectionString))
            {
                tables = info.Tables;
                columns = info.Columns;
                primaryKeys = info.PrimaryKeys;
            }
        }

        private static List<DataRow> FilterByTableName(DataTable dt, string tableName)
        {
            var rows = (from r in dt.Rows.ToList<DataRow>()
                        where r["TABLE_NAME"].ToString() == tableName
                        orderby r["ORDINAL_POSITION"] ascending
                        select r).ToList();

            return rows;
        }

        private static void BackupExistingFile(string path)
        {
            if (File.Exists(path))
            {
                string prevGenDir = Path.Combine(Path.GetDirectoryName(path), "Previous Generations");
                string file = Path.GetFileNameWithoutExtension(path);
                string ext = Path.GetExtension(path);
                int ii = 1;

                if (!Directory.Exists(prevGenDir))
                {
                    Directory.CreateDirectory(prevGenDir);
                }

                string prevGenPath = Path.Combine(prevGenDir, string.Format("{0}({1}){2}", file, ii.ToString("00"), ext));
                while (File.Exists(prevGenPath))
                {
                    ii += 1;
                    prevGenPath = Path.Combine(prevGenDir, string.Format("{0}({1}){2}", file, ii, ext));
                }

                File.Move(path, prevGenPath);
            }
        }

        #endregion

        #endregion
    }
}
