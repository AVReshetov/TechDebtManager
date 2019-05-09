using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BL.Tags
{
    public sealed class CSharpTagFinder : ITagFinder
    {
        private static readonly string _regexTemplate = @"(?<=\W|^)(?<TAG>{0})(\W|$)(.*)";

        /// <inheritdoc />
        public IEnumerable<TagInfo> Find(string content,
                                         string tag)
        {
            var regex = new Regex(String.Format(_regexTemplate, tag));
            foreach (Match match in regex.Matches(content))
            {
                yield return new TagInfo(match.Value);
            }
        }
    }
}
