using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CS2_CaseOpening
{
    public class Account
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public double Balance { get; set; } = 100.0;
        public List<Skin> MySkins { get; set; } = new List<Skin>();
    }

    public static class AccountsManager
    {
        private static readonly string DirectoryPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CS2_CaseOpening");
        private static readonly string FilePath = Path.Combine(DirectoryPath, "accounts.json");

        private static List<Account> accounts = new List<Account>();
        public static Account? CurrentAccount { get; private set; }

        private static JsonSerializerOptions JsonOptions => new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public static void LoadAccounts()
        {
            try
            {
                if (!Directory.Exists(DirectoryPath))
                    Directory.CreateDirectory(DirectoryPath);

                if (!File.Exists(FilePath))
                {
                    accounts = new List<Account>();
                    SaveAccounts();
                    return;
                }

                string json = File.ReadAllText(FilePath);
                accounts = JsonSerializer.Deserialize<List<Account>>(json, JsonOptions) ?? new List<Account>();
            }
            catch
            {
                accounts = new List<Account>();
            }
        }

        public static void SaveAccounts()
        {
            try
            {
                if (!Directory.Exists(DirectoryPath))
                    Directory.CreateDirectory(DirectoryPath);

                string json = JsonSerializer.Serialize(accounts, JsonOptions);
                File.WriteAllText(FilePath, json);
            }
            catch
            {
                
            }
        }

        public static bool TryGetAccount(string username, out Account? account)
        {
            account = accounts.FirstOrDefault(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return account != null;
        }

        public static bool AddAccount(Account account)
        {
            if (accounts.Any(a => a.Username.Equals(account.Username, StringComparison.OrdinalIgnoreCase)))
                return false;

            accounts.Add(account);
            SaveAccounts();
            return true;
        }

        public static bool ValidateCredentials(string username, string password, out Account? account)
        {
            account = accounts.FirstOrDefault(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && a.Password == password);
            return account != null;
        }

        public static bool Login(string username, string password)
        {
            if (ValidateCredentials(username, password, out var account))
            {
                CurrentAccount = account!;
                SyncGameDataFromAccount();
                return true;
            }

            return false;
        }

        public static void Logout()
        {
            SyncAccountFromGameData();
            CurrentAccount = null;
        }

        public static void SyncGameDataFromAccount()
        {
            if (CurrentAccount is null) return;

            GameData.Balance = CurrentAccount.Balance;
            GameData.MySkins = new List<Skin>(CurrentAccount.MySkins ?? new List<Skin>());
        }

        public static void SyncAccountFromGameData()
        {
            if (CurrentAccount is null) return;

            CurrentAccount.Balance = GameData.Balance;
            CurrentAccount.MySkins = new List<Skin>(GameData.MySkins ?? new List<Skin>());
            SaveAccounts();
        }

        
        public static void SaveCurrentAccount()
        {
            SyncAccountFromGameData();
        }
    }
}