# The Personal Finance Manager (PFM)
ASP.Net MVC web application for control personal expenses/revenues. </br>
Uses the Entity Framework to access data from a MySQL database.

Contains two entities: categories and transactions.
- Categories store information about type of purchase e.g. Food, Traveling, Clothes etc.
- Transactions have information about this expenses or revenues.

Fully implemented with CRUD operations.

Expenses Report available!

<hr/>

### Categories
![image](https://github.com/ShadowPrice1328/Personal_Finance_Manager/assets/60846759/6bf7a2dc-70d7-4e6a-b87a-6dff5b1c2c1a)
### Transactions
![image](https://github.com/ShadowPrice1328/Personal_Finance_Manager/assets/60846759/33db6105-72c5-4fec-b5e3-fbf11c4fe89d)
### Report Generator
![image](https://github.com/ShadowPrice1328/Personal_Finance_Manager/assets/60846759/ba5f731c-3d9d-4e37-8b1c-b1f15a366e20)
![image](https://github.com/ShadowPrice1328/Personal_Finance_Manager/assets/60846759/a47f36a5-f0de-45d2-a4c3-20c0c02b827b)
![image](https://github.com/ShadowPrice1328/Personal_Finance_Manager/assets/60846759/33caf3ec-43b4-47ed-9b94-ca3177f17017)
_(Day-by-day only works for expenses, regardless of what you chose!)_

### How to use it?
All you need to do is create a connection to your database and configure the connection string in the [Program.cs](Program.cs) file. <br/>
_**It is not necessary to create tables!**_
``` cs
options.UseMySQL("server=127.0.0.1;user=root;password=1111;database=pfm;"));
```
[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/banner-personal-page.svg)](https://stand-with-ukraine.pp.ua)
