using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
internal class Program
{
    static async Task Main(string[] args)
    {
        EssentialControlBot bot = new EssentialControlBot();
        await bot.WorkingBot();

    }
}
public class EssentialControlBot
{
    public async Task WorkingBot()
    {
        TelegramBotClient botClient = new TelegramBotClient("6937198399:AAEcSrSbn3w5GFOKMUGv8_faC2SNtDzOnvw");
        using CancellationTokenSource cts = new();
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token);
        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
        cts.Cancel();
    }
    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { })
            return;

        var handler = update.Type switch
        {

            UpdateType.Message => ControlMessageClass.EssentialAsyncMessga(botClient, update, cancellationToken),
            _ => ControlMessageClass.OtherMessga(botClient, update, cancellationToken),
        };
        try
        {
            await handler;
        }
        catch (Exception ex)
        {
            throw new Exception();
        }

    }

    static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
public class ControlMessageClass
{
    public static string? posttext = null;
    public static string? chanelname = null;
    public static string? photo = null;
    public static string? link = null;

    public static bool posttextBOOL = false;
    public static bool chanelnameBOOL = false;
    public static bool photoBOOL = false;
    public static bool linkBOOL = false;

    public static async Task EssentialAsyncMessga(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var handler = update.Message.Type switch
        {
            MessageType.Text => TextAsyncFunction(botClient, update, cancellationToken),
            MessageType.Photo => PhotoAsyncFunctionPost(botClient, update, cancellationToken),
            _ => OtherMessga(botClient, update, cancellationToken),
        };
    }
    public static async Task TextAsyncFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        #region
        var message = update.Message.Text;
        if (message == "/start")
        {
            chanelnameBOOL = false;
            posttextBOOL = false;
            photoBOOL = false;
            chanelname = null; photo = null; posttext = null;
            ControlBottonClass.StartButton(botClient, update, cancellationToken);


            Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            replyToMessageId: update.Message.MessageId,
            text: "Postni quyidagi tartibda yasaysiz\n\n" +
            "Chanelname update : kanal nomi\n" +
            "Post update : matn qismi\n" +
            "Image update : rasm joylaysiz\n\n" +
            "BU MAALUMOTNI TOLIQ KIRITNASANGIZ BOT ISHLAMAYDI!\n\n" +
            "/start qayta boshlash\n" +
            "/see tayyor postni ko'rish\n" +
            "/edit postni tahrirlash\n",
            cancellationToken: cancellationToken);
        }
        else if (message == "create post")
        {
            ControlBottonClass.CreateButton(botClient, update, cancellationToken);
        }
        else if (message == "back")
        {
            chanelnameBOOL = false;
            posttextBOOL = false;
            photoBOOL = false;
            chanelname = null; photo = null; posttext = null;

            ControlBottonClass.StartButton(botClient, update, cancellationToken);
        }
        else if (message == "/see")
        {
            seepost(botClient, update, cancellationToken);
        }
        else if (message == "/edit")
        {
            if (photo != null && chanelname != null && posttext != null)
            {
                ControlBottonClass.EditButtons(botClient, update, cancellationToken);
            }
            else
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                replyToMessageId: update.Message.MessageId,
                text: "Edit qilishingiz uchun postni yaratgan bolishingiz kerak!",
                cancellationToken: cancellationToken);
            }
        }
        else if (message == "Send Chanel")
        {
            if (photo != null && chanelname != null && posttext != null)
            {
                Message sentMessage = await botClient.SendPhotoAsync(
                    chatId: "@Abduvahobov09",
                    photo: InputFile.FromFileId(photo),
                    caption: posttext + "\n" + "Kanalga o'tiing : " + chanelname,
                    cancellationToken: cancellationToken);

            }
        }
        else if (message == "ChanelName update" || message == "Edit ChanelName")
        {
            chanelnameBOOL = true;

            Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            replyToMessageId: update.Message.MessageId,
            text: "Kanal nomini kiriting ",
            cancellationToken: cancellationToken);

        }

        else if (message == "PostText update" || message == "Edit PostText")
        {
            posttextBOOL = true;
            Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            replyToMessageId: update.Message.MessageId,
            text: "Post kiriting ",
            cancellationToken: cancellationToken);
        }
        #endregion


        else if (message == "Image update" || message == "Edit Image")
        {
            photoBOOL = true;
            Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            replyToMessageId: update.Message.MessageId,
            text: "Rasm jonating ",
            cancellationToken: cancellationToken);
        }
        else if (message == "link update" || message == "Edit link")
        {
            linkBOOL = true;
            Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            replyToMessageId: update.Message.MessageId,
            text: "link jonating ",
            cancellationToken: cancellationToken);
        }


        else
        {
            if (posttextBOOL)
            {
                posttext = message;
                posttextBOOL = false;
                Message sentMessage = await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               replyToMessageId: update.Message.MessageId,
               text: "qabul qilindi ",
               cancellationToken: cancellationToken);


            }
            else if (chanelnameBOOL)
            {
                chanelname = message;
                chanelnameBOOL = false;
                Message sentMessage = await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               replyToMessageId: update.Message.MessageId,
               text: "qabul qilindi ",
               cancellationToken: cancellationToken);
            }
            else if (linkBOOL)
            {
                link = message;
                linkBOOL = false;
                Message sentMessage = await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               replyToMessageId: update.Message.MessageId,
               text: "qabul qilindi ",
               cancellationToken: cancellationToken);
            }
            else
            {
                return;
            }
        }

    }
    public static async Task PhotoAsyncFunctionPost(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (photoBOOL)
        {
            photo = update.Message.Photo.Last().FileId;
            photoBOOL = false;
            Message sentMessage = await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               replyToMessageId: update.Message.MessageId,
               text: "qabul qilindi ",
               cancellationToken: cancellationToken);
        }
    }
    public static async Task OtherMessga(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        return;
    }
    public static async Task seepost(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (photo == null)
        {
            if (chanelname != null && posttext != null)
            {
                ///jdgfuygdsyifgyis
                var message = update.Message;
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    disableNotification: true,
                    replyToMessageId: message.MessageId,
                    text: posttext + "\n" + "Kanalga o'ting : " + chanelname + " \n " + link,
                    cancellationToken: cancellationToken);
            }
        }

        else
        {
            if (chanelname != null && posttext != null)
            {
                var message = update.Message;
                Message sentMessage = await botClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    disableNotification: true,
                    replyToMessageId: message.MessageId,
                    caption: posttext + "\n" + "Kanalga o'ting : " + chanelname + " \n " + link,
                    photo: InputFile.FromFileId(photo),
                    captionEntities: null,
                    cancellationToken: cancellationToken);

            }
        }

    }

}
