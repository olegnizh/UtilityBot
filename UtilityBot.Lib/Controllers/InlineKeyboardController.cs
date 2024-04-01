using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Lib.Services;

namespace UtilityBot.Lib.Controllers
{

    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).OperationCode = callbackQuery.Data;

            // Генерим информационное сообщение
            string OperationCode = callbackQuery.Data switch
            {
                "cnt" => " Подсчет символов",
                "sum" => " Расчет суммы",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,$"<b> Операция - {OperationCode}.{Environment.NewLine}</b>", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}