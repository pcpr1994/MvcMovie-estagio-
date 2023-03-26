using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace MvcMovie;

public class MvcMovieWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<MvcMovieWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
