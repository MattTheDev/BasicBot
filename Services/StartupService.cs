using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace BasicBot.Services
{
    public class StartupService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commandService;
        private readonly IConfigurationRoot _configuration;

        public StartupService(
            IServiceProvider provider,
            DiscordSocketClient discord,
            CommandService commandService,
            IConfigurationRoot configuration)
        {
            _provider = provider;
            _discord = discord;
            _commandService = commandService;
            _configuration = configuration;
        }
        
        public async Task StartAsync()
        {
            var discordToken = _configuration["tokens:discord"];

            if(string.IsNullOrEmpty(discordToken))
            {
                throw new NullReferenceException("There was no Discord token provided in the BotConfig.json.");
            }

            await _discord.LoginAsync(TokenType.Bot, discordToken);
            await _discord.StartAsync();

            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}