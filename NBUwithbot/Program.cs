#region birinshi sposob
//class Program
//{
//    static void Main(string[] args)
//    {
//        var client = new TelegramBotClient("6947589393:AAE8_wNhTDj6TWACItwqHeBAtPM5cShl12Y");
//        client.StartReceiving(Update, Error);
//        Console.ReadLine();
//    }

//    private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
//    {
//        throw new NotImplementedException();
//    }


//    async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
//    {
//        HttpClient httpclient = new HttpClient();
//        var rquest = new HttpRequestMessage(HttpMethod.Get, "https://nbu.uz/exchange-rates/json/");
//        var response = await httpclient.SendAsync(rquest);

//        var body = await response.Content.ReadAsStringAsync();

//        var courses = JsonConvert.DeserializeObject<List<Kurs>>(body);
//        var message = update.Message;
//        if (message != null) 
//        {
//            if (message.Text == "/start") 
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Kursni kiriting misol: Usd");
//            }
//            else 
//            {
//                bool chek = false;
//                string kurs = message.Text.ToString();
//                foreach (var item in courses) 
//                {
//                    if (kurs.ToUpper() == item.code) 
//                    {
//                        await botClient.SendTextMessageAsync(message.Chat.Id, $"one {kurs} {item.cb_price} in sum");
//                        chek = true;
//                        break;
//                    }
//                }
//                if (chek == false) 
//                {
//                    await botClient.SendTextMessageAsync(message.Chat.Id, $"{kurs} a course does not exist");
//                }
//            }
//        }

//    }
//}
#endregion

#region ikkinchi sposob
using NBUwithbot;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("6947589393:AAE8_wNhTDj6TWACItwqHeBAtPM5cShl12Y");

using CancellationTokenSource cts = new();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message

    var handler = update.Type switch
    {
        UpdateType.Message => HandleMessageAsync(botClient, update, cancellationToken),
        UpdateType.CallbackQuery => HandleCallBackQueryAsync(botClient, update, cancellationToken),
        UpdateType.EditedMessage => HandleEditedMessageAsync(botClient, update, cancellationToken),
        //UpdateType.CallbackQuery =>HandleMessageAsync(botClient, update, cancellationToken),
        _ => HandleUnknownUpdateType(botClient, update, cancellationToken),
    };
}
async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    var message = update.Message;
    var user = message.Chat.FirstName;
    var handler = message.Type switch
    {
        MessageType.Text => HandleTextMessageAsync(botClient, update, cancellationToken, user),

        _ => HandleUnknownMessageTypeAsync(update, update, cancellationToken),
    };
}


async Task HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string user)
{
    var chatName = update.Message.Chat.FirstName;
    var messageText = update.Message.Text;
    Console.WriteLine($"Received a '{messageText}' message in chat {chatName}.");

    // Echo received message text

    HttpClient lets = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Get, "https://nbu.uz/exchange-rates/json/");
    var response = await lets.SendAsync(request);

    var body = await response.Content.ReadAsStringAsync();

    var courses = JsonConvert.DeserializeObject<List<Kurs>>(body);
    var buttons = new List<List<InlineKeyboardButton>>();
    var button = new List<InlineKeyboardButton>();
    var button1 = new List<InlineKeyboardButton>();
    var button2 = new List<InlineKeyboardButton>();

    byte count = 1;

    foreach (Kurs item in courses)
    {
        if (item.code == "USD" || item.code == "EUR")
        {
            if (item.code == "USD")
            {
                button.Add(InlineKeyboardButton.WithCallbackData("Dollar", $"{item.cb_price}"));
            }
            else 
            {
                button.Add(InlineKeyboardButton.WithCallbackData("Euro", $"{item.cb_price}"));
            }
        }
        else if (item.code == "RUB" || item.code == "AED")
        {
            if (item.code == "RUB")
            {
                button1.Add(InlineKeyboardButton.WithCallbackData("Rubl", $"{item.cb_price}"));
            }
            else
            {
                button1.Add(InlineKeyboardButton.WithCallbackData("dirham", $"{item.cb_price}"));
            }
        }
        else if (item.code == "CNY" || item.code == "GBP")
        {
            if (item.code == "CNY")
            {
                button2.Add(InlineKeyboardButton.WithCallbackData("yuan", $"{item.cb_price}"));
            }
            else
            {
                button2.Add(InlineKeyboardButton.WithCallbackData("pound sterling", $"{item.cb_price}"));
            }
        }

    }
    buttons.Add(button);
    buttons.Add(button1);
    buttons.Add(button2);


    Message sentMessage2 = await botClient.SendTextMessageAsync(
        chatId: update.Message.Chat.Id,
        text: "Valyuta nomini tanlang!",
        replyMarkup: new InlineKeyboardMarkup(buttons),
        cancellationToken: cancellationToken);
}
async Task HandleCallBackQueryAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.CallbackQuery.Data != null)
    {
        string a = update.CallbackQuery.Data.ToString();
        await botClient.SendTextMessageAsync(
             chatId: update.CallbackQuery.From.Id,

             text: $"{a}",

             cancellationToken: cancellationToken);
    }
}
Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

Task HandleEditedMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    throw new NotImplementedException();
}

async Task HandleUnknownMessageTypeAsync(Update update1, Update update2, CancellationToken cancellationToken)
{
    throw new NotImplementedException();
}
async Task HandleUnknownUpdateType(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    throw new NotImplementedException();
}

#endregion