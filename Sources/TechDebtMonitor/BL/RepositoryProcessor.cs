using System.Collections.Generic;
using System.Linq;

using Core;

using LibGit2Sharp;

namespace BL
{
    public class RepositoryProcessor
    {
        private readonly string _path;
        private readonly string _tag;

        public RepositoryProcessor(string path,
                                   string tag)
        {
            _path = path;
            _tag  = tag;
        }

        public IEnumerable<TechDebt> GetTechDebts()
        {
            var techDebtList = new List<TechDebt>();
            var actualDebts  = new List<TechDebt>();
            using (var repository = new Repository(_path))
            {
                var headCommit = repository.Commits.First();
                actualDebts.AddRange(CommitProcessor.ProcessCommit(headCommit, _tag));
                techDebtList.AddRange(repository.Commits.SelectMany(commit => CommitProcessor.ProcessCommit(commit, _tag)));
            }

            return techDebtList.Where(td => actualDebts.Contains(td, new TechDebtEqualityComparer()))                             // get only actual
                               .GroupBy(td => td.Text).Select(gr => gr.First(td1 => td1.StartDate == gr.Min(td => td.StartDate))) // get earliest exemplar
                               .OrderByDescending(td => td.StartDate);
        }
    }
}
