using GeoService.ApiClients;
using GeoService.ApiClients.WftGeoDBApiClient;
using GeoService.Services;
using SoapCore;
using System.ServiceModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGeoApiClient, WftGeoDBApiCLient>();
builder.Services.AddHttpClient<IGeoApiClient, WftGeoDBApiCLient>();

//WCF            
builder.Services.AddSingleton<ISoapApiGeoService, SoapApiGeoService>();
builder.Services.AddMvc();
//

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//WCF
app.UseSoapEndpoint<ISoapApiGeoService>("/api/soap/Service.svc", new SoapEncoderOptions());
//
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
