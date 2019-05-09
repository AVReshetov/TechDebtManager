using System;
using System.Collections.Generic;

namespace Core
{
    public class TechDebt
    {
        public DateTime StartDate { get; set; }
        public string   Tag       { get; set; }
        public string   File      { get; set; }
        public string   Text      { get; set; }
    }

    public class TechDebtEqualityComparer : IEqualityComparer<TechDebt>
    {
        /// <inheritdoc />
        public bool Equals(TechDebt x,
                           TechDebt y)
        {
            return x.Text == y.Text;
        }

        /// <inheritdoc />
        public int GetHashCode(TechDebt obj)
        {
            return obj.Text.GetHashCode();
        }
    }
}
