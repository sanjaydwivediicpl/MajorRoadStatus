# ProjectInfo
Major Road Status is checked using this application. Its built using .net core 3.1 and used NUnit for unit test cases

# Approach
Followed TDD and AAA for tests

builder pattern to call Api using httpclient

Dependency injection pattern for loosely coupled architecure.

Solid principles

MajorRoadStatus - Console application to receive the input

MajorRoadStatusSystem - Main system for retrieving the status of major roads

MajorRoadStatusTest - Unit test solution

# Prerequisite
Install VS2019 Professional or community edition.
Install Powershell.

# AppId and AppKey
There are two json configs used in the solution.One in the main project and another in test project.
MajorRoadStatus - AppSettings.json
MajorRoadStatusTest - AppSettings_Test.json

Please update the values in solution and published folder.

# Assumptions
Deployment mode as Frameworkdependent

git available

# How to build the code
1. Clone the repository using GitBash/Powershell from Github
2. https://github.com/sanjaydwivediicpl/MajorRoadStatus
3. Open the .sln file using Visual Studio 2019
4. Press on Ctrl+Shift+B to build the code.

o	How to run the output
1. Run MajorRoadStatus application in visual studio
2. This will open console window taking A2 as initial input
   
   To run application - ./MajorRoadStatus A2
   In powershell, C:\publish> ./MajorRoadStatus.exe A4   
   The Status of the A4 is as follows   
   Road Status is Closure   
   Road Status Description is Closure   
   To run tests - dotnet vstest MajorRoadStatusTest.dll
 
3. Run
    
    cd MajorRoadStatus\MajorRoadStatus\bin\Release\netcoreapp3.1
    
    ./MajorRoadStatus.exe A2
    
    Expected result
    
    PS C:\MajorRoadStatus\MajorRoadStatus\bin\Release\netcoreapp3.1> ./MajorRoadStatus.exe A2
    The Status of the A2 is as follows
    Road Status is Good
    Road Status Description is No Exceptional Delays
     
4. UnitTest
     
     cd \MajorRoadStatus\MajorRoadStatusTest\bin\Release\netcoreapp3.1
     
     dotnet vstest MajorRoadStatusTest.dll
  
    
   
   
