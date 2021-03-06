﻿using System;
using System.Threading.Tasks;
using BasicBot.Contracts;
using BasicBot.Implementations;
using BasicBot.Models;
using BasicBot.Services;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasicBot
{
    class Program
    {
        private IConfigurationRoot _config;
        private DiscordSocketClient _discord;

        private static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("BotConfig.json");
            _config = builder.Build();

            var serviceProvider = ConfigureServices();
            serviceProvider.GetRequiredService<LoggingService>();
            await serviceProvider.GetRequiredService<StartupService>().StartAsync();
            _discord = serviceProvider.GetRequiredService<DiscordSocketClient>();

            while (_discord.CurrentUser == null)
            {
                Console.WriteLine("Checking if service is started / completely logged in.");
                System.Threading.Thread.Sleep(5000);
            }

            serviceProvider.GetRequiredService<CommandService>();
            serviceProvider.GetRequiredService<CommandHandlerService>();

            await Task.Delay(-1);
        } 

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
            .AddSingleton(new DiscordSocketClient(
                new DiscordSocketConfig{
                    LogLevel = Discord.LogSeverity.Verbose
                }
            ))
            .AddSingleton<CommandHandlerService>()
            .AddSingleton<StartupService>()
            .AddSingleton<LoggingService>()
            .AddSingleton<CommandService>()
            .AddScoped<ILogAccessor, LogAccessor>();

            services.AddOptions();
            services.Configure<BotConfig>(_config);

            return services.BuildServiceProvider();
        }
    }
}