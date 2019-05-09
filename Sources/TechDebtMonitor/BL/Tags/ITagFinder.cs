using System.Collections.Generic;

namespace BL.Tags
{
    public interface ITagFinder
    {
        IEnumerable<TagInfo> Find(string content,
                                  string tag);
    }
}
