# Wilson Towers Watson

## Developer Interview Problem  

#### Pete Field

### Description
This is a dotnet core console application that allows users to specify the path to a file. The contents of the file are then transformed from an incremental tabular form into a text representaion of a claims traingle.

This is a .net core 2.1 app.

This has been tested on Windows 10.

### Build

Ensure the latest dotnet core 2.1 sdk is installed on your machine 'https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial'

1) Unzip, download or clone this repo and open a command prompt into the */src* directory

2) Run `dotnet build`

### Run

The path to the file to be transformed can be provided via the commandline or, if no such path is specified, by entering it when prompted by the app,

1) Change directory in to *WTW.ClaimsReservation.Console*

2) run `dotnet run ../../documents/sampledata/example/input.csv`

  this will use the example data provided in the test description as an input and output the results file to 
  
  *../../documents/sampledata/example/input_output.csv*
