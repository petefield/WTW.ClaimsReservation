# Wilson Towers Watson

## Developer Interview Problem  

#### Pete Field

### Description
This is a dotnet core console application that allows users to specify the  path to a file name which is then transformed from an incremental tabular form into a text representaion of a claims traingle.

This is a .net core 2.1 app.

### Build
1) Ensure the latest dotnet core 2.1 sdk is installed on your machine 'https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial'

2) Unzip, download or clone this repo and cd to its root.

3) `cd ./src/WTW.ClaimsReservation.Console`

4)  'dotnet build'

### Run

The path to the file to be transformed can be provided via the commandline or, if no such path is specified, by entering it when prompted by the app,

1) `dotnet run ../../documents/sampledata/example/input.csv`
  this will use the example data provided in the test descripiton as an input and output the results file to '../../documents/sampledata/example/input_output.csv'
  
