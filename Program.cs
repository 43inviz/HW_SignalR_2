using Microsoft.AspNetCore.SignalR;

namespace HW_SignalR1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            StartCurrency(app.Services);
            app.Run();
        }
        private static void StartCurrency(IServiceProvider services)
        {
            var random = new Random();
            var hubContext = services.GetService<IHubContext<CurrencyHub>>();

            new Thread(() =>
            {
                while (true)
                {
                    var usdToEur = Math.Round(random.NextDouble() * (1.2 - 1.0) + 1.0, 4);
                    var gbpToEur = Math.Round(random.NextDouble() * (1.2 - 1.0) + 1.0, 4);

                    hubContext.Clients.All.SendAsync("ReceiveCurrencyUpdate", "USD/EUR", usdToEur);
                    hubContext.Clients.All.SendAsync("ReceiveCurrencyUpdate", "GBP/EUR", gbpToEur);

                    Thread.Sleep(5000);
                }
            }).Start();
        }
    }

}
