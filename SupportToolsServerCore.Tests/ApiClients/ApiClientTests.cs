using SupportToolsServerCore.Domain.ApiClients;
using Xunit;

namespace SupportToolsServerCore.Tests.ApiClients;

//The API keys are made up
public sealed class ApiClientTests
{
    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = ApiClientId.CreateUnique();

        var apiClient = new ApiClient(id, "Pc1.WebAgent", "http://localhost:5031/api/v1/", "made-up-key", 5);

        Assert.Equal(id, apiClient.Id);
        Assert.Equal("Pc1.WebAgent", apiClient.Name);
        Assert.Equal("http://localhost:5031/api/v1/", apiClient.Server);
        Assert.Equal("made-up-key", apiClient.ApiKey);
        Assert.Equal(5, apiClient.Version);
        Assert.Empty(apiClient.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewApiClientWithAUniqueIdAndTheFirstVersion()
    {
        ApiClient first = ApiClient.Create("Pc1.WebAgent", "http://localhost:5031/api/v1/", "made-up-key");
        ApiClient second = ApiClient.Create("Pc1.WebAgent", "http://localhost:5031/api/v1/", "made-up-key");

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("Pc1.WebAgent", first.Name);
        Assert.Equal("http://localhost:5031/api/v1/", first.Server);
        Assert.Equal("made-up-key", first.ApiKey);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Create_AcceptsAMissingServerAndApiKey()
    {
        ApiClient apiClient = ApiClient.Create("Pc1.WebAgent", null, null);

        Assert.Null(apiClient.Server);
        Assert.Null(apiClient.ApiKey);
    }

    [Fact]
    public void Update_ChangesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var id = ApiClientId.CreateUnique();
        var apiClient = new ApiClient(id, "Pc1.WebAgent", "http://localhost:5031/api/v1/", "key-a", 2);

        apiClient.Update("PC1.WEBAGENT", "https://pc1.example.com/api/v1/", "key-b");

        Assert.Equal(id, apiClient.Id);
        Assert.Equal("PC1.WEBAGENT", apiClient.Name);
        Assert.Equal("https://pc1.example.com/api/v1/", apiClient.Server);
        Assert.Equal("key-b", apiClient.ApiKey);
        Assert.Equal(3, apiClient.Version);

        apiClient.Update("PC1.WEBAGENT", null, null);
        Assert.Null(apiClient.Server);
        Assert.Null(apiClient.ApiKey);
        Assert.Equal(4, apiClient.Version);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        ApiClientId id = ApiClientId.CreateUnique();

        Assert.Equal(id, new ApiClientId(id.Value));
        Assert.NotEqual(id, ApiClientId.CreateUnique());
    }
}
