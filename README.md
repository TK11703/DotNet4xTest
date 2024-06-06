# .NET 4.8 SQL Connection Test Project
## Overview
This project was built to test or troubleshoot ASP.Net 4.8 web applications and the connection to the configured SQL database. There are two primary tests used here. A test can be executed as the assigned web identity, or a test can be executed as an authenticated user to the application.

## Prerequisites
- A SQL database instance, which could be a stand alone SQL server, an Azure SQL Database instance, or an Azure SQL Managed Instance.
- Configured identity for your deployed web application (IIS Application Pool or an Azure Managed Identity).
- Connection string for your SQL Database.
