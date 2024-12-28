# FinanceCaculationBot
Expense Tracker Telegram Bot is a C# .NET bot that helps track expenses and income via Telegram. Users input data in {amount} + {description} format, and the bot logs it into an Excel file, categorizing it as income or expense. Built with Telegram.Bot and EPPlus, it’s a lightweight tool for personal finance management.

## Features

- Easy expense/income logging via Telegram
- Automatic transaction categorization (Income for positive amounts, Expense for negative amounts)
- Excel-based storage
- Real-time transaction confirmation
- Simple message-based input

## Prerequisites

- .NET 6.0 or higher
- Telegram Bot Token (obtain from [@BotFather](https://t.me/botfather))
- NuGet Packages:
  - EPPlus
  - Telegram.Bot

## Installation

1. Clone the repository:
```bash
git clone https://github.com/codersaiya/FinanceCaculationBot.git
cd FinanceCaculationBot
```

2. Install required NuGet packages:
```bash
dotnet restore
```

3. Update the bot token:
   - Open `Program.cs`
   - Replace `YOUR_TOKEN` in the `BotToken` variable with your actual Telegram bot token:
```csharp
private static readonly string BotToken = "YOUR_TOKEN";
```

## Usage

### Starting the Bot

1. Build and run the project:
```bash
dotnet run
```

2. Open Telegram and search for your bot using the username you set up with BotFather
3. Start a conversation with the bot

### Recording Transactions

To record a transaction, send a message in the following format:
```
{amount} + {description}
```

Examples:
- Expense: `-50000 + Grocery shopping`
- Income: `1000000 + Monthly salary`

The bot will:
- Automatically categorize the transaction based on the amount's sign
- Save the transaction to `data.xlsx`
- Send a confirmation message

### Excel File Structure

The bot automatically creates and maintains an Excel file (`data.xlsx`) with the following columns:
- Ngày (Date)
- Loại (Type: Thu/Chi - Income/Expense)
- Số tiền (Amount)
- Mô tả (Description)

The file is updated in real-time with each new transaction.

## Error Handling

The bot handles several types of errors:
- Invalid message format
- Invalid amount input
- Excel file operations
- General bot operations

Users will receive appropriate error messages in Vietnamese when issues occur.

## Project Structure

```
├── Program.cs           # Main bot logic and Excel operations
└── data.xlsx           # Auto-generated Excel file for storing transactions
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgments

- [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot) library
- [EPPlus](https://github.com/JanKallman/EPPlus) for Excel operations

## Support

If you encounter any issues or have questions:
1. Check the existing issues in the repository
2. Create a new issue with a detailed description
3. Contact the maintainers through the repository

---

**Note**: Remember to never share your bot token publicly or commit it to version control systems.

## License
Distributed under the MIT License. See `LICENSE` for more information.

## 📞 Contact

Nhat Cuong (CoderSaiya) - sonysam.contacts@gmail.com

Project link: [project repo](https://github.com/CoderSaiya/DrivingExamApp)
