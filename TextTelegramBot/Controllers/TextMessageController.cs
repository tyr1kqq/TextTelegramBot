using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using TelegramBotTask.Configuration;
using System.Diagnostics;
using TelegramBotTask.Services;

namespace TelegramBotTask.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Длина сообщения" , $"LehgtMessage"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел " , $"SummMssage")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Бот считает сумму числел , либо возвращает длину сообщения .</b> {Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));


                    break;
                default:
                    
                    Process(_memoryStorage.GetSession(message.Chat.Id).LanguageCode, message, ct);
                    break;


            }

        }
        public async Task Process(string languageCode, Message message, CancellationToken ct)
        {
            if (languageCode == "LehgtMessage")
            {
                int Leght = message.Text.Length;
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Длина сообщения - {Leght}");
            }
            if (languageCode == "SummMssage")
            {
                string messageText = message.Text;
                string[] Value = messageText.Split(' ');
                int SummValue = 0;

                foreach (string Val in Value)
                {
                    SummValue += int.Parse(Val);
                }

                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел - {SummValue}");
            }

        }
    }
}
