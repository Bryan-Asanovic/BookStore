# Bookstore Application

Welcome to the **Bookstore Application**! This project is a web application built with **C#** and **ASP.NET MVC** that allows users to manage a collection of books. You can easily add, edit, and delete books in the system, making it a great starting point for learning about CRUD (Create, Read, Update, Delete) operations in ASP.NET MVC.

## Features
- **Add Books:** Add new books to the collection with ease.
- **Edit Books:** Update the details of existing books.
- **Delete Books:** Remove books from the collection.

## Project Structure
The project is organized into several folders for better maintainability:

### 1. `BookStore.DataAccess`
Contains all data access logic, including the code that connects to the database.

### 2. `BookStore.Models`
Defines the data models used in the application, such as the `Book` model.

### 3. `BookStore.Utility`
Contains utility classes and helpers used throughout the project.

### 4. `MyBookStore`
Holds the main web application, including controllers and views for managing books.

### Other Files
- **`BookStore.sln`**: The solution file for the project.
- **`.gitattributes` & `.gitignore`**: Git configuration files.
- **`README.md`**: This file, providing an overview of the project.

## Getting Started
Follow these steps to set up and run the project locally:

### Prerequisites
- **.NET SDK**: Install the latest version of the .NET SDK from [here](https://dotnet.microsoft.com/download).
- **IDE**: Use an IDE like Visual Studio or Visual Studio Code.

### Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/Bryan-Asanovic/Bookstore-Application.git
   ```
2. Open the solution file `BookStore.sln` in Visual Studio.
3. Update the connection string in `appsettings.json` to point to your database.
4. Run database migrations (if applicable) to set up the database schema.
5. Build and run the application:
   ```bash
   dotnet run
   ```

### Usage
- Open the application in your browser.
- Use the provided interface to add, edit, or delete books.

---

Feel free to customize this README further to fit your project’s needs!

