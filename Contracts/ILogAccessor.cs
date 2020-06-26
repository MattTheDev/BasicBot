using Discord;
using System.Threading.Tasks;

namespace BasicBot.Contracts
{
    public interface ILogAccessor
    {
        /// <summary>
        /// Create a new logging entry in our logging database.
        /// </summary>
        /// <param name="logMessage">Discord LogMessage Object</param>
        /// <returns>Task</returns>
        Task Create(LogMessage logMessage);
    }    
}