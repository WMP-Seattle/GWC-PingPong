# GWC-PingPong
Girls Who Code - Ping Pong App

This branch contains the skeleton code for GWC.
It will be used for the perpose of teaching coding skills and best practices.

Within the project there are skeleton functions that will assist in completing the project.

## Install

1. Install .Net Core SDK 1.0.4. https://www.microsoft.com/net/core#windowscmd
2. Recommended IED - Visual Studio Code https://code.visualstudio.com/
    - Code is not required but is recommend for quick start up of the project.
    - Install the C# extention for VS Code. https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp
3. Install git https://git-scm.com/downloads.
4. Clone the github. https://github.com/WMP-Seattle/GWC-PingPong.git.
    1. navigate/create folder to hold project.
    2. run `git init`
    3. run `git clone https://github.com/WMP-Seattle/GWC-PingPong.git`
5. In a terminal/command prompt. (recommend VS Code's integrated terminal)
    1. Navigate to where you cloned the project, be sure to be in \PingPong\ where the PingPong.csproj file is.
    2. run `dotnet restore`
    3. run `dotnet ef database update`
6. Run the solution.
    - Within VS Code hit F5.
    - or run `dotnet run`
    - webapp will be hosted at http://localhost:5000