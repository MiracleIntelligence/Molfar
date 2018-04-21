namespace Molfar.Core.Models
{
    public class MolfarAnswer
    {
        public MolfarAnswer(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
