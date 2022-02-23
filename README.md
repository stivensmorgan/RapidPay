# RapidPay
Payment provider API for coding challenge at BairesDev

Technology used in RapidPay project:
* Microsoft NetCore 5
* SQL Server Express 2019
* EntityFramework 5
* Microsoft Visual Studion 2019

## Migrations
Before run the application, please run the migration tool for create and initiale the database
1. Change the connection string in the file RapidPay.Persistency / Context / RapidPayContextFactory.cs
~~~
  optionsBuilder.UseSqlServer(
    "Server=localhost;Database=RapidPay;User Id=sa;Password=YOUR-PASSWORD;");
~~~
2. Open the PM Console in Tools / Nuget Package Manager / Package Manager Console
3. execute the following migrations command:
~~~
update-database -p RapidPay.Persistency -s RapidPay.Persistency
~~~
5. jhh
