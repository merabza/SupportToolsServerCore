using Microsoft.EntityFrameworkCore;
using SupportToolsServerCore.Domain.GitIgnoreFileTypes;
using SupportToolsServerCore.Domain.GitRepos;

namespace SupportToolsServerCore.Application.Abstractions;

public interface ISupportToolsServerDbContext
{
    DbSet<GitIgnoreFileType> GitIgnoreFileTypes { get; set; }
    DbSet<GitRepo> GitRepos { get; set; }
}
