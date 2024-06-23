namespace Molfar.Models
{
    public class MolfarCommand : IMolfarCommand
    {
        public MolfarCommand(string message)
        {
            Message = message;
        }
        public string Message { get; protected set; }
    }
}
