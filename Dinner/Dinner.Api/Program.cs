using Dinner.Api.Errors;
using Dinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, DinnerProblemDetailsFactory>();
}
var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/error");
    app.MapControllers();
    app.Run();
}