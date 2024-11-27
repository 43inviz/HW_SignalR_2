using Microsoft.AspNetCore.SignalR;

namespace HW_SignalR1
{
    public class CurrencyHub:Hub
    {
        public async Task SendCurrencyUpdate(string currencyPair,decimal rate)
        {
            await Clients.All.SendAsync("ReceiveCurrencyUpdate", currencyPair, rate);
        }
    }
}
