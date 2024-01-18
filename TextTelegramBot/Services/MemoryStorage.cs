using System.Collections.Concurrent;
using TelegramBotTask.Services;
using TelegramBotTask.Models;

namespace TelegramBotTask.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;


        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            // Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { LanguageCode = "LehgtMessage" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;

        }
    }
}
