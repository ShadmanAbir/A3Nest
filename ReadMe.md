\# !\[A3Nest Logo](https://via.placeholder.com/150x50?text=A3Nest)



\*\*A3Nest – Modern Property Management Platform\*\*



\[!\[.NET](https://img.shields.io/badge/.NET-8-blue)](https://dotnet.microsoft.com/)

\[!\[MAUI](https://img.shields.io/badge/MAUI-CrossPlatform-green)](https://learn.microsoft.com/en-us/dotnet/maui/)

\[!\[License](https://img.shields.io/badge/License-MIT-lightgrey)](LICENSE)

\[!\[SignalR](https://img.shields.io/badge/SignalR-Realtime-orange)](https://dotnet.microsoft.com/apps/aspnet/signalr)



---



\## \*\*Overview\*\*



\*\*A3Nest\*\* is a \*\*cross-platform property management and tenant collaboration system\*\*, built with \*\*.NET 8 MAUI\*\* and \*\*Clean Architecture\*\*.

Supports \*\*Windows Desktop, Android, and iOS\*\*, with real-time updates via \*\*SignalR\*\* and advanced property search via \*\*Elasticsearch\*\*.



A3Nest provides a \*\*modern, sleek interface\*\* for property owners, managers, and tenants — fully modular and ready for business logic implementation.



---



\## \*\*Table of Contents\*\*



1\. \[Features](#features)

2\. \[Architecture](#architecture)

3\. \[Tech Stack](#tech-stack)

4\. \[Screenshots](#screenshots)

5\. \[Project Structure](#project-structure)

6\. \[Setup Instructions](#setup-instructions)

7\. \[Future Work](#future-work)

8\. \[Contributing](#contributing)

9\. \[License](#license)



---



\## \*\*Features (Skeleton / Placeholder)\*\*



\* 🏠 Dashboard with KPIs, widgets, and charts

\* 🏢 Properties management with \*\*Elastic-powered search\*\* and filters

\* 👥 Tenant management and lease applications

\* 📊 Owner portal with reports, payment summaries, and approvals

\* 💬 Real-time messaging with \*\*SignalR\*\*

\* 📅 Calendar with task and event management

\* ⚙️ Settings for roles, permissions, and theme selection

\* 👀 Guest view mode

\* UI components: \*\*DataGrid, CardWidget, ModalForm, SearchBar\*\*



> \*\*Note:\*\* All service methods, controllers, and SignalR methods are stubbed; actual business logic to be implemented later.



---



\## \*\*Architecture\*\*



\*\*Clean Architecture Layers:\*\*



```

Presentation (MAUI) --> Application (Interfaces, DTOs, Use Cases) --> Infrastructure (EF Core, SignalR, Elastic)

Domain Layer: Independent entities, value objects, enums

```



\*\*Layers Description:\*\*



\* \*\*Domain:\*\* Core entities (Property, Tenant, LeaseApplication, User, Role, Task, CalendarEvent, Message, Report)

\* \*\*Application:\*\* Interfaces, DTOs, Commands/Queries

\* \*\*Infrastructure:\*\* EF Core DbContext, Repositories, SignalR Hubs, Elastic indexers, external services

\* \*\*Presentation:\*\* MAUI pages, ViewModels, components, and navigation



---



\## \*\*Tech Stack\*\*



\* \*\*Frontend/UI:\*\* .NET 8 MAUI

\* \*\*Backend:\*\* ASP.NET Core 8 Web API

\* \*\*Database:\*\* SQL Server 2019/2022

\* \*\*Real-time Communication:\*\* SignalR

\* \*\*Search:\*\* Elasticsearch

\* \*\*Architecture:\*\* Clean Architecture

\* \*\*Hosting:\*\* Windows Server / IIS



---



\## \*\*Screenshots\*\*



> \*\*Placeholder images – replace with actual screenshots once UI is implemented\*\*



| Dashboard                                                        | Properties                                                         | Messaging                                                        |

| ---------------------------------------------------------------- | ------------------------------------------------------------------ | ---------------------------------------------------------------- |

| !\[Dashboard](https://via.placeholder.com/300x200?text=Dashboard) | !\[Properties](https://via.placeholder.com/300x200?text=Properties) | !\[Messaging](https://via.placeholder.com/300x200?text=Messaging) |



| Calendar                                                       | Owner Portal                                                         | Settings                                                       |

| -------------------------------------------------------------- | -------------------------------------------------------------------- | -------------------------------------------------------------- |

| !\[Calendar](https://via.placeholder.com/300x200?text=Calendar) | !\[OwnerPortal](https://via.placeholder.com/300x200?text=OwnerPortal) | !\[Settings](https://via.placeholder.com/300x200?text=Settings) |



---



\## \*\*Project Structure\*\*



```

A3Nest/

├─ A3Nest.Domain/       # Entities, ValueObjects, Enums

├─ A3Nest.Application/  # Interfaces, DTOs, Commands/Queries

├─ A3Nest.Infrastructure/ # DbContext, Repositories, SignalR Hubs, Elastic Indexer

├─ A3Nest.MAUI/         # Views, ViewModels, Components, Resources

└─ A3Nest.Solution.sln  # Solution file

```



---



\## \*\*Setup Instructions\*\*



1\. Clone the repository:



```bash

git clone https://github.com/yourusername/A3Nest.git

```



2\. Open `A3Nest.Solution.sln` in Visual Studio 2022+



3\. Restore NuGet packages:



```bash

dotnet restore

```



4\. Configure \*\*SQL Server connection\*\* in `Infrastructure/Db/A3NestDbContext.cs`.



5\. Run the MAUI project:



```bash

dotnet build

dotnet run --project A3Nest.MAUI

```



6\. Optional: Start \*\*SignalR hubs\*\* (stub methods ready for implementation)



---



\## \*\*Future Work\*\*



\* Implement business logic in \*\*Application services\*\*

\* Unit testing (skipped for initial skeleton)

\* Elastic search queries and indexing

\* Push notifications (Firebase / Windows Toast)

\* CI/CD pipelines for Windows, Android, iOS



---



\## \*\*Contributing\*\*



1\. Fork the repository

2\. Create a branch: `git checkout -b feature-name`

3\. Commit changes: `git commit -m "Add feature"`

4\. Push branch: `git push origin feature-name`

5\. Open a Pull Request



---



\## \*\*License\*\*



MIT License © 2025 \\\[Shadman Sakib]

