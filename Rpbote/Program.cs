using System.IO.Compression;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


internal class Program
{
    private static async Task Main(string[] args)
    {

        var botClientDev = new TelegramBotClient("6909069659:AAG11DnALOYYhqT5qYPdK4pNeNZpBGXsl4s");

        string filepath = "C:\\Users\\akraz\\Desktop\\myfail\\achivement\\path.txt";

        int blockLevel = 0;

        int year;
        int month;
        int day;
        int hour;
        int minute;
        int second;

        long chatId = 0;
        string messageText;
        int messageId;
        string firstName;
        string lastName;
        long id;
        Message sentMessage;

        int pollId = 0;


        year = int.Parse(DateTime.UtcNow.Year.ToString());
        month = int.Parse(DateTime.UtcNow.Month.ToString());
        day = int.Parse(DateTime.UtcNow.Day.ToString());
        hour = int.Parse(DateTime.UtcNow.Hour.ToString());
        minute = int.Parse(DateTime.UtcNow.Minute.ToString());
        second = int.Parse(DateTime.UtcNow.Second.ToString());
        Console.WriteLine("Data: " + year + "/" + month + "/" + day);
        Console.WriteLine("Time: " + hour + ":" + minute + ":" + second);

        using var cts = new CancellationTokenSource();

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }
        };
        botClientDev.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token);

        var me = await botClientDev.GetMeAsync();

        Console.WriteLine($"\nHello! I'm {me.Username} and i'm your Bot!");

        Console.ReadKey();
        cts.Cancel();


        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            chatId = message.Chat.Id;
            messageText = message.Text;
            messageId = message.MessageId;
            firstName = message.From.FirstName;
            lastName = message.From.LastName;
            id = message.From.Id;
            year = message.Date.Year;
            month = message.Date.Month;
            day = message.Date.Day;
            hour = message.Date.Hour;
            minute = message.Date.Minute;
            second = message.Date.Second;

            string Homework_datatime = ("\nData message --> " + year + "/" + month + "/" + day + " - " + hour + ":" + minute + ":" + second);
            Console.WriteLine($"Received a '{messageText}' message in chat {chatId} from user:\n" + firstName + " - " + lastName + " - " + " 5873853");



            if (messageText != null && int.Parse(day.ToString()) >= day && int.Parse(hour.ToString()) >= hour && int.Parse(minute.ToString()) >= minute && int.Parse(second.ToString()) >= second - 10)
            {

                var getchatmember = await botClient.GetChatMemberAsync("@Abduvahobov09", id);


                if (getchatmember.Status.ToString() == "Left" || getchatmember.Status.ToString() == null || getchatmember.Status.ToString() == "null" || getchatmember.Status.ToString() == "")
                {
                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                          {
                    new []
                    {
                        InlineKeyboardButton.WithUrl(text: "Canale 1", url: "https://t.me/Abduvahobov09"),

                    },
                }); ;

                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Before use the bot you must follow this channels.\nWhen you are ready, click -> /home <- to continue", //The message to display
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);



                    ReplyKeyboardMarkup replyKeyboardMarkup = new(
                        new[]
                         {
                                  KeyboardButton.WithRequestContact("Contact"),

                         })
                    {
                        ResizeKeyboard = true
                    };
                    Thread.Sleep(4000);

                    await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "𝐀𝐬𝐬𝐚𝐥𝐨𝐦𝐮 𝐚𝐥𝐞𝐲𝐤𝐮𝐦, 𝐛𝐨𝐭𝐝𝐚𝐧 𝐟𝐨𝐲𝐝𝐚𝐥𝐚𝐧𝐢𝐬𝐡 𝐮𝐜𝐡𝐮𝐧 𝐤𝐨𝐧𝐭𝐚𝐤𝐭𝐧𝐢 𝐣𝐨'𝐧𝐚𝐭𝐢𝐧𝐠",
                                replyMarkup: replyKeyboardMarkup,
                                cancellationToken: cancellationToken);


                }
            }

            else if (message.Type == MessageType.Document)
            {
                await botClient.SendDocumentAsync(
                    chatId: "@Abduvahobov09",
                    document: InputFile.FromFileId(message.Document.FileId),
                    cancellationToken: cancellationToken);

                await botClient.SendTextMessageAsync(message.Chat.Id, "𝐊𝐮𝐭𝐚𝐲𝐨𝐭𝐤𝐚𝐧..");
                return;
            }

            else if (message.Contact != null)
            {
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine(message.Contact.PhoneNumber);
                    writer.WriteLine(message.Contact.FirstName);
                    writer.WriteLine("Time: " + hour + ":" + minute + ":" + second);
                }
            }

            if (message.Text != null)
            {
                Console.WriteLine(message.Chat.Id+" user id");

            }
        }

f



        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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


    }
}