
# Article Web Scraping Project

This project contains a C# application that performs web scraping of articles from the Dergipark website based on specific URLs and stores them into MongoDB. Additionally, it utilizes HtmlAgilityPack for processing HTML content, Entity Framework for accessing MongoDB, and Elasticsearch for indexing and searching articles.

Installation
Before running the project, follow these steps to install and configure the necessary dependencies:

MongoDB Installation and Configuration:

Download the latest version of MongoDB from the MongoDB website and install it.
Configure MongoDB as necessary (port settings, database name, etc.).
Elasticsearch Installation and Configuration:

Download the latest version of Elasticsearch from the Elasticsearch website and install it.
Configure Elasticsearch as necessary (port settings, index settings, etc.).
Install Required C# Packages:

Use the NuGet package manager to install the required C# packages for the project. You can install the necessary packages by using the following command:
## Run on Your Computer

Clone Project:

```bash
  git clone https://github.com/fatihserhatturan/Web_Scraper_UI.git
```

Switch to project directory

```bash
  cd ./Web_Scraper_UI
```

Install required packages

```bash
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package NLog
  dotnet restore
```

Run the server

```bash
  dotnet build
  dotnet run
```
Cleanup

After running the project, you can clean up unnecessary files and data from the database by following these steps:

Run the Cleanup Command:

Execute the following command in the terminal or command prompt to clean up the project:

  
## Used Technologies


- MongoDb ![MongoDb](https://brandfolder.com/mongodb/press-kit/#!asset/jv3gcw39g9hswvbwrvjc3qtj)
- ElasticSearch
- Html Agility Pack

## Contribution

Contributions are always welcome!

Please follow this project's code of conduct.


## Collaborators 
- [Fatih Serhat Turan](https://github.com/fatihserhatturan) 
- [Mustafa Surmeli](https://github.com/mustafasurmeli) 


  
