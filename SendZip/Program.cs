using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using System.IO.Compression;
using static Zips;
using File = System.IO.File;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient("6482056515:AAHU6-FHmT6yKxcIlS4wZFMaOGKIazE_fps");

        using CancellationTokenSource cts = new();

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
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
        cts.Cancel();
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

        

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => HandleMessageAsync(botClient, update, cancellationToken),
                _ => HandleMessageAsync(botClient, update, cancellationToken),
            };
        }

        Task HandleUnknownUpdateType(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var user = message.Chat.FirstName;
            switch (message.Type)
            {
                case MessageType.Text:
                    HandleTextMessageAsync(botClient, update, cancellationToken, user).GetAwaiter();
                    break;


                default:
                    HandleUnknownMessageTypeAsync(update, update, cancellationToken).GetAwaiter();
                    break;
            };
        }

        Task HandleUnknownMessageTypeAsync(Update update1, Update update2, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        async Task HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string? user)
        {
            var message = update.Message;
            string filename = message.Text.ToString();


            await using Stream stream = System.IO.File.OpenRead(Zipfiles(filename));
            await botClient.SendDocumentAsync(
                        chatId: message.Chat.Id,
                        disableNotification: true,
                        replyToMessageId: message.MessageId,
                        document: InputFile.FromStream(stream: stream, fileName: "result.zip"),
                        cancellationToken: cancellationToken);
            Directory.Delete(filename, true);
        }
    }
}