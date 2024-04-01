using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Lib.Services;

namespace UtilityBot.Lib.Controllers
{

    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IOperation _operation;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IOperation operation, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _operation = operation;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                //Главное меню
                case "/start":
                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Подсчет символов" , $"cnt"),
                        InlineKeyboardButton.WithCallbackData($" Расчет суммы" , $"sum")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>Наш бот делает 2 операции {Environment.NewLine}1.Подсчитывает символы в тексте</b> {Environment.NewLine}" +
                        " Например: папар - введено 5 символов" + $"{Environment.NewLine} <b>2. Подсчитывает сумму введенных чисел через пробел</b> {Environment.NewLine}" +
                        " Например: 1 2 3 - сумма чисел равна 6", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    string userOperationCode = _memoryStorage.GetSession(message.Chat.Id).OperationCode;
                    if (userOperationCode == "sum")
                        //подсчет суммы чисел
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, _operation.Sum(message.Text), cancellationToken: ct);
                    else if (userOperationCode == "cnt")
                        //подсчет символов в сообщении
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, _operation.Cnt(message.Text), cancellationToken: ct);
                    break;

            }
			
        }
		
    }
	
}
