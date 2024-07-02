# .NET 4.8 Test Project
## Overview
This project was built to test or troubleshoot ASP.Net 4.8 web applications, and the configuration of them when deployed. One major item is to test the connection to the configured SQL database. There are two primary tests used here. A test can be executed as the assigned web identity, or a test can be executed as an authenticated user to the application. Another test helps validate the PDF display options within your app and a browser. These tests work with a ASPX page or an HttpHandler. Finally, with authentication enabled through OWIN and an Entra tenant, there are some tests that occur for authentication and utilization of the Grap API.

## Prerequisites
- A SQL database instance, which could be a stand alone SQL server, an Azure SQL Database instance, or an Azure SQL Managed Instance.
- Configured identity for your deployed web application (IIS Application Pool or an Azure Managed Identity).
- Connection string for your SQL Database.
- Entra tenant, application registration to include a client ID and a secret.

## Versions
- **Version 1:** This version represents base functionality of connecting to a database and using windows authentication (integrated) within the environment, whether that is a local development machine or a deployment into a web server. Windows auth can be turned off within the web.config or IIS settings, and this version will function with or without a logged in user. However, the proper database permissions will need to be granted to enable the application pool or authenticated user identity to connect.
- **Version 1.1:** This version introduced the OWIN authentication code to enable login with Entra.
- **Version 1.2:** Additional tests were introduced. In addition the OWIN code was redone based on some MSFT Azure repos, due to complexity and time debugging issues with current code. Introduced new code to handle the graph API calls.
