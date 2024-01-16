try
{
    TelegramBot bot = new TelegramBot();
    await bot.MainFunction();
}
catch (NullReferenceException)
{
    throw new Exception("Hato");
}
catch (Exception)
{
    throw new Exception("Ota kata hato ");
}