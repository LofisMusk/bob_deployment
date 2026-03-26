# bob (build orchestrator binary)
bob is a minimalist build orchestrator for Linux systems inspired by the KISS philosophy of Arch Linux and the simplicity of makepkg. It streamlines the process of fetching source code, reading necessary data from bob-config.json and compiling the project into ready-to-use binaries without the need to remember 
complex compiler flags for every different language.
## Features
* **Multi-language support:** native handling for compiled languages like C# and Rust
* **Git integration:** the `bob fetch` command automatically clones repositories directly into your workspace and builds it (if bob-config.json exists)
* **Minimal configuration:** a single `bob-config.json` file is all bob needs to understand your project structure
* **KISS philosophy:** lightweight, fast, and designed for the terminal
## How it works
If you want to build the project with bob instead of entering tens of compiler flags, you can do it with a simple command `bob build`. Firstly bob reads the data from bob-config.json which contains main project file name, compiler flags to use and output file location. Based on the main 
project file's extension bob picks the proper compiler to use. Then it checks if the compiler is installed in the system. After that bob begins to build the project based on the provided data from json file.

Example of a bob-config.json file for .NET project:
```
{
  "CompilerFlags": "-c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -v d",
  "MainFile": "bob.csproj",
  "OutputFile": "dist"
}
```
Based on that bob will run `dotnet publish bob.csproj -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -v d -o dist`. Note that if you leave the OutputFile field empty, bob will automatically specify the default name for the built binary.

**note:** bob is still in early development and many features are missing.
## Supported languages
* **C#** (.NET)
* **Rust** (Cargo)
* (planned) **C/C++** (GCC including build systems like Make, CMake, Meson)
* (planned) **Golang**
* (planned) **Java**
## Setup
To compile bob, run `dotnet publish bob.csproj -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true`. Then add the output binary to your system `PATH`. 
## Available commands
* **bob build** - read bob-config.json and compile project to executable
* **bob fetch** - clone a repository and automatically build the project (if bobconfig.json exists)
* **bob config** - create an empty bobconfig.json template
* **bob info** - display information about bob
