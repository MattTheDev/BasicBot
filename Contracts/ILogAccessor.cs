using System.Threading.Tasks;
using Discord;

namespace BasicBot.Contracts
{
    public interface ILogAccessor
    {
        Task Create(LogMessage logMessage);
    }    
}