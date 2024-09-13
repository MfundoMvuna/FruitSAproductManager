 

FruitSA Product Manager 

Overview 

This is a .NET 7-based web application for managing products, categories, and other relevant features. It includes functionality such as image uploading, category management, and product code auto-generation. 

Prerequisites 

Before you begin, ensure you have the following installed: 

.NET 7 SDK 

SQL Server or SQL Server Express 

Visual Studio 2022 (or another suitable IDE) 

SendGrid Account and API Key (for email functionality) 

Git (if cloning the repository) 

Clone the Repository 

To clone the project, run the following command in your terminal: 

Bash -  

git clone https://github.com/MfundoMvuna/FruitSAproductManager.git 

 

Navigate to the project directory: 

Bash - 

cd FruitSAproductManager 

 -------------------------

Configuration 

1. Database Setup 

Make sure you have a SQL Server instance running and create a new database for this project. 

Modify the appsettings.json file in the root of the project to include your SQL Server connection string. Replace YourConnectionString with the actual connection string for your database: 

Json: 

"ConnectionStrings": { 

  "DefaultConnection": "YourConnectionString" 

} 

-------------------------

2. Apply Migrations 

To set up the database schema, apply the Entity Framework Core migrations for the (ApplicationDbContext) context: the application database.
 - Apply Migrations to the Application Database Context
The application context is responsible for managing the app's data (e.g., Products, Categories). Run the following command to apply the migrations for this context:

- dotnet CLI:
Bash: 
dotnet ef database update --context ApplicationDbContext

- PMC (Package Manager Console):
powershell:
Update-Database -Context ApplicationDbContext

This will apply the migrations and create the database structure based on the models. 

-------------------------

3. Seed Data (Optional) 

If you'd like to seed the database with initial data, you can modify the SeedData class in the project to include your preferred data. Then run the application, and the seed logic will populate the tables. 

 -------------------------

4. Configure SendGrid (For Email Functionality)  - Updated - not in appsettings.json

    - To enable email functionality (e.g., password resets), follow these steps to configure SendGrid securely:

    1 Sign up at SendGrid and obtain your API key.

    2 Store your SendGrid API key securely:

    - Using Visual Studio Secret Manager:
     - Open a command prompt or terminal in the root directory of your project.

     - Run the following command to store your SendGrid API key:

     - bash:
     - dotnet user-secrets set "SendGrid:ApiKey" "Your_SendGrid_API_Key"

     - This command securely stores your API key in your local user secrets store, which is not included in the source code or configuration files.

  3 Ensure that the EmailSender service is registered in the DI container. For example:
     - builder.Services.AddTransient<IEmailSender, EmailSender>();

-------------------------

5. Running the Application 

Once everything is configured, you can run the application with: 

Bash: 

dotnet run 

 

 

Alternatively, open the project in Visual Studio and press F5 to build and run. 

Features 

Product Management: Add, edit, and delete products. Upload product images. 

Category Management: Manage categories, including updating all associated products when a category is edited. 

Auto-Generated Product Codes: Automatically generates product codes based on the year and month. 

Image Upload: Upload and update product images. 

Email Notifications: Send email notifications using SendGrid. 

 

 

Troubleshooting 

If you run into any issues, check the following: 

Ensure the database connection string is correct and the database is accessible. 

Ensure migrations have been applied correctly by running dotnet ef database update. 

Ensure that the SendGrid API key is valid. 

Contributions 

If you'd like to contribute to this project, feel free to submit issues or pull requests. Feedback and suggestions are always welcome! 

License 

This project is licensed under the MIT License - see the LICENSE file for details. 

 
