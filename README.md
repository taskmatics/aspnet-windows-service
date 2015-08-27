aspnet-windows-service
=======

This repository provides a shell project you can use to get a Windows service up running hosting ASP.NET 5 with static files and MVC 6.

For detailed information, please read the following post: [How to Host ASP.NET in a Windows Service](http://taskmatics.com/blog/host-asp-net-in-a-windows-service/)

### Installation

#### 0. Run a Command Prompt as Administrator
This is needed in order to run the `install` command.

#### 1. Clone the repository
Run `git clone https://github.com/taskmatics/aspnet-windows-service`

Run `cd aspnet-windows-service`

#### 2. Install the service
Run `install <optional-service-name>` (`AspNetWindowsService` is used by default for the name)

This command does a few things:
* Publishes the project to an output folder (`.\publish-output`)
* Installs the Windows service (using the service name provided) and points to the `run.cmd` in the published output folder
* Starts the service

#### Browsing the Website
To view static content (from `index.html`), launch a browser a pull up `http://localhost:5000`. The port is configurable in code in the `src\Program.cs`.

To view MVC output (from `TimeController`), launch a browser a pull up `http://localhost:5000/time`. The port is configurable in code in the `src\Program.cs`.

#### Uninstalling
Run `uninstall <optional-service-name>` (The name must match the one used during install.)

This command will stop and uninstall the Windows service (using the service name provided).