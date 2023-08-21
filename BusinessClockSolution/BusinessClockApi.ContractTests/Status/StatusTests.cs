
using Alba;
using BusinessClockApi.Models;
using BusinessClockApi.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace BusinessClockApi.ContractTests.Status;

public class StatusTests
{
    [Fact]
    public async Task WhileWeAreOpen()
    {
        var host = await AlbaHost.For<Program>(config =>
        {
            var clock = Substitute.For<ISystemTime>();

            clock.GetCurrent().Returns(new DateTime(2023, 8, 21, 16, 59, 00));

            config.ConfigureServices(services =>
            {
                services.AddSingleton<ISystemTime>(clock);
            });
        });

        var expectedResponse = new ClockResponseModel
        {
            IsOpen = true,
            SupportContact = new SupportContactResponseModel
            {
                Name = "Mitch",
                Phone = "800 555-1212",
                Email = "mitch@company.com"
            }
        };
        
        var response = await host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();

        });

        var responseMessage = response.ReadAsJson<ClockResponseModel>();

        Assert.NotNull(responseMessage);

        Assert.Equal(expectedResponse, responseMessage);

    }

    [Fact]
    public async Task WhileWeAreClosed()
    {

        var host = await AlbaHost.For<Program>(config =>
        {
            var clock = Substitute.For<ISystemTime>();

            clock.GetCurrent().Returns(new DateTime(2023, 8, 21, 17, 00, 00));

            config.ConfigureServices(services =>
            {
                services.AddSingleton<ISystemTime>(clock);
            });
        });

        var expectedResponse = new ClockResponseModel
        {
            IsOpen = false,
            SupportContact = new SupportContactResponseModel
            {
                Name = "Support Pros Inc.",
                Phone = "800 999-1213x23",
                Email = "support@support-pros.com"
            }
        };

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();

        });

        var responseMessage = response.ReadAsJson<ClockResponseModel>();

        Assert.NotNull(responseMessage);

        Assert.Equal(expectedResponse, responseMessage);
    }
}
