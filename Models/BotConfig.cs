namespace BasicBot.Models
{
    public class BotConfig
    {
        public Tokens Tokens {get;set;}
        public string Prefix {get;set;}
        public ConnectionStrings ConnectionStrings{get;set;}
    }

    public class Tokens
    {
        public string Discord {get;set;}
    }

    public class ConnectionStrings {
        public string BasicBotLogging {get;set;}
    }
}