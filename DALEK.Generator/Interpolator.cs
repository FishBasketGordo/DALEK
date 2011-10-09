using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DALEK.Generator
{
    internal abstract class Interpolator
    {
        #region Fields

        private Dictionary<string, Func<InterpolationArgs, string>> _placeHolderDictionary = null;

        #endregion

        #region Properties

        public string TemplateName { get; private set; }
        public string TemplateExtension { get; private set; }

        protected Dictionary<string, Func<InterpolationArgs, string>> PlaceHolderDictionary
        {
            get
            {
                if (_placeHolderDictionary == null)
                {
                    _placeHolderDictionary = new Dictionary<string, Func<InterpolationArgs, string>>();
                    PopulatePlaceHolderDictionary(_placeHolderDictionary);
                }

                return _placeHolderDictionary;
            }
        }
        
        #endregion

        #region Methods

        public static Interpolator GetInterpolatorForTemplate(string templatePath)
        {
            string ext = GetExtension(templatePath);

            if (string.Compare(ext, ".sql", true) == 0)
            {
                return new SqlInterpolator 
                { 
                    TemplateName = Path.GetFileNameWithoutExtension(
                        Path.GetFileNameWithoutExtension(templatePath)
                    ),
                    TemplateExtension = ext
                };
            }
            else if (string.Compare(ext, ".cs", true) == 0)
            {
                return new CsInterpolator
                {
                    TemplateName = Path.GetFileNameWithoutExtension(
                        Path.GetFileNameWithoutExtension(templatePath)
                    ),
                    TemplateExtension = ext
                };
            }
            else
            {
                throw new ArgumentException(string.Format("No interpolator registered for template type \"{0}.tmpl\".", ext));
            }
        }

        public bool TryGetNextReplacement(string template, InterpolationArgs args, out string placeHolder, out string replacement)
        {
            Match match = Regex.Match(template, @"(.*?)\{\{([^}]+)\}\}");

            if (!match.Success)
            {
                placeHolder = string.Empty;
                replacement = string.Empty;
                return false;
            }
            else
            {
                string placeHolderName = match.Groups[2].Value;

                args.IndentSpaces = match.Groups[1].Value.Length;

                placeHolder = string.Format("{{{{{0}}}}}", placeHolderName);

                replacement = PlaceHolderDictionary.ContainsKey(placeHolderName)
                            ? PlaceHolderDictionary[placeHolderName](args)
                            : string.Format("??{0}??", placeHolderName);

                return true;
            }
        }

        protected static string GetList(InterpolationArgs args, Func<InterpolationArgs, string> getItem)
        {
            var sb = new StringBuilder();
            
            for (int ii = 0; ii < args.Columns.Count; ii++)
            {
                args.ColumnIndex = ii;
                string item = getItem(args);

                if (!string.IsNullOrEmpty(item))
                {
                    sb.Append(getItem(args));
                }
            }

            return sb.ToString();
        }

        private static string GetExtension(string templatePath)
        {
            if (string.IsNullOrWhiteSpace(templatePath))
            {
                throw new ArgumentNullException("templatePath");
            }

            string ext = Path.GetExtension(templatePath);
            if (string.Compare(ext, ".tmpl", 0) != 0)
            {
                throw new ArgumentException("Given path must be to a .tmpl file.");
            }

            ext = Path.GetExtension(Path.GetFileNameWithoutExtension(templatePath)).ToLower();

            return ext;
        }

        #region Abstract

        public abstract string GetOutputFileName(InterpolationArgs args);

        protected abstract void PopulatePlaceHolderDictionary(Dictionary<string, Func<InterpolationArgs, string>> dictionary);

        #endregion

        #endregion
    }
}
