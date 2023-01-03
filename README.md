# Vitastic

[![License](https://img.shields.io/github/license/hur1can3/Vitastic.svg)](https://github.com/hur1can3/Vitastic/blob/main/LICENSE.txt)
[![Build Status](https://img.shields.io/azure-devops/build/hur1can3/VoidCore/18.svg)](https://dev.azure.com/hur1can3/VoidCore/_build/latest?definitionId=18&branchName=main)
[![Test Coverage](https://img.shields.io/azure-devops/coverage/hur1can3/VoidCore/18.svg)](https://dev.azure.com/hur1can3/VoidCore/_build/latest?definitionId=18&branchName=main)
[![ReleaseVersion](https://img.shields.io/github/release/hur1can3/Vitastic.svg)](https://github.com/hur1can3/Vitastic/releases)

A web application for managing recipes.

Vitastic is based on ASP.NET Core 7 with minimal api and Vue.js 3.


## Features

* Printer-friendly views.
* Category tags on recipes.
* Multi-image uploads for recipes.
* Copy recipes.
* Text search on recipe names and categories.
* Recent history list.
* Full logging on the server.
* Bootstrap-Vue UI.
* See screenshots [here](docs/screenshots.md).


## Build and Run Vitastic

### Make a Database

1. Build a SQL Server database by running the migration scripts in /build/sql in order by date.

### Local build (production and development)

Install the following tools:

* [.NET SDK](https://www.microsoft.com/net/download)
* [Node](https://nodejs.org/en/)
* [Yarn](https://yarnpkg.com/)

See the /build folder for scripts used to test and build this project.

There are [VSCode](https://code.visualstudio.com/) tasks for each script. The build task (ctrl + shift + b) performs the standard CI build.

Run build.ps1 to make a production build.

```powershell
./build/build.ps1
```

Use the deployment/setup scripts as templates to deploy to your environment.

```powershell
./build/setupWebServer.ps1

./build/deployAppToProduction.ps1
```

### Docker multi-stage build

You don't need .NET or Node locally to run this application in [Docker](https://www.docker.com/).

```powershell
docker build
```
