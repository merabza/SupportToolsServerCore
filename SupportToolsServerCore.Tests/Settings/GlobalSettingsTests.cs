using System;
using SupportToolsServerCore.Domain.ApiClients;
using SupportToolsServerCore.Domain.FileStorages;
using SupportToolsServerCore.Domain.Settings;
using SupportToolsServerCore.Domain.SmartSchemas;
using Xunit;

namespace SupportToolsServerCore.Tests.Settings;

//The license keys are made up
public sealed class GlobalSettingsTests
{
    private static DatabasesBackupFilesExchange EmptyExchange()
    {
        return new DatabasesBackupFilesExchange(null, null, null, null, null);
    }

    [Fact]
    public void Constructor_SetsTheValues_AndRaisesNoDomainEvent()
    {
        var id = new GlobalSettingsId(Guid.NewGuid());
        FileStorageId fileStorageId = FileStorageId.CreateUnique();
        SmartSchemaId exchangeSchemaId = SmartSchemaId.CreateUnique();
        SmartSchemaId localSchemaId = SmartSchemaId.CreateUnique();
        ApiClientId apiClientId = ApiClientId.CreateUnique();
        var exchange = new DatabasesBackupFilesExchange(".down!", ".up!", fileStorageId, exchangeSchemaId,
            localSchemaId);

        var globalSettings = new GlobalSettings(id, "ltgmz", ".up!", "yyyyMMddHHmmss", ".zip", "yyyyMMdd", ".json",
            "made-up-license-key", fileStorageId, exchangeSchemaId, localSchemaId, apiClientId, exchange, 5);

        Assert.Equal(id, globalSettings.Id);
        Assert.Equal("ltgmz", globalSettings.ServiceDescriptionSignature);
        Assert.Equal(".up!", globalSettings.UploadTempExtension);
        Assert.Equal("yyyyMMddHHmmss", globalSettings.ProgramArchiveDateMask);
        Assert.Equal(".zip", globalSettings.ProgramArchiveExtension);
        Assert.Equal("yyyyMMdd", globalSettings.ParametersFileDateMask);
        Assert.Equal(".json", globalSettings.ParametersFileExtension);
        Assert.Equal("made-up-license-key", globalSettings.MediatRLicenseKey);
        Assert.Equal(fileStorageId, globalSettings.FileStorageForExchangeId);
        Assert.Equal(exchangeSchemaId, globalSettings.SmartSchemaForExchangeId);
        Assert.Equal(localSchemaId, globalSettings.SmartSchemaForLocalId);
        Assert.Equal(apiClientId, globalSettings.LocalPackageManagerWebApiClientId);
        Assert.Same(exchange, globalSettings.DatabasesBackupFilesExchange);
        Assert.Equal(5, globalSettings.Version);
        Assert.Empty(globalSettings.DomainEvents);
    }

    [Fact]
    public void ExchangeConstructor_SetsTheValues()
    {
        FileStorageId fileStorageId = FileStorageId.CreateUnique();
        SmartSchemaId exchangeSchemaId = SmartSchemaId.CreateUnique();
        SmartSchemaId localSchemaId = SmartSchemaId.CreateUnique();

        var exchange = new DatabasesBackupFilesExchange(".down!", ".up!", fileStorageId, exchangeSchemaId,
            localSchemaId);

        Assert.Equal(".down!", exchange.DownloadTempExtension);
        Assert.Equal(".up!", exchange.UploadTempExtension);
        Assert.Equal(fileStorageId, exchange.ExchangeFileStorageId);
        Assert.Equal(exchangeSchemaId, exchange.ExchangeSmartSchemaId);
        Assert.Equal(localSchemaId, exchange.LocalSmartSchemaId);
    }

    [Fact]
    public void Create_ReturnsTheSingletonWithTheFirstVersion()
    {
        FileStorageId fileStorageId = FileStorageId.CreateUnique();
        SmartSchemaId schemaId = SmartSchemaId.CreateUnique();
        ApiClientId apiClientId = ApiClientId.CreateUnique();
        DatabasesBackupFilesExchange exchange = EmptyExchange();

        GlobalSettings first = GlobalSettings.Create("ltgmz", ".up!", "yyyyMMddHHmmss", ".zip", "yyyyMMdd", ".json",
            "made-up-license-key", fileStorageId, schemaId, schemaId, apiClientId, exchange);
        GlobalSettings second = GlobalSettings.Create(null, null, null, null, null, null, null, null, null, null, null,
            EmptyExchange());

        Assert.Equal(GlobalSettingsId.Singleton, first.Id);
        Assert.Equal(first.Id, second.Id);
        Assert.Equal("ltgmz", first.ServiceDescriptionSignature);
        Assert.Equal(".up!", first.UploadTempExtension);
        Assert.Equal("yyyyMMddHHmmss", first.ProgramArchiveDateMask);
        Assert.Equal(".zip", first.ProgramArchiveExtension);
        Assert.Equal("yyyyMMdd", first.ParametersFileDateMask);
        Assert.Equal(".json", first.ParametersFileExtension);
        Assert.Equal("made-up-license-key", first.MediatRLicenseKey);
        Assert.Equal(fileStorageId, first.FileStorageForExchangeId);
        Assert.Equal(schemaId, first.SmartSchemaForExchangeId);
        Assert.Equal(schemaId, first.SmartSchemaForLocalId);
        Assert.Equal(apiClientId, first.LocalPackageManagerWebApiClientId);
        Assert.Same(exchange, first.DatabasesBackupFilesExchange);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
        Assert.Null(second.ServiceDescriptionSignature);
        Assert.Null(second.MediatRLicenseKey);
        Assert.Null(second.FileStorageForExchangeId);
        Assert.Null(second.LocalPackageManagerWebApiClientId);
    }

    [Fact]
    public void Update_ReplacesTheValuesKeepingTheIdAndIncrementsTheVersion()
    {
        var globalSettings = new GlobalSettings(GlobalSettingsId.Singleton, null, null, null, null, null, null, null,
            null, null, null, null, EmptyExchange(), 2);
        FileStorageId fileStorageId = FileStorageId.CreateUnique();
        SmartSchemaId exchangeSchemaId = SmartSchemaId.CreateUnique();
        SmartSchemaId localSchemaId = SmartSchemaId.CreateUnique();
        ApiClientId apiClientId = ApiClientId.CreateUnique();
        var exchange = new DatabasesBackupFilesExchange(".down!", ".up!", fileStorageId, exchangeSchemaId,
            localSchemaId);

        globalSettings.Update("ltgmz", ".up!", "yyyyMMddHHmmss", ".zip", "yyyyMMdd", ".json", "made-up-license-key",
            fileStorageId, exchangeSchemaId, localSchemaId, apiClientId, exchange);

        Assert.Equal(GlobalSettingsId.Singleton, globalSettings.Id);
        Assert.Equal("ltgmz", globalSettings.ServiceDescriptionSignature);
        Assert.Equal(".up!", globalSettings.UploadTempExtension);
        Assert.Equal("yyyyMMddHHmmss", globalSettings.ProgramArchiveDateMask);
        Assert.Equal(".zip", globalSettings.ProgramArchiveExtension);
        Assert.Equal("yyyyMMdd", globalSettings.ParametersFileDateMask);
        Assert.Equal(".json", globalSettings.ParametersFileExtension);
        Assert.Equal("made-up-license-key", globalSettings.MediatRLicenseKey);
        Assert.Equal(fileStorageId, globalSettings.FileStorageForExchangeId);
        Assert.Equal(exchangeSchemaId, globalSettings.SmartSchemaForExchangeId);
        Assert.Equal(localSchemaId, globalSettings.SmartSchemaForLocalId);
        Assert.Equal(apiClientId, globalSettings.LocalPackageManagerWebApiClientId);
        Assert.Same(exchange, globalSettings.DatabasesBackupFilesExchange);
        Assert.Equal(3, globalSettings.Version);

        DatabasesBackupFilesExchange emptyExchange = EmptyExchange();
        globalSettings.Update(null, null, null, null, null, null, null, null, null, null, null, emptyExchange);
        Assert.Null(globalSettings.ServiceDescriptionSignature);
        Assert.Null(globalSettings.UploadTempExtension);
        Assert.Null(globalSettings.ProgramArchiveDateMask);
        Assert.Null(globalSettings.ProgramArchiveExtension);
        Assert.Null(globalSettings.ParametersFileDateMask);
        Assert.Null(globalSettings.ParametersFileExtension);
        Assert.Null(globalSettings.MediatRLicenseKey);
        Assert.Null(globalSettings.FileStorageForExchangeId);
        Assert.Null(globalSettings.SmartSchemaForExchangeId);
        Assert.Null(globalSettings.SmartSchemaForLocalId);
        Assert.Null(globalSettings.LocalPackageManagerWebApiClientId);
        Assert.Same(emptyExchange, globalSettings.DatabasesBackupFilesExchange);
        Assert.Equal(4, globalSettings.Version);
    }

    [Fact]
    public void SingletonId_IsFixed()
    {
        Assert.Equal(new Guid("00000000-0000-0000-0000-000000000001"), GlobalSettingsId.Singleton.Value);
        Assert.Same(GlobalSettingsId.Singleton, GlobalSettingsId.Singleton);
        Assert.Equal(GlobalSettingsId.Singleton, new GlobalSettingsId(GlobalSettingsId.Singleton.Value));
        Assert.NotEqual(GlobalSettingsId.Singleton, new GlobalSettingsId(Guid.NewGuid()));
    }
}
