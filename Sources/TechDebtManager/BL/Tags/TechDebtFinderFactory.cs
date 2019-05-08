using System;

using Core;

using JetBrains.Annotations;

using LibGit2Sharp;

namespace BL.Tags
{
    public static class TagFinderFactory
    {
        /// <summary>
        ///     'null' means 'there is no TagFinder for you, I'm very sorry'
        /// </summary>
        [CanBeNull]
        public static ITagFinder GetFinder(TreeEntry treeEntry)
        {
            if (treeEntry.TargetType != TreeEntryTargetType.Blob)
            {
                throw new Exception("That's not a file, deary"); //Todo: meh, Exception type is so smelly
            }

            var sourceType = treeEntry.Path.GetSourceType();
            switch (sourceType)
            {
                case SourceType.CSharp:  return new CSharpTagFinder();
                case SourceType.Unknown: return null;
                case SourceType.Txt:     return null; //Todo: make some tea and TxtTagFinder
                default:                 throw new ArgumentOutOfRangeException(nameof(sourceType), $"You forgot handle some value of {nameof(SourceType)}, silly");
            }
        }
    }
}
