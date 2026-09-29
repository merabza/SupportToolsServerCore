using Microsoft.EntityFrameworkCore;
using SupportToolsServerCore.Domain.EditorConfigFileTypes;
using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using SupportToolsServerCore.Domain.GitRepos;

namespace SupportToolsServerCore.Application.Abstractions;

public interface ISupportToolsServerDbContext
{
    DbSet<EditorConfigFileType> EditorConfigFileTypes { get; set; }
    DbSet<GitIgnoreFileType> GitIgnoreFileTypes { get; set; }
    DbSet<GitRepo> GitRepos { get; set; }
}
