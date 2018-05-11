using System.Collections;
using System.Collections.Generic;

namespace Molfar.Core.Models
{
    public class MolfarAnswer : IMolfarAnswer
    {
        public MolfarAnswer(string message)
        {
            Message = message;
        }

        public virtual IEnumerable<string> GetAnswer()
        {
            yield return Message;
        }

        public string Message { get; set; }
    }
}
