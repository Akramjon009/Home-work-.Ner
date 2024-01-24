using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


internal class Program
{
    private static async Task Main(string[] args)
    {

        var botClientDev = new TelegramBotClient("6909069659:AAG11DnALOYYhqT5qYPdK4pNeNZpBGXsl4s");

        //block 
        int blockLevel = 0;
        bool messDeleted = false;
      

        //Time
        int year;
        int month;
        int day;
        int hour;
        int minute;
        int second;

        //Messages and user info
        long chatId = 0;
        string messageText;
        int messageId;
        string firstName;
        string lastName;
        long id;
        Message sentMessage;

        //poll info
        int pollId = 0;


        //Read time and save variables
        year = int.Parse(DateTime.UtcNow.Year.ToString());
        month = int.Parse(DateTime.UtcNow.Month.ToString());
        day = int.Parse(DateTime.UtcNow.Day.ToString());
        hour = int.Parse(DateTime.UtcNow.Hour.ToString());
        minute = int.Parse(DateTime.UtcNow.Minute.ToString());
        second = int.Parse(DateTime.UtcNow.Second.ToString());
        Console.WriteLine("Data: " + year + "/" + month + "/" + day);
        Console.WriteLine("Time: " + hour + ":" + minute + ":" + second);

        //cts token
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

        //write on console a hello message by bot 
        Console.WriteLine($"\nHello! I'm {me.Username} and i'm your Bot!");

        // Send cancellation request to stop bot and close console
        Console.ReadKey();
        cts.Cancel();

        //----------------------//

        //Answer of the bot to the input.
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            //set variables
            chatId = update.Message.Chat.Id;
            messageText = update.Message.Text;
            messageId = update.Message.MessageId;
            firstName = update.Message.From.FirstName;
            lastName = update.Message.From.LastName;
            id = update.Message.From.Id;
            year = update.Message.Date.Year;
            month = update.Message.Date.Month;
            day = update.Message.Date.Day;
            hour = update.Message.Date.Hour;
            minute = update.Message.Date.Minute;
            second = update.Message.Date.Second;

            string Homework_datatime=("\nData message --> " + year + "/" + month + "/" + day + " - " + hour + ":" + minute + ":" + second);
            Console.WriteLine($"Received a '{messageText}' message in chat {chatId} from user:\n" + firstName + " - " + lastName + " - " + " 5873853");

            messageText = messageText.ToLower();

            if (messageText != null && int.Parse(day.ToString()) >= day && int.Parse(hour.ToString()) >= hour && int.Parse(minute.ToString()) >= minute && int.Parse(second.ToString()) >= second - 10)
            {
              
                var getchatmember = await botClient.GetChatMemberAsync("@Abduvahobov09",id);


                if (getchatmember.Status.ToString() == "Left" || getchatmember.Status.ToString() == null || getchatmember.Status.ToString() == "null" || getchatmember.Status.ToString() == "")
                {
                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                          {
                    new []
                    {
                        InlineKeyboardButton.WithUrl(text: "Canale 1", url: "https://t.me/Abduvahobov09"),
                    },
                });

                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Before use the bot you must follow this channels.\nWhen you are ready, click -> /home <- to continue", //The message to display
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);
                }
                else
                {

               
                    if (messageText == "/vulgarity")
                    {
                        switch (blockLevel)
                        {
                            case 0:
                                blockLevel = 1;
                                await botClient.SendTextMessageAsync
                                (
                                chatId: chatId,
                                text: "myblog_discuss: \"Medium block\".",
                                 cancellationToken: cancellationToken
                                );
                                return;

                            case 1:
                                blockLevel = 2;
                                await botClient.SendTextMessageAsync
                                (
                                chatId: chatId,
                                text: "myblog_discuss: \"Hard block\".",
                                 cancellationToken: cancellationToken
                                );
                                return;
                            case 2:
                                blockLevel = 0;
                                await botClient.SendTextMessageAsync
                                (
                                chatId: chatId,
                                text: "myblog_discuss: \"Block disabled\".",
                                 cancellationToken: cancellationToken
                                );
                                return;
                        }
                    }

                    
                    messDeleted = false;

                    


                  
                }
            }
        }

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