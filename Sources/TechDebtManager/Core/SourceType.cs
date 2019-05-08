using System;
using System.IO;

namespace Core
{
    public enum SourceType
    {
        Unknown,
        CSharp,
        Txt
    }

    public static class SourceTypeExtensions
    {
        public static SourceType GetSourceType(this string filePath)
        {
            var ext = Path.GetExtension(filePath);
            if (String.IsNullOrEmpty(ext))
            {
                return SourceType.Unknown;
            }

            switch (ext.ToLower())
            {
                case ".cs":  return SourceType.CSharp;
                case ".txt": return SourceType.Txt;
                default:     return SourceType.Unknown;
            }
        }

        public static string GetExtension(this SourceType initial)
        {
            switch (initial)
            {
                case SourceType.Unknown: return String.Empty;
                case SourceType.CSharp:  return ".cs";
                case SourceType.Txt:     return ".txt";
                default:                 throw new ArgumentOutOfRangeException(nameof(initial), initial, null);
            }
        }
    }
}
