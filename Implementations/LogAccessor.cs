using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using BasicBot.Contracts;
using BasicBot.Models;
using Dapper;
using Discord;
using Microsoft.Extensions.Options;

namespace BasicBot.Implementations 
{
    public class LogAccessor : ILogAccessor
    {
        private readonly BotConfig _botSettings;

        public LogAccessor(IOptions<BotConfig> botSettings)
        {
            _botSettings = botSettings.Value;
        }

        public async Task Create(LogMessage logMessage)
        {
            await using var connection = new SQLiteConnection($"DataSource={_botSettings.ConnectionStrings.BasicBotLogging}");

            var sql = "INSERT INTO DiscordLogs (Severity, Source, Message, Exception, CreatedDate) VALUES " +
                        "(@Severity, @Source, @Message, @Exception, @CreatedDate)";

            await connection.ExecuteAsync(sql, new {
                Severity = logMessage.Severity.ToString(),
                logMessage.Source,
                logMessage.Message,
                Exception = logMessage.Exception?.Message,
                CreatedDate = DateTime.UtcNow
            });
        }
    }
}