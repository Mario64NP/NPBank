using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Text.Json;

namespace Controller
{
    public class Coordinator
    {
        public BankContext BankContext { get; }

        public Coordinator() 
        {
            BankContext = new BankContext();

            BankContext.Clients.Load();
            BankContext.BankAccounts.Load();
            BankContext.Currencies.Load();
            BankContext.FiscalAccounts.Load();
            BankContext.ExchangeRates.Load();
            BankContext.Transactions.Load();
        }

        /// <summary>
        /// Executes the transaction. Decreases the balance of the source account by the amount of the transaction,
        /// and increases the balance of the destination account by the amount of the transaction multiplied by the exchange rate.
        /// </summary>
        /// <param name="t">The transaction to execute</param>
        public void ExecuteTransaction(Transaction t)
        {
            double rate = 0;

            if (t.FromAccount.Currency.Equals(t.ToAccount.Currency))
                rate = 1;
            else
                rate = BankContext.ExchangeRates.Single(r => r.FromCurrency.Equals(t.FromAccount.Currency) && r.ToCurrency.Equals(t.ToAccount.Currency)).Rate;

            t.FromAccount.Balance -= t.Amount;
            t.ToAccount.Balance += t.Amount * rate;
            BankContext.SaveChanges();
        }

        /// <summary>
        /// Gets the current exchange rate for the selected currencies online
        /// </summary>
        /// <param name="apiKey">Your API key for <see href="https://apilayer.com">apilayer.com</see></param>
        /// <returns>The current exchange rate</returns>
        public static double GetRateOnline(string apiKey, string fromCurrencyCode, string toCurrencyCode)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
            string response = httpClient.GetStringAsync($"https://api.apilayer.com/exchangerates_data/convert?to={toCurrencyCode}&from={fromCurrencyCode}&amount=1").Result;

            using JsonDocument document = JsonDocument.Parse(response);
            JsonElement rootElement = document.RootElement;
            return rootElement.GetProperty("result").GetDouble();
        }
    }
}