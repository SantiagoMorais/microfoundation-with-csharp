# Microfoundation: Backend Web Development

# Unit 1 - Topic 1 - Architectural Styles

## What Is an Architectural Style?
- A proven, well-understood solution to a **recurring problem** in a given **context**: **context + problem + solution**.
- Each style favors certain **quality attributes** (e.g., performance, security, extensibility), so the choice depends on what you need.
- You **take inspiration** from a style; you don't copy it exactly. Real systems usually **combine several** (e.g., client/server + MVC).
- The style's name works as **shorthand**: saying "layered monolith" tells another architect the structure, strengths, and weaknesses right away.

## The Styles at a Glance
| Style | Core idea | Strength | Weakness | Examples |
|---|---|---|---|---|
| **Monolithic** | The whole app is **one unit**, deployed as a single package | Good **performance** (no communication between components) | **Tightly coupled** and hard to maintain: a change touches everything ("big ball of mud") | Offline systems, popular games, web apps deployed as one package |
| **Microkernel (plug-in)** | A fixed **core** plus **plug-ins** added dynamically | **Extensibility and customization** | **Security**: third-party code gets access to the core | Chrome extensions, Eclipse / IDE plug-ins |
| **Pipes and Filters** | Data flows through a chain of **filters** joined by **pipes** | Tasks split and recombined; each filter is simple and reusable | Needs a **standard input/output format** between filters | Unix shell (`cmd1 \| cmd2`), compilers, ETL tools |
| **Layered** | Components split into layers with clear **responsibilities** | Organization, **evolution** (new layers can be added) | Can pass data through layers that do nothing (overhead) | Web apps, the OSI model |
| **Client/Server** | Clients **request**, a server **responds** (2 tiers) | Centralized control; one server serves many clients | Server is the **bottleneck** and single point of failure; replication is expensive | Browser + web server |
| **Peer-to-Peer** | Every node is **client and server** at the same time | **Scalability** and fault tolerance; no central point | Data is scattered, so **administration is hard** | File sharing (torrent), distributed processing |

> A monolith can still have **internal layers** (presentation, business rules, data). The whole is one unit, but it's organized inside.

## Pipes and Filters
```
source ──► [filter 1] ──pipe──► [filter 2] ──pipe──► [filter 3] ──► output
```
- **Pipes**: one-way, usually point-to-point channels.
- **Filters**: independent, usually **stateless**, and **one task each**. A complex task becomes a sequence of filters.
- Compiler example: source code → lexical analysis → syntax analysis → intermediate code → machine code. Each stage's output is the next stage's input.

## Layered: Open vs. Closed Layers
```
 Presentation
      ▼
 Business rules   ← CLOSED: requests must pass through it
      ▼
 Persistence      ← OPEN: layers above may skip it
      ▼
 Database
```
| | Closed layer | Open layer |
|---|---|---|
| Can a request skip it? | No | Yes |
| Effect | **Isolation**: changing one layer doesn't break others | Faster for simple reads, but **more dependencies**, harder to maintain |

- **Sinkhole anti-pattern**: a request crosses many layers that only pass it along without doing anything. It adds cost for no benefit.
- Layers make it easy to add a middle layer (e.g., **RPC/RMI middleware**) so the app doesn't have to deal with TCP/IP directly.

## Layer vs. Tier
Both translate to "camada" in Portuguese, but:
- **Layer** = **logical** separation (code organization).
- **Tier** = **physical** separation (different servers, VMs, or cloud instances).
- Example: 3 layers (presentation, business, data) all on **one server** = 1 tier. Moving the database to its own server = 2 tiers.

## Client/Server Details
- **Thin client**: only renders (HTML/CSS/JS); the server does the processing.
- **Fat client**: takes on part of the processing (e.g., rendering via APIs) to **relieve the server**, which matters with thousands of clients.
- Once the system is no longer monolithic, **communication becomes a security concern** (e.g., HTTP over the internet). On an intranet, lighter protocols (TCP, RPC) can give better performance than HTTP.

