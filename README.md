 

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

 

Configuration 

1. Database Setup 

Make sure you have a SQL Server instance running and create a new database for this project. 

Modify the appsettings.json file in the root of the project to include your SQL Server connection string. Replace YourConnectionString with the actual connection string for your database: 

Json: 

"ConnectionStrings": { 

  "DefaultConnection": "YourConnectionString" 

} 

2. Apply Migrations 

Open a terminal in the project directory and apply the Entity Framework Core migrations to create the necessary database tables: 

Bash: 

dotnet ef database update 

 

This will apply the migrations and create the database structure based on the models. 

 

3. Seed Data (Optional) 

If you'd like to seed the database with initial data, you can modify the SeedData class in the project to include your preferred data. Then run the application, and the seed logic will populate the tables. 

 

4. Configure SendGrid (For Email Functionality) 

To enable email functionality (e.g., password resets), set up your SendGrid API key: 

Sign up at SendGrid and get your API key. 

In the appsettings.json, add your SendGrid API key as follows: 

Json: 

"SendGrid": { 

  "ApiKey": "Your_SendGrid_API_Key" 

} 

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

 
