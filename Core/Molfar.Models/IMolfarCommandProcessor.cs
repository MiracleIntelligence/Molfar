using Molfar.Core.Models;
using System.Threading.Tasks;

namespace Molfar.Core
{
    public abstract class MolfarCommandProcessor
    {
        public abstract bool CanExcecute(string message);
        public abstract Task<MolfarAnswer> ExcecuteCommand(string message);        
       
    }
}
