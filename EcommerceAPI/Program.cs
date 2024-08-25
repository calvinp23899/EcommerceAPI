using EcommerceAPI.ConfigSwagger;
using EcommerceAPI.Extensions;
using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Service.UriService;
using EcommerceAPI.ServicesExtension;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NLog;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigUriService();
builder.Services.ConfigSwaggerGen();
builder.Services.ConfigSwaggerVersion();


builder.Services.AddControllers()
    .AddApplicationPart(typeof(EcommerceAPI.Presentation.AssemblyReference).Assembly)
    .AddJsonOptions(opt =>
    {
        // api properties response Pascal Case
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
        // serialize enums as strings in api responses (e.g. Role)
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // serialize date only as string
        opt.JsonSerializerOptions.Converters.Add(new JsonDateOnlyConverter());
    });

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);


var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
