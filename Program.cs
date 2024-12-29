using OfficeOpenXml;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{
    private static readonly string ExcelFilePath = "data.xlsx";
    private static readonly string BotToken = "TOKEN_HERE"; // thay bằng token bot của bạn
    static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient(BotToken);

        Console.WriteLine("Bot đang chạy...");

        // Thiết lập handler nhận tin nhắn
        using var cts = new CancellationTokenSource();
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() },
            cancellationToken: cts.Token
        );

        Console.ReadLine();
        cts.Cancel();
    }

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
            await botClient.SendTextMessageAsync(chatId, "Có lỗi xảy ra khi xử lý. Vui lòng thử lại.", cancellationToken: cancellationToken);
        }
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource handleErrorSource, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Lỗi: {exception.Message}");
        return Task.CompletedTask;
    }

    public static void WriteToExcel(DateTime date, string type, decimal amount, string description)
    {
        // Write to Excel
        FileInfo file = new FileInfo(ExcelFilePath);

        using var package = new ExcelPackage(file);

        var worksheet = package.Workbook.Worksheets.Count == 0 
            ? package.Workbook.Worksheets.Add("Sheet1") 
            : package.Workbook.Worksheets[0];

        if (worksheet.Dimension == null)
        {
            worksheet.Cells[1, 1].Value = "Ngày";
            worksheet.Cells[1, 2].Value = "Loại";
            worksheet.Cells[1, 3].Value = "Số tiền";
            worksheet.Cells[1, 4].Value = "Mô tả";
        }

        var row = worksheet.Dimension.Rows + 1;

        worksheet.Cells[row, 1].Value = date.ToString();
        worksheet.Cells[row, 2].Value = type;
        worksheet.Cells[row, 3].Value = amount;
        worksheet.Cells[row, 4].Value = description;

        package.Save();
    }
}