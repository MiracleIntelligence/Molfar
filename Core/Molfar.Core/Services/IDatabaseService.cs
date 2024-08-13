using SQLite;

namespace Molfar.Core.Services
{
    public interface IDatabaseService
    {
        SQLiteConnection Connection { get; }
    }
}