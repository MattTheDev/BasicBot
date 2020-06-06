using System.Threading.Tasks;
using Discord.Commands;

namespace BasicBot.Modules 
{
    public class UtilityCommands : ModuleBase
    {
        [Command("ping", RunMode = RunMode.Async)]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
        }
    }
}