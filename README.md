# SupportToolsServerCore

Domain model and application abstractions of [SupportToolsServer](https://github.com/merabza/SupportToolsServer), shared by the server and by its database tooling ([SupportToolsServerDbPart](https://github.com/merabza/SupportToolsServerDbPart), [SupportToolsServerDbTools](https://github.com/merabza/SupportToolsServerDbTools)).

| Project | Purpose |
|---|---|
| `SupportToolsServerCore.Domain` | Entities with strongly-typed ids and their column-length constants (`GitIgnoreFileTypes`, `GitRepos` — a repo references its gitignore file type by id), `Primitives` (`Entity<TId>`, `ValueObject`), repository interfaces and the generic `Sync/Syncroniser<T,TId>` |
| `SupportToolsServerCore.Application.Abstractions` | `ISupportToolsServerDbContext`: the `DbSet`s the application sees; `SupportToolsServerDbContext` in SupportToolsServerDbPart implements it |

A change to an entity needs its EF configuration in SupportToolsServerDbPart and a migration in SupportToolsServerDbTools.

## Build

The projects have no sibling-repo references, so the repository builds on its own:

```powershell
dotnet build SupportToolsServerCore.slnx
```

## License

[MIT](LICENSE)
