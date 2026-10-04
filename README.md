# SupportToolsServerCore

Domain model and application abstractions of [SupportToolsServer](https://github.com/merabza/SupportToolsServer), shared by the server and by its database tooling ([SupportToolsServerDbPart](https://github.com/merabza/SupportToolsServerDbPart), [SupportToolsServerDbTools](https://github.com/merabza/SupportToolsServerDbTools)).

| Project | Purpose |
|---|---|
| `SupportToolsServerCore.Domain` | Entities with strongly-typed ids and their column-length constants (`DeploymentEnvironments`, `DotnetTools`, `EditorConfigFileTypes`, `GitIgnoreFileTypes`, `GitRepos`, `NpmPackages`, `ReactAppTemplates`, `Runtimes`; a repo references its gitignore file type by id), `Primitives` (`Entity<TId>`; `VersionedEntity<TId>`, the base of every registry aggregate root, whose `Version` starts at `EntityVersion.Initial` and is the optimistic-concurrency token; `ValueObject`), repository interfaces and the generic `Sync/Syncroniser<T,TId>` |
| `SupportToolsServerCore.Application.Abstractions` | `ISupportToolsServerDbContext`: the `DbSet`s the application sees; `SupportToolsServerDbContext` in SupportToolsServerDbPart implements it |

A change to an entity needs its EF configuration in SupportToolsServerDbPart and a migration in SupportToolsServerDbTools.

## Build

`SupportToolsServerCore.Domain` references `SystemTools.SharedKernel` by relative path (`..\..\SystemTools\...`) for the domain-event base types (`Entity`, `IDomainEvent`; `GitRepo` raises `GitRepoAddedDomainEvent` and `GitRepoUpdatedDomainEvent`), so [SystemTools](https://github.com/merabza/SystemTools) must be cloned next to this repository:

```powershell
dotnet build SupportToolsServerCore.slnx
```

## License

[MIT](LICENSE)
