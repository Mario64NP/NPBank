# NPBank

A Windows Forms CRUD application for managing bank clients, accounts, transactions, and exchange rates. Built in C# with Entity Framework, NUnit tests, and a layered architecture. Developed as a college project to simulate a desktop financial management system.

---

## 🏦 Features

- Manage clients (individuals and companies), bank accounts, fiscal accounts, and transactions
- Track currencies and exchange rates
- Fetch real-world exchange rates via external API

---

## 💻 Tech Stack

- **Framework:** Windows Forms (.NET 7)
- **Language:** C#
- **ORM:** Entity Framework (Code-First)
- **Architecture:** Layered (UI, Controller, Model, DbContext)
- **Testing:** NUnit
- **Database:** SQL Server (local instance)
- **API Integration:** External currency exchange API (apilayer.com)

---

## 🗂️ Data Structure

- **Clients**: Name, Phone Number, Email, Bank Account, Owner (Legal Entities)  
- **Bank Accounts**: Owner, Date Created, Fiscal Accounts  
- **Fiscal Accounts**: Number, Currency, Balance, Bank Account  
- **Transactions**: From Account, To Account, Amount, Timestamp  
- **Currencies**: Name, Code
- **Exchange Rates**: From Currency, To Currency, Rate

---

## 🚀 How to Run

1. Clone this repository:

    ```bash
    git clone https://github.com/Mario64NP/NPBank.git
    ```

2. Open the `NPBank.sln` file in Visual Studio

3. Ensure your local SQL Server instance is running

4. Run Entity Framework migrations to create the database:

    ```bash
    Update-Database
    ```

    Optionally run the included `sql.sql` script to populate sample data

5. Build and run the app via Visual Studio

---

## 🧪 Testing

The project includes unit tests for:
- **Entity validation** – ensures models meet expected constraints (e.g. required fields, valid data)
- **Controller logic** – verifies business rules like adding/removing clients, handling duplicates, and API interaction
- **Transaction operations** – tests for correct behavior of transfers, balance updates, and edge cases (included in controller tests)

Run the tests using the `NPBank.UnitTests` project in Visual Studio with NUnit.

---

## 📦 Notes

- The app uses a modular project structure and separates concerns cleanly
- `Coordinator.cs` centralizes database access across forms
- External API integration for exchange rates adds real-world functionality
- Created as a college project, but structured with clarity and maintainability in mind
