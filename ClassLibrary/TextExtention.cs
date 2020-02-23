using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class TextExtention
    {
        private static Regex punctuationMarks = new Regex(@"[^ \w'-]|_|\B'");
        private static Regex wordsReg = new Regex(@"\w+(?:'|’)?\w*");
        public static string TextWithoutPunctuationMarks(this string text)
        {
            return punctuationMarks.Replace(text, string.Empty);
        }
        public static string[] GetWords(this string text)
        {
            var matches = wordsReg.Matches(text);
            string[] words = new string[matches.Count];
            return matches
                .OfType<Match>()
                .Select(m => m.Value)
                .ToArray();
        }
        static TextExtention()
        {
            var chars1 = Path.GetInvalidFileNameChars();
            var chars2 = Path.GetInvalidPathChars();
            chars = chars1.Concat(chars2).ToArray();
        }
        private static char[] chars;
        public static string GetRightName(this string name)
        {
            foreach (var c in chars)
            {
                name = name.Replace(c, ' ');
            }
            name = Regex.Replace(name, @"\s{2,}", " ");
            return name;
        }
    }
}
