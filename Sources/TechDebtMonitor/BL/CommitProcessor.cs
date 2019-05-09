using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using BL.Tags;

using Core;

using LibGit2Sharp;

namespace BL
{
    public static class CommitProcessor
    {
        public static List<TechDebt> ProcessCommit(Commit commit,
                                                   string tag)
        {
            var commitDate = commit.Committer.When.DateTime;
            return ProcessTreeEntries(commit.Tree, commitDate, tag);
        }

        private static List<TechDebt> ProcessTreeEntries(IEnumerable<TreeEntry> treeEntries,
                                                         DateTime               commitDate,
                                                         string                 tag)
        {
            var result = new List<TechDebt>();
            foreach (var treeEntry in treeEntries)
            {
                switch (treeEntry.TargetType)
                {
                    case TreeEntryTargetType.GitLink:

                        // Todo: lets findout what to do with that! somewhen...
                        break;
                    case TreeEntryTargetType.Tree:
                        var tree = (Tree) treeEntry.Target;
                        result.AddRange(ProcessTreeEntries(tree, commitDate, tag));
                        break;
                    case TreeEntryTargetType.Blob:
                        var tagFinder = TagFinderFactory.GetFinder(treeEntry);
                        if (tagFinder == null)
                        {
                            continue;
                        }

                        var blob          = (Blob) treeEntry.Target;
                        var contentStream = blob.GetContentStream();
                        using (var reader = new StreamReader(contentStream, Encoding.UTF8))
                        {
                            var content = reader.ReadToEnd();
                            result.AddRange(tagFinder.Find(content, tag).Select(ti => new TechDebt
                                                                                      {
                                                                                          StartDate = commitDate,
                                                                                          Tag       = tag,
                                                                                          Text      = ti.Text,
                                                                                          File      = treeEntry.Path
                                                                                      }));
                        }

                        break;
                }
            }

            return result;
        }
    }
}
