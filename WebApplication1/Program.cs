
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace WebApplication1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();


        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();
        var BotClient = new TelegramBotClient(builder.Configuration["Token"]!);
        BotClient.ReceiveAsync<UpdateHundler>();
        app.Run();

    }

}
public class UpdateHundler : IUpdateHandler
{
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {

            await botClient.SendTextMessageAsync(update.Message!.Chat.Id, update.Message.Text!);

        }
        catch (Exception ex)
        {

        }
    }
}
