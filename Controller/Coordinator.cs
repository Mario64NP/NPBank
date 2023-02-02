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

        public int AddClient(Client cl)
        {
            BankContext.Add(cl);
            return BankContext.SaveChanges();
        }

        public int RemoveClient(Client cl)
        {
            BankContext.Remove(cl);
            return BankContext.SaveChanges();
        }

        public List<Client> GetAllClients()
        {
            return BankContext.Clients.Local.ToList();
        }

        public int AddBAccount(BankAccount b)
        {
            BankContext.Add(b);
            return BankContext.SaveChanges();
        }

        public int RemoveBAccount(BankAccount b)
        {
            BankContext.Remove(b);
            return BankContext.SaveChanges();
        }

        public List<BankAccount> GetAllBAccounts()
        {
            return BankContext.BankAccounts.Local.ToList();
        }

        public int AddFAccount(FiscalAccount f)
        {
            BankContext.Add(f);
            return BankContext.SaveChanges();
        }

        public int RemoveFAccount(FiscalAccount f)
        {
            BankContext.Remove(f);
            return BankContext.SaveChanges();
        }

        public List<FiscalAccount> GetAllFAccounts()
        {
            return BankContext.FiscalAccounts.Local.ToList();
        }

        public int AddExchangeRate(ExchangeRate e)
        {
            BankContext.Add(e);
            return BankContext.SaveChanges();
        }

        public int RemoveExchangeRate(ExchangeRate e) 
        {
            BankContext.Remove(e);
            return BankContext.SaveChanges();
        }

        public List<ExchangeRate> GetAllExchangeRates()
        {
            return BankContext.ExchangeRates.Local.ToList();
        }

        public int AddTransaction(Transaction t)
        {
            BankContext.Add(t);
            return BankContext.SaveChanges();
        }

        public int RemoveTransaction(Transaction t)
        {
            BankContext.Remove(t);
            return BankContext.SaveChanges();
        }

        public List<Transaction> GetAllTransactions()
        {
            return BankContext.Transactions.Local.ToList();
        }

        public List<Currency> GetAllCurrencies() 
        {
            return BankContext.Currencies.Local.ToList();
        }
    }
}