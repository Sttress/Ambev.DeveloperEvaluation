[![Build status](https://ci.appveyor.com/api/projects/status/4ua0x7oxsqqfx6kq?svg=true)](https://ci.appveyor.com/project/Britz/abi-gth-omnia-developer-evaluation)
[![codecov](https://codecov.io/gh/gBritz/abi-gth-omnia-developer-evaluation/graph/badge.svg?token=63H7JZ296O)](https://codecov.io/gh/gBritz/abi-gth-omnia-developer-evaluation)

<a href="https://app.aikido.dev/audit-report/external/IMQ1OonvClmSAXC0xooBTD2P/request" target="_blank">
    <img src="https://app.aikido.dev/assets/badges/full-light-theme.svg" alt="Aikido Security Audit Report" height="40" />    
</a>

# Ambev Challenge - Shopping Cart Management API

A development test for evaluation in a selection process.
Observing skills in software development, code organization, best practices, and technical problem-solving.

## Getting Started
# Introduction to .NET 8

## Prerequisites

Before you begin, make sure you have the .NET 8 SDK installed on your machine. You can check this by running the following command in your terminal:

```bash
dotnet --version
You should see a version that starts with 8.. Additionally, ensure that the necessary Entity Framework Core packages are installed in your project.

Updating Migrations
When you make changes to your data model, it's important to apply those changes to your database. This can be done through the migration update command, which applies all pending migrations to the database.

Command to Update Migrations
To apply all pending migrations to your database, use the following command:

CopyReplit
dotnet ef database update --project C:\Users\Lucaz\Desktop\Work\Tests\template\backend\src\Ambev.DeveloperEvaluation.ORM\Ambev.DeveloperEvaluation.ORM.csproj --startup-project C:\Users\Lucaz\Desktop\Work\Tests\template\backend\src\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj

Command Parameters
--project: This parameter specifies the path to the project where the DbContext configuration is located. In this case, it is:
Copy
C:\Users\Lucaz\Desktop\Work\Tests\template\backend\src\Ambev.DeveloperEvaluation.ORM\Ambev.DeveloperEvaluation.ORM.csproj
--startup-project: This parameter indicates the path of the project that should be used to run the application. Normally, this is the Web API project:
Copy
C:\Users\Lucaz\Desktop\Work\Tests\template\backend\src\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj
Example Command
CopyReplit

dotnet ef database update --project C:\Users\Lucaz\Desktop\Work\Tests\template\backend\src\Ambev.DeveloperEvaluation.ORM\Ambev.DeveloperEvaluation.ORM.csproj --startup-project C:\Users\Lucaz\Desktop\Work\Tests\template\backend\src\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj
Checking Applied Migrations
To view all the migrations that have been applied to your database, you can run the following command:

CopyReplit
dotnet ef migrations list --project C:\Users\Lucaz\Desktop\Work\Tests\template\backend\src\Ambev.DeveloperEvaluation.ORM\Ambev.DeveloperEvaluation.ORM.csproj
This will display a list of all migrations and their status.

Conclusion
You are now ready to update migrations in your .NET 8 application using Entity Framework Core!

Copy

### Notes

- You can further customize this documentation to include practical examples or additional steps if necessary.
- Feel free to expand this documentation to include information about creating new migrations, testing migrations in development or production environments, among other relevant topics.

## Instructions
**The test below will have up to 7 calendar days to be delivered from the date of receipt of this manual.**

- The code must be versioned in a public Github repository and a link must be sent for evaluation once completed
- Upload this template to your repository and start working from it
- Read the instructions carefully and make sure all requirements are being addressed
- The repository must provide instructions on how to configure, execute and test the project
- Documentation and overall organization will also be taken into consideration

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

## Overview
This section provides a high-level overview of the project and the various skills and competencies it aims to assess for developer candidates. 

See [Overview](/.docs/overview.md)


## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.docs/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.docs/frameworks.md)

## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](/.docs/general-api.md)
- [Products API](/.docs/products-api.md)
- [Carts API](/.docs/carts-api.md)
- [Users API](/.docs/users-api.md)
- [Auth API](/.docs/auth-api.md)

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.docs/project-structure.md)
