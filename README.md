# Stream-Processing

stream-processing is a .NET 5 application that processes an infinite stream and exposes statistics via API

## Installation
Clone the [Github Repository](https://github.com/taladari/stream-processing.git) to your computer

#### Windows
1. Make sure you have [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed on your computer (```dotnet --list-sdks```)
2. Navigate to the **StreamProcessing.API** folder using the command line
3. Run the command ```dotnet run``` in the command line
4. Navigate to https://localhost:5001/swagger in your browser

#### MacOS
1. Make sure you have .NET 5 SDK installed on your computer (```dotnet --list-sdks```)
    1. In case .NET 5 SDK is not installed - use [brew](https://brew.sh/) to install it (```brew install --cask dotnet-sdk```)
2. Navigate to the **StreamProcessing.API/generators** folder using the terminal
3. Run the command ```chmod +x generator-*```
4. Navigate back to the **StreamProcessing.API** (```cd ..```)
5. Run the command ```dotnet run``` in the terminal
6. Navigate to https://localhost:5001/swagger in your browser

#### Linux (Ubuntu)
1. Make sure you have .NET 5 SDK installed on your computer (```dotnet --list-sdks```)
    1. In case .NET 5 SDK is not installed - use snap to install it (```sudo snap install --classic dotnet-sdk```)
2. Navigate to the **StreamProcessing.API/generators** folder using the terminal
3. Run the command ```chmod +x generator-*```
4. Navigate back to the **StreamProcessing.API** (```cd ..```)
5. Run the command ```dotnet run``` in the terminal
6. Navigate to https://localhost:5001/swagger in your browser

## Usage

Use the swagger on https://localhost:50001/swagger or any other way to do HTTP requests in order to retrieve statistics from processing the infinite stream
1. ```/statistics/event-types GET``` - count of events by type
2. ```/statistics/words GET``` - count of words appearances

## Improvement Notes
1. **Synchronization** - At the moment if we scale the application and run multiple processors (stream readers) we might have unhandled concurrency issues while updating the statistics in the database
2. **Separation** - The processor module could be separated to an entirely different service in order to be more scalable
3. **Dedicated framework** - Usage of a reactive programming framework would have improved the code and the processing pipeline (more time to study the concept of reactive programming would have helped with this)
4. **Cleanup** - The application does not entriely clean after itself - the generator executable is not being shut down properly