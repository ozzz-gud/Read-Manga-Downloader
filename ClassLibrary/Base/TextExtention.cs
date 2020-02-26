using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassLibrary.Base
{
    static class TextExtention
    {
        private static readonly char[] charsToDelete;
        static TextExtention()
        {
            charsToDelete = Path.GetInvalidFileNameChars();
            charsToDelete = charsToDelete.Append('.').ToArray();
        }
        public static string GetRightFolderName(this string name)
        {
            foreach (var c in charsToDelete)
            {
                name = name.Replace(c, ' ');
            }
            name = Regex.Replace(name, @"\s{2,}", string.Empty);
            name = name.Trim();
            return name;
        }
    }
}
