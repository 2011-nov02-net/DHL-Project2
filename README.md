# Course Portal

## Project Description

Course Portal is an application that a school could use for managing everything related to a school's set of courses.  It allows for a new student to sign up, view their current enrollment, add courses to their enrollment, and view transcripts.  It also supports management of the courses, such as reserving a building, room, and time that the course will be held in,  grading of the courses by instructors of students currently enrolled, and adding/removing course offerings. This application will help with the complex day to day organization and management of the courses that the school offers.

## Technologies Used

* C# - version 9.0
* .NET framework - version 5.0
* ASP.NET API - version 5.0
* Angular - 11.0.5
* Angular Material - 11.0.3
* T-SQL (Azure SQL) - 15.0.2000.5
* EntityFramework Core - 5.0

## Features

List of features ready and TODOs for future development
* Login by Email to view enrolled courses
* Enroll in a course
* Change a user's email and name
* Create a new user 

To-do list:
* Okta Authentication Login
* Add Angular component for instructors assign grades to students
* Add Angular component for instrctors to enroll a student of the waitlist

## Getting Started

Prerequisites:

[Install .Net](https://docs.microsoft.com/en-us/dotnet/core/install/)
Install Microsoft SQL Server or otherwise get a ADO.NET connection string to a remote SQL Server.

```
git clone https://github.com/2011-nov02-net/DHL-Project2.git
cd DHL-Project2
dotnet run -p ./Project2.Api
```

## Usage

This project can be run locally, but is intended to be deployed to Azure App Service. To run locally set the port number in the `appsettings.json`. The application is build on top of database first entityframework, so the `*schema.sql` in the datamodel folder must be run before starting the server, and the application needs the ADO.NET connection string to be named `Project2connection` in the `appsettings.json` or `appsettings.Development.json` in the `Project2.Api` folder. The rest api server can be started in with `dotnet run -p ./Project2.Api`.

## Contributors

* David Barnes
* Luke Fisher
* Har'el Fishbein

## License

This project uses the following license: [MIT License](https://opensource.org/licenses/MIT).

[![Board Status](https://dev.azure.com/2011-Revature-Project2/0ca1e1e0-6d85-4309-8693-a63578c346e0/4aa37e0a-9cec-461c-9d30-01574428975e/_apis/work/boardbadge/da1df55a-1c6d-493e-bed6-e7014ce5ef9d)](https://dev.azure.com/2011-Revature-Project2/0ca1e1e0-6d85-4309-8693-a63578c346e0/_boards/board/t/4aa37e0a-9cec-461c-9d30-01574428975e/Microsoft.EpicCategory/)

[![Build Status](https://dev.azure.com/2011-Revature-Project2/2011-Revature-Project2/_apis/build/status/2011-nov02-net.DHL-Project2?branchName=master)](https://dev.azure.com/2011-Revature-Project2/2011-Revature-Project2/_build/latest?definitionId=2&branchName=master)

[![Deployment Status](https://vsrm.dev.azure.com/2011-Revature-Project2/_apis/public/Release/badge/0ca1e1e0-6d85-4309-8693-a63578c346e0/2/3)](https://dev.azure.com/2011-Revature-Project2/2011-Revature-Project2/_release?view=mine&_a=releases&definitionId=2)

[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=2011-nov02-net_DHL-Project2&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=2011-nov02-net_DHL-Project2)

[Project_2_Proposal.gdoc](https://docs.google.com/document/d/1SUtmGZhvOQ8VzlGmNKQhKBRXFrF6DCC7mK_G612JgCM/edit?usp=sharing)

[Project 2 User Stories.gdoc](https://docs.google.com/document/d/1r4kRdUUC9NR1ERdfVNyVslxUc3c1wagCKCZLz6pIRFs/edit?usp=sharing)

[DHL-Project2-DB-Design.drawio](https://drive.google.com/file/d/1vbZ_E7XsWTnbINBwabt1uGdYjkSsvt09/view?usp=sharing)

[project2-db-diagram2(harelfishbein)](https://dbdiagram.io/d/5fd67b429a6c525a03bae5d2)
