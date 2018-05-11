using System.Collections.Generic;

namespace Molfar.Core.Models
{
    public interface IMolfarAnswer
    {
        IEnumerable<string> GetAnswer();
    }
}