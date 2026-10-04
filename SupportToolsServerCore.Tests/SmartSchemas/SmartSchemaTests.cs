using System.Collections.Generic;
using System.Linq;
using SupportToolsServerCore.Domain.SmartSchemas;
using Xunit;

namespace SupportToolsServerCore.Tests.SmartSchemas;

public sealed class SmartSchemaTests
{
    [Fact]
    public void Constructor_SetsTheValuesWithoutDetails_AndRaisesNoDomainEvent()
    {
        var id = SmartSchemaId.CreateUnique();

        var smartSchema = new SmartSchema(id, "Reduce", 2, 5);

        Assert.Equal(id, smartSchema.Id);
        Assert.Equal("Reduce", smartSchema.Name);
        Assert.Equal(2, smartSchema.LastPreserveCount);
        Assert.Empty(smartSchema.Details);
        Assert.Equal(5, smartSchema.Version);
        Assert.Empty(smartSchema.DomainEvents);
    }

    [Fact]
    public void Create_ReturnsANewSmartSchemaWithItsDetailsAUniqueIdAndTheFirstVersion()
    {
        SmartSchemaDetail day = SmartSchemaDetail.Create("Day", 3);
        SmartSchemaDetail month = SmartSchemaDetail.Create("Month", 1);

        SmartSchema first = SmartSchema.Create("Reduce", 1, [day, month]);
        SmartSchema second = SmartSchema.Create("Reduce", 1, []);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("Reduce", first.Name);
        Assert.Equal(1, first.LastPreserveCount);
        Assert.Equal([day, month], first.Details);
        Assert.Empty(second.Details);
        Assert.Equal(1, first.Version);
        Assert.Empty(first.DomainEvents);
    }

    //An update replaces the whole aggregate: the details of the update are the only ones left
    [Fact]
    public void Update_ReplacesTheValuesAndTheDetailsKeepingTheIdAndIncrementsTheVersion()
    {
        var id = SmartSchemaId.CreateUnique();
        var smartSchema = new SmartSchema(id, "Reduce", 1, 2);
        smartSchema.Update("Reduce", 1, [SmartSchemaDetail.Create("Day", 3), SmartSchemaDetail.Create("Week", 2)]);
        SmartSchemaDetail hour = SmartSchemaDetail.Create("Hour", 48);

        smartSchema.Update("REDUCE", 4, [hour]);

        Assert.Equal(id, smartSchema.Id);
        Assert.Equal("REDUCE", smartSchema.Name);
        Assert.Equal(4, smartSchema.LastPreserveCount);
        Assert.Equal([hour], smartSchema.Details);
        Assert.Equal(4, smartSchema.Version);

        smartSchema.Update("REDUCE", 4, []);
        Assert.Empty(smartSchema.Details);
        Assert.Equal(5, smartSchema.Version);
    }

    //The new details may come from the current ones, e.g. the same list again
    [Fact]
    public void Update_KeepsTheDetails_WhenTheyAreGivenFromTheAggregateItself()
    {
        SmartSchema smartSchema =
            SmartSchema.Create("Reduce", 1, [SmartSchemaDetail.Create("Day", 3), SmartSchemaDetail.Create("Week", 2)]);
        List<SmartSchemaDetail> details = [.. smartSchema.Details];

        smartSchema.Update("Reduce", 1, smartSchema.Details);

        Assert.Equal(details, smartSchema.Details);
        Assert.Equal(2, smartSchema.Version);
    }

    //Adding to the given list later does not change the schema
    [Fact]
    public void Create_CopiesTheGivenDetails()
    {
        List<SmartSchemaDetail> details = [SmartSchemaDetail.Create("Day", 3)];

        SmartSchema smartSchema = SmartSchema.Create("Reduce", 1, details);
        details.Add(SmartSchemaDetail.Create("Week", 2));

        Assert.Equal(["Day"], smartSchema.Details.Select(x => x.PeriodType));
    }

    [Fact]
    public void Detail_ConstructorAndCreate_SetTheValues()
    {
        var id = SmartSchemaDetailId.CreateUnique();

        var detail = new SmartSchemaDetail(id, "Day", 3);
        SmartSchemaDetail first = SmartSchemaDetail.Create("Month", 1);
        SmartSchemaDetail second = SmartSchemaDetail.Create("Month", 1);

        Assert.Equal(id, detail.Id);
        Assert.Equal("Day", detail.PeriodType);
        Assert.Equal(3, detail.PreserveCount);
        Assert.Equal("Month", first.PeriodType);
        Assert.Equal(1, first.PreserveCount);
        Assert.NotEqual(first.Id, second.Id);
        Assert.Empty(first.DomainEvents);
    }

    [Fact]
    public void Ids_WithTheSameValueAreEqual()
    {
        SmartSchemaId id = SmartSchemaId.CreateUnique();
        SmartSchemaDetailId detailId = SmartSchemaDetailId.CreateUnique();

        Assert.Equal(id, new SmartSchemaId(id.Value));
        Assert.NotEqual(id, SmartSchemaId.CreateUnique());
        Assert.Equal(detailId, new SmartSchemaDetailId(detailId.Value));
        Assert.NotEqual(detailId, SmartSchemaDetailId.CreateUnique());
    }
}
