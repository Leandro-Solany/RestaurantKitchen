using KitchenRouting.Infrastructure;
using KitchenRouting.Services;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseUrls("http://localhost:5132");
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IKitchenQueueStore, InMemoryKitchenQueueStore>();
builder.Services.AddScoped<IOrderRoutingService, OrderRoutingService>();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
