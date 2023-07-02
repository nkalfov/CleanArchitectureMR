[![.NET](https://github.com/nkalfov/CleanArchitectureMR/actions/workflows/dotnet.yml/badge.svg)](https://github.com/nkalfov/CleanArchitectureMR/actions/workflows/dotnet.yml)

# Abstract
The repository is an extended example of a Clean Architecture Solution build on the base example of Matthew Renze on Pluralsight. The solution will be developed until it gets to a state, which I would consider as complete. I Plan to implement additional features not found in the base example; details of the solution are already different. For example, I have removed to automatic database creation and lazy loading and have started using database migrations. The structure has changes as well; the Unit Tests are organized in their own solutions, the Domain is in a single folder, etc. 

I find Mathew's approach more suitable for small projects, where time to delivery is more critical, compared to other Clean Architecture Examples. 

Ideologically, the manifested Command-Query Segregation will differ from the original _Gang-of-Four_ by having the command return a result. This is a valuable tradeoff to keep the solution simple.


# Environment
The repository relies on PostgreSQL to store its data, compared to the common SQL Server. I do consider PostgreSQL as a more feature-rich database, which is quite extensible in additional configuration. Another reason I have chosen PostgreSQL is because I have originally started working on the application in a GNU/Linux installation and have later moved on MacOS Ventura. 

The solution is developed mostly in Visual Studio for Mac with .NET 6 (LTS), ASP.NET Core and EntityFrameworkCore 6. The Unit Tests are using XUnit Framework.


## PostgreSQL
I have installed PostgreSQL 14 with Homebrew. To do the same, simple run in the Terminal:
```zsh
brew install postgresql@14
```

I don't like having services run when I don't need them and for this reason I am starting the database when I need it and stop it when I don't. To do the same, you can create two command files - one to start and one to stop the database engine. 

Create a file to store the first command, by executing in the Terminal:
```zsh
touch postgres.start.command
``` 

Next open the command with `nano`:
```zsh
nano postgres.start.command
```

Paste in the file the following text:
```shell
#! /bin/bash
brew services start postgresql@14
```

Save the content by pressing `control+X` and make the file executable:
```zsh
chmod u+x postgres.start.command
```

You can now execute the command from the Terminal or by double-clicking it from Finder. 

At some point you may need to stop the database engine. This can also be done from a command file! Let's start with creating the file itself:
```zsh
touch postgres.stop.command
```

Open the file with `nano`:
```zsh
nano postgres.stop.command
```

Paste the following text inside the file:
```shell
#! /bin/bash
brew services stop postgresql@14
```

Save the content (press `control+X`) and make the file executable:
```zsh
chmod u+x postgres.stop.command
```


## .NET 6
If you have installed Visual Studio for Mac, you should already have `dotnet` available in the Terminal. The solution would need at least .NET 6 installed (or newer), You can verify by executing:
```zsh
dotnet --info
```

Which should produce an output, similar too:
```shell
.NET SDK:
 Version:   7.0.202
 Commit:    6c74320bc3

Runtime Environment:
 OS Name:     Mac OS X
 OS Version:  13.3
 OS Platform: Darwin
 RID:         osx.13-x64
 Base Path:   /usr/local/share/dotnet/sdk/7.0.202/

Host:
  Version:      7.0.4
  Architecture: x64
  Commit:       0a396acafe

.NET SDKs installed:
  6.0.406 [/usr/local/share/dotnet/sdk]
  6.0.407 [/usr/local/share/dotnet/sdk]
  7.0.200 [/usr/local/share/dotnet/sdk]
  7.0.202 [/usr/local/share/dotnet/sdk]

.NET runtimes installed:
  Microsoft.AspNetCore.App 6.0.14 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 6.0.15 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 7.0.3 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 7.0.4 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 6.0.14 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 6.0.15 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 7.0.3 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 7.0.4 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]

Other architectures found:
  None

Environment variables:
  Not set

global.json file:
  Not found

Learn more:
  https://aka.ms/dotnet/info

Download .NET:
  https://aka.ms/dotnet/download
```

If you don't have a similar output, then please get Visual Studio for Mac. Alternatively, you can use JetBrains Rider or Visual Studio Code. If you do use VS Code, then download the auto-suggested plugins when you open the solution with it. 


### .NET 6 on a Debian-based GNU/Linux (Ubuntu)
If you are on a Debian-based GNU/Linux, you can acquire .NET by running in the Terminal:
```bash
sudo apt update && sudo apt -y install dotnet-sdk-6.0 aspnetcore-runtime-6.0
```

If you are on an Ubuntu and have instead installed .NET from PMC (`packages.microsoft.com`), you may encounter the error [A fatal error occurred. The folder [/usr/share/dotnet/host/fxr] does not exist](https://stackoverflow.com/questions/73753672/a-fatal-error-occurred-the-folder-usr-share-dotnet-host-fxr-does-not-exist). In this case, consider uninstalling the PMC packages and reverting back to the jammy feed.

The easiest way to install Visual Studio Code is to navigate to the official [VS Code Download Page](https://code.visualstudio.com/Download#) and find the appropriate package for you. Download VS Code `.deb` package in you home downloads folder, `~/Downloads`. 

Open a terminal, navigate to your downloads folder and use `apt` to install VS Code. Replace `<file>` with the name of the `.deb` package you have just downloaded.

```console
cd ~/Downloads
sudo apt install ./<file>.deb
```

You are almost good to go and start coding whatever awesome applications you can think of. You just need to add a single extension to VSCode, [C# for Visual Studio Code (powered by OmniSharp)](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp). 

Open Visual Studio Code, press `Ctrl+P` and type 
```
ext install ms-dotnettools.csharp
```


## Entity Framework Tool
The solution uses a code-first approach over its persistance model and uses Database Migrations, which are found in Entity Framework Tool. Open the terminal and execute:
```zsh
dotnet tool install --global dotnet-ef
```

Then you must add the tools to your PATH variable:
```zsh
cat << \EOF >> ~/.zprofile
```

You will now add lines to your ZSH Profile, until a new line marked with an `EOF` is typed in the Terminal. Replace `{YOUR_USERNAME}` with 
the username you are logging into your Mac.
```zsh
# Add .NET Core SDK Tools
export PATH="$PATH:/Users/{YOUR_USERNAME}/.dotnet/tools"
EOF
```
The above will vary if you are using a GNU/Linux, and it will vary between different distributions. You should check how to set it up in Learn @ Microsoft or StackOverflow.

Afterward, you will need to open a new instance of the Terminal or reload the current environment variables:
```zsh
zsh -l
```

Entity Framework Tool needs to know about two different projects in the solution.
* The first project is the startup project used to start your solution. In the case of this repo, the project is 
`src/CleanArchitecture.Presentation`. In the `appsettings.json` of this project you should have the connection string to the database, where
 you will persist the data the solution utilized. This project will be passed to EF-Tool as the `--startup-project` argument. For this 
 reason, it must contain a reference to `Microsoft.EntityFrameworkCore.Design`. The whole solution is developed in .NET 6, and in the time
 of this writing, the most up-to-date version is `6.0.15`. 
 ```zsh
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.15
 ```

* The second project is where you have the persistance model details; in the context of this solution, the project is `src/CleanArchitecture.
Persistance`. The project will be passed to EF-Tool as the `--project` argument. This will be enough for the EF-Tool to read the necessary 
configuration and generate a migration.

To create your first migration, navigate to the solution root and run
```zsh
dotnet ef migrations add InitialCreate --startup-project src/CleanArchitecture.Presentation --project src/CleanArchitecture.Persistance
```

The command will create a folder, called `Migrations` in `src/CleanArchitecture.Persistance`, which will contain all the migrations. To update
the database with the latest migrations, execute
```zsh
dotnet ef database update --startup-project src/CleanArchitecture.Presentation --project src/CleanArchitecture.Persistance
```


## Running Unit Tests
You can run the Unit Tests from your IDE (VS or Rider), or you can do it with VS Code extensions; another way to run the unit tests is from the Terminal. Considering you are over the solution root, run:
```zsh
dotnet test --logger:"console;verbosity=detailed"
```

