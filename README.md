# Lightning Strike Data Processing And Alert System Proposal
This is a solution for processing high-traffic lightning strikes data and creating notifications when a strike happens within a region of an asset.
It has both API to be used publicly or HostedService to consume and process large files. 
Also the solution supports persistence for both asset info and lightning strike records. 
It's extensible with the dispatcher mechanism that supports application events.

## Technologies
ASP.NET Core 5
Entity Framework Core 5
FluentValidation
Serilog
xUnit, FluentAssertions, Bogus

## Getting Started
The easiest way to get started is to follow these instructions to get the project up and running:

### Prerequisites
You will need the following tools:

* [JetBrains Rider](https://www.jetbrains.com/rider/) (version 2021.1.5 or later) or [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.10.4 or later)
* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)

### Setup
Follow these steps to get your development environment set up:
1. Clone the repository
2. At the root directory, restore required packages by running: 
   ``` 
   dotnet restore 
   ```
3. Next, build the solution by running: 
   ``` 
   dotnet build 
   ```
4. Once the solution has been built, within the `\src\HostedService` directory, launch the hosted service by running:
   ```
   dotnet run
   ```
5. Alternatively, API can be started within the `\src\API` directory, by running:
   ```
   dotnet run
   ```
6. Sample curl command can be used to test after running the API project:
   ```
   curl --location --request POST 'http://localhost:5000/lightning-strikes' 
   --header 'Content-Type: application/json'
   --data-raw '{ 
        "flashType": 1, 
        "strikeTime": 1446760902510, 
        "latitude": 8.7020156,
        "longitude": -12.2736188, 
        "peakAmps": 3034, 
        "reserved": "000", 
        "icHeight": 11829,
        "receivedTime": 1446760915181,
        "numberOfSensors": 6,
        "multiplicity": 1
   }'
   ```
   
## Overview

### Domain
This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application
This layer contains all application logic. 
It is dependent on the domain layer, but has no dependencies on any other layer or project. 
This layer defines interfaces that are implemented by outside layers. 
For example; if the application need to access another service, a new interface would be added to application and an implementation would be created within the infrastructure.

### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. 
These classes should be based on interfaces defined within the application layer.

### API
This layer is a web api application based on ASP.NET Core 5. 
This layer depends on both the Application and Infrastructure layers, however, the dependency on Application is only to support dependency injection.

### HostedService
This project is not a conventional layer of a 'clean architecture' approach. 
However, the purpose of the project is to provide a mechanism to mimic, for example sensors or other IOT devices and demonstrate the behavior of the application/services under high-traffic requests.
This service is configurable.