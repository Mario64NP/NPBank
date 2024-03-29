USE [NPBank]
GO
SET IDENTITY_INSERT [dbo].[BankAccounts] ON 

INSERT [dbo].[BankAccounts] ([ID], [Client], [DateCreated]) VALUES (2, 1, CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[BankAccounts] ([ID], [Client], [DateCreated]) VALUES (6, 9, CAST(N'2023-01-13T03:15:19.1890208' AS DateTime2))
SET IDENTITY_INSERT [dbo].[BankAccounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Currencies] ON 

INSERT [dbo].[Currencies] ([ID], [Name], [Code]) VALUES (1, N'Dinar', N'RSD')
INSERT [dbo].[Currencies] ([ID], [Name], [Code]) VALUES (2, N'Euro', N'EUR')
INSERT [dbo].[Currencies] ([ID], [Name], [Code]) VALUES (3, N'US Dolar', N'USD')
SET IDENTITY_INSERT [dbo].[Currencies] OFF
GO
SET IDENTITY_INSERT [dbo].[FiscalAccounts] ON 

INSERT [dbo].[FiscalAccounts] ([ID], [Number], [CurrencyID], [Balance], [BankAccountID]) VALUES (1, N'840-123-40', 2, 80, 2)
INSERT [dbo].[FiscalAccounts] ([ID], [Number], [CurrencyID], [Balance], [BankAccountID]) VALUES (2, N'840-123-41', 3, 40.9, 2)
SET IDENTITY_INSERT [dbo].[FiscalAccounts] OFF
GO
INSERT [dbo].[ExchangeRates] ([FromCurrencyID], [ToCurrencyID], [Rate]) VALUES (3, 1, 108.180854)
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([Id], [FromAccountID], [ToAccountID], [Amount], [Timestamp]) VALUES (1, 1, 2, 10, CAST(N'2023-01-02T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Transactions] ([Id], [FromAccountID], [ToAccountID], [Amount], [Timestamp]) VALUES (6, 1, 2, 10, CAST(N'2023-01-13T11:09:05.8286385' AS DateTime2))
INSERT [dbo].[Transactions] ([Id], [FromAccountID], [ToAccountID], [Amount], [Timestamp]) VALUES (7, 1, 2, 50, CAST(N'2023-01-13T11:09:51.5508595' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221202025640_Init', N'7.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230109024029_AddedFiscalAccounts', N'7.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230109082944_AddedCurrencies', N'7.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230110032932_ChangeDecimalToDouble', N'7.0.0')
GO
INSERT [dbo].[LegalEntity] ([ID], [Name], [PhoneNumber], [Email], [Owner]) VALUES (3, N'Microsoft', N'345', N'mail@microsoft.com', N'Nadela')
INSERT [dbo].[LegalEntity] ([ID], [Name], [PhoneNumber], [Email], [Owner]) VALUES (4, N'mt:s', N'456', N'mail@telekom.rs', N'Lučić')
GO
INSERT [dbo].[NaturalEntity] ([ID], [Name], [PhoneNumber], [Email]) VALUES (1, N'Mario', N'123', N'abc@mail.com')
INSERT [dbo].[NaturalEntity] ([ID], [Name], [PhoneNumber], [Email]) VALUES (9, N'Novke', N'1355', N'asd@123.com')
GO
