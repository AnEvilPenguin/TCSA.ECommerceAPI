# ECommerce API

This is a project designed around learning more about ASP.NET Core.  
Specifically this particular project is designed around simulating a more 
enterprise aligned project (compared to the more personal apps so far).  
The business case is going to be fairly loose. I'm going to pick something to 
'sell', and whilst there can be some creativity here, the focus should be on
the technical requirements.

# How to use

This project requires the use of Microsoft SQL Server. There shouldn't be any requirement to use a specific type or 
edition of SQL Server, however this was developed using the Linux container image:

``` powershell
docker pull mcr.microsoft.com/mssql/server
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

Which will then run the container using the default port, the developer edition, and (at the time of writing) the 2022
version of the software.

Once the project is built you'll need to update the configuration in the `appsettings.json` file to use your SQL
Server. During development the sa account was used. However, it should be possible to create a lower privilege account
as long as it can create a database and tables. If no database or tables are present the application will seed some data 
for you.

# Requirements

- [X] The project needs to be an ASP.NET Core Web API, with Entity Framework and a choice of SQL Server or Sqlite as a 
database
- [X] The API needs to use Dependency Injection
- [X] The database needs to have at least three tables
  - [X] Products
  - [X] Categories
  - [X] Sales
- [X] Products and Sales need to have a many-to-many relationship
- [X] Products need to have a price. Multiple products can be sold in the same sale. 
- [X] A Postman Collection with all possible requests for the API must be provided
- [X] The GetProducts and GetSales endpoints must have pagination capabilities
- [X] In retail it's good practice to prevent the deletion of records. Feel free to add soft-deletes 
- [X] You shouldn't update product prices. What would happen if you made a sale and later updated the price of that 
product?
- [ ] Seed data from a spreadsheet when there is no data present
- [ ] Report functionality
  - [ ] Wrtie report to a local PDF
- [ ] Handle errors gracefully
  - [ ] Incorrect path
  - [ ] Permissions issues
  - [ ] File corruption


## Stretch Goals

- [X] Add filtering and sorting capabilities to the endpoints
- [ ] Create a console UI to consume the Web API
- [ ] Seed data from choice of file formats
- [ ] Export data to different formats
- [ ] Periodic report functionality
- [ ] Import/Export to an Azure Blob for further processing
- [ ] Deal with potentially dodgy file formats
  - [ ] FileSignatures package  ~~or MimeDetective~~? Former looks more interesting

# Challenges

Another reasonably straightforward challenge here.   
Most of the issues I encountered came from overthinking and looking for the best way to achieve something. In most cases
I could have just cracked on and come to a similar (if not identical) solution.
I've held off on some of the stretch goals here, mostly as I've already done these in other projects. For example I've 
already used Spectre a lot in these projects and in my own personal projects.

# Lessons Learned

Funnily enough I'd already looked at dependency injection for an unrelated personal project just before this. 
I didn't expect it to come up here (though I was expecting it at some point). 
This was however the first time I used it with ASP.Net. The previous project was a client app.

# Areas to Improve

I still feel that I need more work with EF Core. I'm building up a reasonable code base of examples, but I wouldn't say 
that I feel I'm fluent yet. There have been some cases where I feel that I've potentially been generating queries badly.
Additionally I think I was a little bit slack on refactoring here. I should have put in more async code, and I could 
have moved a lot to a common base class if I had worked on it harder.

# Resources Used

- https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-9.0&tabs=visual-studio
- https://thecsharpacademy.com/course/6/article/1/500150/False

Generally I was able to do this from my existing repositories. However both of the previous links were helpful with 
refreshing my memory on certain topics (and the former in terms of database seeding)

I didn't actually use the following link in the end. However it was very interesting reading. I generally work with
JavaScript so merge patch is a natural default. I haven't encountered JSON Patch through work and was initially a bit
confused to find it referenced in all the documentation instead of merge patch.

https://blog.primarilysoftware.com/2019/json-merge-patch-dot-net/
