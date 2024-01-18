using TelegramBotTask.Models;

namespace TelegramBotTask.Services
{
    public interface IStorage
    {
       
        Session GetSession(long chatId);
    }
}