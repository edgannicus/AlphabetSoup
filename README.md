# AlphabetSoup
This is a simple API that allows users to find words in a matrix of letters, searching both horizontally and vertically.

Receives a matrix and the words that needs to be found in there.

## Prerequisites

Before you begin, ensure you have the following installed:
- [.NET 8.0] higher
- [Postman](https://www.postman.com/downloads/) (optional, for testing)

## Installation

1. Clone this repository to your local machine, and start testing it via swagger or Postman.
2. Build the solution: dotnet build
3. Run the API in local: dotnet run --project WordFinderAPI

## API Endpoints

POST /api/WordFinder/find-words


Finds words in the given matrix. It searches horizontally and vertically for matches.

Request Body Example
{
    "matrix": [
        "EXAMPLE",
        "WEGLOHPN",
        "WORLDREQ",
        "TESTYYLR",
        "XABCDELG",
        "YFGHOSER"
    ],
    "wordStream": [
        "HELLO", "EXAMPLE", "TEST", "YES"
    ]
}

Expected response 

{
    "wordsFound": ["EXAMPLE", "TEST", "YES"]
}

## Running Tests

Unit Test

You can run the unit tests with the following command:
dotnet test