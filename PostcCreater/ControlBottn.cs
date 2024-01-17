using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

internal class ControlBottonClass
{
    public static async Task StartButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
        {
                new KeyboardButton[] { "create post" },
        }
            )

        {
            ResizeKeyboard = true
        };


        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            text: "Yangi post yaratmoqchi bolsangiz Create postni bosing",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }
    public static async Task CreateButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var buttons = new List<List<KeyboardButton>>();

        var buttonsgorizontal1 = new List<KeyboardButton>();
        buttonsgorizontal1.Add(new KeyboardButton("ChanelName update"));
        buttonsgorizontal1.Add(new KeyboardButton("PostText update"));
        buttonsgorizontal1.Add(new KeyboardButton("Image update"));
        buttonsgorizontal1.Add(new KeyboardButton("link update"));

        var buttonsgorizontal2 = new List<KeyboardButton>();
        buttonsgorizontal2.Add(new KeyboardButton("back"));
        buttonsgorizontal2.Add(new KeyboardButton("Save"));
        buttonsgorizontal2.Add(new KeyboardButton("Send Chanel"));
        buttons.Add(buttonsgorizontal1);
        buttons.Add(buttonsgorizontal2);


        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            text: "Choose a response",
            replyMarkup: new ReplyKeyboardMarkup(buttons),
            cancellationToken: cancellationToken);
    }
    public static async Task EditButtons(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var buttons = new List<List<KeyboardButton>>();

        var buttonsgorizontal1 = new List<KeyboardButton>();
        buttonsgorizontal1.Add(new KeyboardButton("Edit ChanelName"));
        buttonsgorizontal1.Add(new KeyboardButton("Edit PostText update"));
        buttonsgorizontal1.Add(new KeyboardButton("Edit Image"));
        buttonsgorizontal1.Add(new KeyboardButton("Edit link"));
        var buttonsgorizontal2 = new List<KeyboardButton>();
        buttonsgorizontal2.Add(new KeyboardButton("back"));
        buttonsgorizontal2.Add(new KeyboardButton("Edit Save"));
        buttons.Add(buttonsgorizontal1);
        buttons.Add(buttonsgorizontal2);

        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            text: "Choose a response",
            replyMarkup: new ReplyKeyboardMarkup(buttons),
            cancellationToken: cancellationToken);
    }
}
