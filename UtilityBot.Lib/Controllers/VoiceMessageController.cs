using Telegram.Bot;
using Telegram.Bot.Types;

namespace UtilityBot.Lib.Controllers
{

    public class VoiceMessageController
    {        
        private readonly ITelegramBotClient _telegramClient;

        public VoiceMessageController(ITelegramBotClient telegramBotClient)
        {            
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправьте текст для подсчета количества символов сообщения или суммы чисел", cancellationToken: ct);
        }
		
    }
	
}