# ECommerce API

This is a project designed around learning more about ASP.NET Core.  
Specifically this particular project is designed around simulating a more 
enterprise aligned project (compared to the more personal apps so far).  
The business case is going to be fairly loose. I'm going to pick something to 
'sell', and whilst there can be some creativity here, the focus should be on
the technical requirements.

## Files expansion

This was extended later to include various different 'file' functionalities.  
This includes seeding data from various file formats, and exporting reports to different locations.

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

If you would like to seed data into the database you can do so by providing `SeedData` settings via `appsettings.json`:

```json
"SeedData": {
  "Enabled": true,
  "Products": {
    "Path": "../../../../SeedData/product.csv"
  },
  "Categories": {
    "Path": "../../../../SeedData/category.txt"
  },
  "Sales": {
    "Path": "../../../../SeedData/sale"
  },
  "ProductSales": {
    "Path": "../../../../SeedData/productSale.csv"
  }
},
```

The program will autodetect the file type and seed data based on this.  
Currently CSV and Excel (xlsx) files are supported.  
If using xlsx you may also wish to provide a specific sheet name to pull from. 
If you do not provide a specific sheet name it will be assumed that the first sheet contains 
the required data.

```json
"SeedData": {
    "Enabled": true,
    "Products": {
      "Path": "../../../../SeedData/SeedData.xlsx",
      "Sheet": "Product"
    },
    "Categories": {
      "Path": "../../../../SeedData/SeedData.xlsx",
      "Sheet": "Category"
    },
    "Sales": {
      "Path": "../../../../SeedData/SeedData.xlsx",
      "Sheet": "Sale"
    },
    "ProductSales": {
      "Path": "../../../../SeedData/SeedData.xlsx",
      "Sheet": "ProductSale"
    }
  },
```

Omitting a type will be taken as not wanting to seed that data.  
E.g. Omitting `Products` will be taken as not wanting to seed product data.
If relying on product data for other seeding (e.g. `ProductSales`) ensure that the required product data is
entered into the table via another route. Any values with mising references will be skipped.

Examples of valid files can be found in the [SeedData](SeedData) folder.  
Optional columns can be omitted, but must be filled in entirely if provided.

## Reports

This also includes a report API. This allows you to download various reports in a PDF format.
Call the API using a mechanism that allows for saving a file. E.g.:

```powershell
Invoke-WebRequest -Method Get -Uri "http://localhost:5000/api/v1/report/product" -OutFile "~/product.pdf"
```

If you have provided an Azure Blob service connection string you can also export the report to an Azure Blob
Storage account:

```json
"ConnectionStrings": {
    "AzureBlob": "DefaultEndpointsProtocol=https;AccountName=storageAccount;AccountKey=ASuperSecretKey;EndpointSuffix=core.windows.net",
    "CommerceContext": "Server=localhost;Database=ECommerce;User Id=myUser;Password=myPass;TrustServerCertificate=true;",
  },
```
```powershell
Invoke-WebRequest -Method Get -Uri "http://localhost:5000/api/v1/report/blob"
```

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
- [X] Seed data from a spreadsheet when there is no data present
- [X] Report functionality
  - [X] Write report to a local PDF
- [X] Handle errors gracefully
  - [X] Incorrect path
  - [X] Permissions issues
  - [X] File corruption


## Stretch Goals

- [X] Add filtering and sorting capabilities to the endpoints
- [ ] Create a console UI to consume the Web API
- [X] Seed data from choice of file formats
- [ ] Export data to different formats
- [ ] Periodic report functionality
- [X] Import/Export to an Azure Blob for further processing
- [X] Deal with potentially dodgy file formats
  - [X] FileSignatures package  ~~or MimeDetective~~? Former looks more interesting

# Challenges

Another reasonably straightforward challenge here.   
Most of the issues I encountered came from overthinking and looking for the best way to achieve something. In most cases
I could have just cracked on and come to a similar (if not identical) solution.
I've held off on some of the stretch goals here, mostly as I've already done these in other projects. For example I've 
already used Spectre a lot in these projects and in my own personal projects.

## Files expansion

The expansion to use 'files' was a fun one as well.  
I think I focused a bit too much on the interesting parts of the challenge and less about the structure.
An example being the automatic file type detection. This was fun and interesting, but led to a slightly 
clunky selection of seeder.  
Additionally I found the Excel section a bit on the tricky side. The library seemed to have limited resources
for getting started. Once I'd hit my stride (mostly thanks to examples from Chat GPT. Begrudgingly. I think
it's a great accelerator, but I worry that I may be sacrificing hard won lessons).

# Lessons Learned

Funnily enough I'd already looked at dependency injection for an unrelated personal project just before this. 
I didn't expect it to come up here (though I was expecting it at some point). 
This was however the first time I used it with ASP.Net. The previous project was a client app.

## Files expansion

The expansion had many great lessons.  
Firstly figuring out file types from magic numbers was something I had been aware of for a while, but this
was my first practical application. It showed me some fun tricks with streams as well.  
I think I've been exposed now to a number of useful libraries for working with these documents. Both ones I 
used and ones I researched and then chose to avoid.  
Sending back a file via API was a nice tool to the kit. Though there was lots of searching through Stack Overflow until I
found the right question. Most seemed to be around convincing a browser to download the file, not providing
via an API like I wanted.  
Finally working with blob storage was a good (if limited by my ambition at 10pm on a Sunday night) 
experience. I'm glad I got started with this and fully intend to explore this more.

# Areas to Improve

I still feel that I need more work with EF Core. I'm building up a reasonable code base of examples, but I wouldn't say 
that I feel I'm fluent yet. There have been some cases where I feel that I've potentially been generating queries badly.
Additionally I think I was a little bit slack on refactoring here. I should have put in more async code, and I could 
have moved a lot to a common base class if I had worked on it harder.

## Files expansion

On the file expansion side of things I think it's mostly Azure Blob. I'm glad I started this, but I 
probably need to look at it in more detail. This was a very limited expansion of my report capability here.
Whilst I didn't look at scheduling here, I went rogue on the previous project with my scheduling so I'm
not too worried about missing that.  
All in all I think I hit all the stretch goals I cared about here!

# Resources Used

- https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-9.0&tabs=visual-studio
- https://thecsharpacademy.com/course/6/article/1/500150/False

Generally I was able to do this from my existing repositories. However both of the previous links were helpful with 
refreshing my memory on certain topics (and the former in terms of database seeding)

I didn't actually use the following link in the end. However it was very interesting reading. I generally work with
JavaScript so merge patch is a natural default. I haven't encountered JSON Patch through work and was initially a bit
confused to find it referenced in all the documentation instead of merge patch.

https://blog.primarilysoftware.com/2019/json-merge-patch-dot-net/

## Files expansion

From the file expansion, so many different resources...  
Here are the key ones from my history:

- https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
- https://www.questpdf.com/quick-start.html
- https://stackoverflow.com/questions/42460198/return-file-in-asp-net-core-web-api
- https://learn.microsoft.com/en-us/office/open-xml/spreadsheet/structure-of-a-spreadsheetml-document?tabs=cs
- https://joshclose.github.io/CsvHelper/
- https://github.com/neilharvey/FileSignatures
