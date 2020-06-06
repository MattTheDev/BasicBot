using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BasicBot.Services
{
    // This service handles all input from all sources and determines if a command should be executed,
    // and executes if needed.
    public class CommandHandlerService 
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commandService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerService(
            DiscordSocketClient discord,
            CommandService commandService,
            IConfiguration configuration,
            IServiceProvider serviceProvider
        )
        {
            _discord = discord;
            _commandService = commandService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;

            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            
            if(msg == null || 
               msg.Author.IsBot || 
               msg.Author.IsWebhook)
            {
                return;
            }

            var context = new SocketCommandContext(_discord, msg);
            var argPosition = 0;
            if(msg.HasStringPrefix(_configuration["prefix"], ref argPosition) || 
                    msg.HasMentionPrefix(_discord.CurrentUser, ref argPosition))
                    {
                        var result = await _commandService.ExecuteAsync(context, argPosition, _serviceProvider);

                        if(!result.IsSuccess)
                        {
                            await context.Channel.SendMessageAsync($"Sorry, {context.User.Username} something " +
                                $"went wrong -> {result.ToString()}");
                        }
                    }
        }
    }
}