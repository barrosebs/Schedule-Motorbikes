using SM.Domain.Enum;
using SM.Domain.Model;
using SM.Web.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class AllocationServiceTests
{
    [Fact]
    public void CalcDays_DeliveryDateLessThanOrEqualStartDate_ThrowsValidationException()
    {
        // Arrange
        var allocation = new AllocationVM
        {
            StartDateToAllocation = new DateTime(2024, 6, 1),
            DeliveryDate = new DateTime(2024, 6, 1),
            EPlan = EAllocationPlan.Basic
        };
        var plan = new PlanModel
        {
            Value = 100,
            LimitDayPlan = 10
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => ToolsHelpers.CalcDays(allocation, plan));
    }

    [Fact]
    public void CalcDays_UsedDaysLessThanLimitDayPlan_ReturnsExpectedAllocation()
    {
        // Arrange
        var allocation = new AllocationVM
        {
            StartDateToAllocation = new DateTime(2024, 6, 1),
            DeliveryDate = new DateTime(2024, 6, 5),
            EPlan = EAllocationPlan.Basic
        };
        var plan = new PlanModel
        {
            Value = 100,
            LimitDayPlan = 10
        };

        // Act
        var result = ToolsHelpers.CalcDays(allocation, plan);

        // Assert
        Assert.Equal(4, result.UsedDays);
        decimal expectedRemainingDays = plan.LimitDayPlan - 4;
        decimal expectedPenalty = expectedRemainingDays * plan.Value * 0.20m;
        decimal expectedSum = 400 + expectedPenalty;
        Assert.Equal(expectedSum, result.Sum);
        Assert.Equal(400, result.ValueDay);
    }

    [Fact]
    public void CalcDays_UsedDaysEqualLimitDayPlan_ReturnsExpectedAllocation()
    {
        // Arrange
        var allocation = new AllocationVM
        {
            StartDateToAllocation = new DateTime(2024, 6, 1),
            DeliveryDate = new DateTime(2024, 6, 11),
            EPlan = EAllocationPlan.Standard
        };
        var plan = new PlanModel
        {
            Value = 100,
            LimitDayPlan = 10
        };

        // Act
        var result = ToolsHelpers.CalcDays(allocation, plan);

        // Assert
        Assert.Equal(0, result.UsedDays);  // No remaining days
        Assert.Equal(1000, result.Sum);
        Assert.Equal(1000, result.ValueDay);
    }

    [Fact]
    public void CalcDays_UsedDaysGreaterThanLimitDayPlan_ReturnsExpectedAllocation()
    {
        // Arrange
        var allocation = new AllocationVM
        {
            StartDateToAllocation = new DateTime(2024, 6, 1),
            DeliveryDate = new DateTime(2024, 6, 15),
            EPlan = EAllocationPlan.Basic
        };
        var plan = new PlanModel
        {
            Value = 100,
            LimitDayPlan = 10
        };

        // Act
        var result = ToolsHelpers.CalcDays(allocation, plan);

        // Assert
        Assert.Equal(14, result.UsedDays);
        Assert.Equal(1400 + 50, result.Sum);  // 14 * 100 + 50
        Assert.Equal(1400, result.ValueDay);  // 14 * 100
    }
}
