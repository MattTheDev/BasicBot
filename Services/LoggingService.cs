using System;
using System.Threading.Tasks;
using BasicBot.Contracts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BasicBot.Services 
{
    public class LoggingService 
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commandService;
        private readonly ILogAccessor _logAccessor;

        public LoggingService(
            DiscordSocketClient discord, 
            CommandService commandService,
            ILogAccessor logAccessor)
        {
            _discord = discord;
            _commandService = commandService;
            _logAccessor = logAccessor;

            _discord.Log += OnLogAsync;
            _commandService.Log += OnLogAsync;
        }

        private async Task OnLogAsync(LogMessage message)
        {
            var logText = $"[{DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss")}] {message}";
            
            await _logAccessor.Create(message);
            await Console.Out.WriteLineAsync(logText);
        }
    }
}