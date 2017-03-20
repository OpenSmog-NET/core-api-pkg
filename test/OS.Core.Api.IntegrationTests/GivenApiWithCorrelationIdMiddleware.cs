using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace OS.Core.Api.IntegrationTests
{
    public class GivenApiWithCorrelationIdMiddleware : ApiTestFixture<GivenApiWithCorrelationIdMiddleware.Startup>
    {
        [Fact]
        public async Task WhenCallingTheApi_SuccessResponse_CorrelationIdShouldBeGenerated()
        {
            var client = Server.CreateClient();
            var result = await client.GetAsync("/");

            result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenCallingTheApi_FailureResponse_CorrelationIdShouldBeGenerated()
        {
            var client = Server.CreateClient();
            var result = await client.GetAsync("/error");

            result.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            { }

            public void Configure(IApplicationBuilder app)
            {
                app.UseOpenSmogMiddlewares();
                app.Map("/error", appl =>
                {
                    appl.Run(async (ctx) =>
                    {
                        AssertCorrelationId(ctx);

                        ctx.Response.StatusCode = 500;

                        await Task.FromResult(0);
                    });
                });
                app.Run(async (ctx) =>
                {
                    AssertCorrelationId(ctx);

                    ctx.Response.StatusCode = 200;

                    await Task.FromResult(0);
                });
            }
        }
    }
}
