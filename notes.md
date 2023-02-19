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



run test with `dotnet test --logger:"console;verbosity=detailed"` 
