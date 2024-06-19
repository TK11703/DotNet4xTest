# .NET 4.8 SQL Connection Test Project
## Overview
This project was built to test or troubleshoot ASP.Net 4.8 web applications and the connection to the configured SQL database. There are two primary tests used here. A test can be executed as the assigned web identity, or a test can be executed as an authenticated user to the application.

## Prerequisites
- A SQL database instance, which could be a stand alone SQL server, an Azure SQL Database instance, or an Azure SQL Managed Instance.
- Configured identity for your deployed web application (IIS Application Pool or an Azure Managed Identity).
- Connection string for your SQL Database.

## Versions
- **Version 1:** This version represents base functionality of connecting to a database and using windows authentication (integrated) within the environment, whether that is a local development machine or a deployment into a web server. Windows auth can be turned off within the web.config or IIS settings, and this version will function with or without a logged in user. However, the proper database permissions will need to be granted to enable the application pool or authenticated user identity to connect.
