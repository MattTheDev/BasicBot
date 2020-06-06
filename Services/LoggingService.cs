using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BasicBot.Services 
{
    public class LoggingService {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commandService;

        public LoggingService(
            DiscordSocketClient discord, 
            CommandService commandService)
        {
            _discord = discord;
            _commandService = commandService;
        }

        private async Task OnLogAsync(LogMessage message)
        {
            var logText = $"[{DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss")}] {message}";
            
            await Console.Out.WriteLineAsync(logText);
        }
    }
}