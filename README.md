# Braz's Tours – Travel Agency Web Application

A full-stack travel agency web application developed as a 2nd Semester 
project for the Web Design module at Atlantic Technological University 
(ATU), Sligo, Ireland.

## About the Project

Braz's Tours is a fictional Irish travel agency website that allows 
users to browse tour packages across Ireland and submit booking 
requests. The application was built using the ASP.NET MVC architecture 
with a SQL Server database connected via Entity Framework.

## Features

- Home page with agency overview
- 5 individual tour pages with embedded YouTube videos:
  - Cliffs of Moher
  - Giants Causeway
  - Malahide Castle
  - Newgrange
  - Wicklow Mountains
- Photo gallery page
- Booking form (Create, Edit, Delete and View bookings)
- Contact via email button in footer across all pages
- Custom error pages (400 and 404)

## Technologies Used

- C# / ASP.NET MVC 5
- Entity Framework 6
- SQL Server Express
- HTML / CSS / JavaScript
- Razor Views (.cshtml)
- .NET Framework 4.7.2

## Project Structure

- Controllers/ — BookingsController, ToursController, 
  GalleryController, HomePageController and ErrorController
- Views/ — Razor templates for each page and shared layouts
- Models/ — Entity Framework data models
- CSS/ and JavaScript/ — Front-end assets
- App_Start/ — Route and filter configurations

## Database

The file `T-SQL_BrazsToursTravelAgencyDB.sql` contains the full 
database schema and data for this project, including tables for 
Bookings, AgeGroup, Gender and PaymentMethod.

To restore the database:
1. Open SQL Server Management Studio (SSMS)
2. Run the script `T-SQL_BrazsToursTravelAgencyDB.sql`
3. Update the connection string in Web.config if needed

## Note on Images

The Content/ folder containing the project images has not been 
included in this repository for privacy reasons. The application 
structure, logic and all source code are fully available above.

## Academic Context

Developed at Atlantic Technological University (ATU), Sligo — 
BSc (Hons) Computer Science, 2nd Semester, Web Design Module.
