# What is a Clean Architecture:
* The Domain is essential for a clean architecture
* Use-cases are essential
* The Domain and the use cases are essential; all dependencies points toward it
* Presentation is just a detail
* Persistence is just a detail

# Types of Domain-Centric Architectures
* Hexagonal Architecture where the application (use cases and domain) are central;
ports and adapters are plugged in the application.
* Onion Architecture is a layered application with domain in the center, surrounded by
the application layer. UI, Persistance and other infrastructure are the most outer layers
* Clean Architecture (by Uncle Bob) consists of Domain Entities in the center, surrounded 
by the uses cases; the other layer consists of ports and adapters.

# Positives of Domain-Centric Architecture
* Focuses on the Domain
* Less coupling between the domain logic and the infrastructure 
* Allows Domain-Driven Design

# Negatives on Domain-Centric Architecture
* It is not standard for newbie developers
* Requires more thought on what to do
* Higher Initial Cost, but it pays off if the application has a long life-cycle


# Commands in Terminal
The provided commands have been executed in a ZSH over a MacOs Ventura. However, they should run just fine in Bash or other Shells
on a Mac or Linux installation. In fact, they should also be executable in a Windows Command Prompt. No matter the operating system,
you should have `dotnet` installed and configured in the `PATH` variable.

You may need to run Unit Tests from the Terminal. To run all the tests from all the test projects, navigate to the solution root and run
```zsh
dotnet test --logger:"console;verbosity=detailed"
```

You will need to include different nugets when you develop your solution. To add a nuget reference, navigate to the project where
you need to add the reference and run
```zsh
add nuget packages {PACKAGE_NAME}
```

If you are making having a code-first approach to your persistance model, you will probably need to utilize EntityFramework Migrations.
This is easily done in Visual Studio on Windows, but for Visual Studio on Mac, or a Visual Studio Code on a Mac or Linux, you will need
to install EntityFramework Tools, run
```zsh
dotnet tool install --global dotnet-ef
```

Then you must add the tools to your PATH variable. If you are on a Mac with a ZSH, run
```zsh
cat << \EOF >> ~/.zprofile
```

You will now add lines to your ZSH Profile, until a new line with an `EOF` is typed in the Terminal. Replace `{YOUR_USERNAME}` with 
the username you are logging into your Mac.
```zsh
# Add .NET Core SDK Tools
export PATH="$PATH:/Users/{YOUR_USERNAME}/.dotnet/tools"
EOF
```
The above will vary if you are using a Linux, and it will vary between different distros. You should check how to set it up in Learn @ Microsoft
or StackOverflow.

Afterward, you will need to open a new instance of the Terminal or reload the current environment variables. For a ZSH on a Mac, execute
```zsh
zsh -l
```

Two different projects will need to be differentiated. 
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