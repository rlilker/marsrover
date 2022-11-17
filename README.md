# README #

A squad of robotic rovers are to be landed by NASA on a plateau on Mars. This plateau, which is curiously rectangular, must be navigated by the rovers so that their on-board cameras can get a complete map of the surrounding terrain to send back to Earth.

A rover's position and location are represented by a combination of x and y coordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and 'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its current spot. 'M' means move forward one grid point and maintain the same heading.
Assume that the square directly North from (x, y) is (x, y+1).

# Additional Information #

It is assumed that the first action is to define the upper-right coordinates (5, 5) of the Plateau.
Once completed, rover objects can be deployed within the plateau. Each rover should be able to take a series of commands following the simple letter commands outlined above.

In the test we will be providing a movements.csv, this file will outline each rover and it’s predefined movements, this information should be sent to the plateau to automate the process of mapping the surroundings.

Each rover should be sequential, meaning the second rover will only complete its tasks once the rover before it has finished moving.

Additional rovers can optionally be added with the ability to define both starting location and movements.
Each line in the movements.csv file represents an independent rover, these lines are then split by a pipe, on the left of the pipe is the rover starting position and on the right of the pipe is the rover's movements.

### Prerequisites and set up

For local development you will need to install:

- [DotNet 6] (https://dotnet.microsoft.com/en-us/download/dotnet/6.0) 
- [Node Js] + NPM (https://nodejs.org/en/download/)

To run the app for the first time, you will need to run:

- `npm install`
- `npm run build`
- `dotnet run`

### Dependencies

Dependencies used in this project are:

- [XUnit](https://xunit.net/) - For Back-End Unit Testing
- [ReportGenerator](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) - For Back-End Code Coverage Report Generation
- [Jest](https://jestjs.io/) - for Front-End Unit Testing
- [Rollup.js](https://rollupjs.org) - Module bundler to convert ES6 Module Javascript into a single file

### How to run tests

Mars Rover uses XUnit for back-end unit testing, and Jest for front-end unit testing.

In VS Code, there is an included launch.json to enable running and debugging the tests using the Run and Debug tool. 

To run the back-end tests in your CLI run `dotnet test` in the root directory, or `dotnet test --collect:"XPlat Code Coverage"` to output the Unit Test Coverage for the project. 

To Generate a HTML Code Coverage report, take the relative path output for the coverage.coberta.xml file and run `reportgenerator -reports:'./MarsRover_Library_Tests/TestResults/{guid}/coverage.cobertura.xml' -targetdir:'./MarsRover_Library_Tests/TestResults/CoverageReport' -reporttypes:'HTML';`

To run the front-end tests in your CLI run `npm test` in the root directory, or `npm test:coverage` to output the Unit Test Coverage for the project.

