using System.Collections.Generic;

namespace Molfar.Core.Models
{
    public class MolfarMultirowAnswer : IMolfarAnswer
    {
        private List<string> _rows;
        public MolfarMultirowAnswer()
        {
            _rows = new List<string>();
        }

        public IEnumerable<string> GetAnswer()
        {
            return new List<string>(_rows);
        }

        public void AddRow(string row)
        {
            _rows.Add(row);
        }
    }
}
