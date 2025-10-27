# ECommerce API

This is a project designed around learning more about ASP.NET Core.  
Specifically this particular project is designed around simulating a more 
enterprise aligned project (compared to the more personal apps so far).  
The business case is going to be fairly loose. I'm going to pick something to 
'sell', and whilst there can be some creativity here, the focus should be on
the technical requirements.

# How to use

TODO

# Requirements

- [ ] The project needs to be an ASP.NET Core Web API, with Entity Framework and a choice of SQL Server or Sqlite as a database
- [ ] The API needs to use Dependency Injection
- [ ] The database needs to have at least three tables
  - [ ] Products
  - [ ] Categories
  - [ ] Sales
- [ ] Products and Sales need to have a many-to-many relationship
- [ ] Products need to have a price. Multiple products can be sold in the same sale. 
- [ ] A Postman Collection with all possible requests for the API must be provided
- [ ] The GetProducts and GetSales endpoints must have pagination capabilities
- [ ] In retail it's good practice to prevent the deletion of records. Feel free to add soft-deletes 
- [ ] You shouldn't update product prices. What would happen if you made a sale and later updated the price of that product?


## Stretch Goals

- [ ] Add filtering and sorting capabilities to the endpoints
- [ ] Create a console UI to consume the Web API

# Features

TODO

# Challenges

What went wrong, what things were difficult, how did you grow as a person?

# Lessons Learned

- Learn some things and stuff

# Areas to Improve

- What things could you explore in a new project?

# Resources Used

- A list of things
