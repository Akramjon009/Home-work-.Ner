using Telegram.Bot.Types;
using Telegram.Bot;
namespace TelegramBot
{
    internal class Functhion
    {
         
        public Functhion(string path)
        {
           
            TelegramBotClient client = new TelegramBotClient(path);
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }
        public static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message != null){
                if (message.Text == @"/start")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        replyToMessageId: message.MessageId,
                        text: "user id: " + message.Chat.Id.ToString() + "\n" +
                        "User name: " + message.Chat.Username.ToString() + "\n",
                        cancellationToken: token);

                }
                else if (message.Text != null)
                {
                    await botClient.SendTextMessageAsync(
                                       chatId: message.Chat.Id,
                                       replyToMessageId: message.MessageId,
                                       text: message.Text,
                                       cancellationToken: token);
                }
                else if (message.Sticker != null)
                {
                    await botClient.SendStickerAsync(
                       chatId: message.Chat.Id,
                       replyToMessageId: message.MessageId,
                       sticker: InputFile.FromFileId(message.Sticker!.FileId),
                       cancellationToken: token);
                }
                else if (message.Voice != null)
                {
                    await botClient.SendVoiceAsync(
                        chatId: message.Chat.Id,
                        replyToMessageId: message.MessageId,
                        voice: InputFile.FromFileId(message.Voice!.FileId),
                        cancellationToken: token);
                }
                else if (message.Photo != null)
                {
                    await botClient.SendPhotoAsync(
                       chatId: message.Chat.Id,
                       photo: InputFile.FromUri("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5VvWIuY9JvTM7aPlVwE_VxmuGOTz9Zajpug&usqp=CAU"),
                       cancellationToken: token);
                    Console.WriteLine($"Recieved Photo from {message.Chat.Username}");
                }
                else if (message.Audio != null)
                {
                    await botClient.SendVideoAsync(
                         chatId: message.Chat.Id,
                         replyToMessageId: message.MessageId,
                         video: InputFile.FromFileId(message.Audio!.FileId),
                         cancellationToken: token);
                }
                else if (message.Video != null)
                {
                    await botClient.SendVideoAsync(
                     chatId: message.Chat.Id,
                     video: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4")
                     );
                }
                else if (message.Animation != null)
                {
                    await botClient.SendAnimationAsync(
                        chatId: message.Chat.Id,
                        replyToMessageId: message.MessageId,
                        animation: InputFile.FromFileId(message.Animation!.FileId),
                        cancellationToken: token);
                }
                else if (message.VideoNote != null)
                {
                    await botClient.SendVideoNoteAsync(
                         chatId: message.Chat.Id,
                         replyToMessageId: message.MessageId,
                         videoNote: InputFile.FromFileId(message.VideoNote!.FileId),
                         cancellationToken: token);
                }
                else if (message.Location != null)
                {
                    await botClient.SendLocationAsync(message.Chat.Id, latitude: 37.7576793, longitude: -122.5076402);
                }
                else
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, "ozer");
                }
            }
        }
    }
}
