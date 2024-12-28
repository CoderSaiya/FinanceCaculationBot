using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{
    static void Main()
    {
        System.Console.WriteLine("Hello World!");
    }

    [Obsolete]
    public static async void HandelUpdateAsyn(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != UpdateType.Message || update.Message!.Type != MessageType.Text)
            return;

        var text = update.Message.Text;
        var chatId = update.Message.Chat.Id;
        var message = update.Message;

        try
        {
            var parts = text!.Split('+', StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                await botClient.SendTextMessageAsync(chatId, "Cú pháp không hợp lệ. Hãy nhập theo dạng: {số tiền} + {mô tả}.", cancellationToken: cancellationToken);
                return;
            }

            if (!decimal.TryParse(parts[0], out var amount))
            {
                await botClient.SendTextMessageAsync(chatId, "Số tiền không hợp lệ.", cancellationToken: cancellationToken);
                return;
            }

            var description = parts[1];

            var type = amount > 0 ? "Thu" : "Chi";

            var date = DateTime.Now;

            WriteToExcel(date, type, Math.Abs(amount), description);

            await botClient.SendTextMessageAsync(chatId, $"Đã ghi: {type} {Math.Abs(amount):N0} - {description}", cancellationToken: cancellationToken);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
            await botClient.SendTextMessageAsync(chatId, "Có lỗi xảy ra khi xử lý. Vui lòng thử lại.", cancellationToken: cancellationToken);
        }
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Lỗi: {exception.Message}");
        return Task.CompletedTask;
    }

    public static void WriteToExcel(DateTime date, string type, decimal amount, string description)
    {
        // Write to Excel

    }
}