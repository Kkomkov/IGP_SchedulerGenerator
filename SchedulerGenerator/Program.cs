using SchedulerGenerator.Services;
using SchedulerGenerator.Services.Interfaces;
using SchedulerGenerator.Models.ExternalApi;
using SchedulerGenerator.Services;
using SchedulerGenerator.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();


var recipeApiSettings = builder.Configuration.GetSection("ExternalServices")
                                             .GetSection("RecipeApi")
                                             .Get<RecipeAPISettings>();

builder.Services.AddSingleton<RecipeAPISettings>(recipeApiSettings);
builder.Services.AddScoped<IRecipeApiService,RecipeApiService>();
builder.Services.AddScoped<IRecipeManipulationService, RecipeManipulationService>();
builder.Services.AddScoped<ISchedulerService, SchedulerService>();

//Inject Locacl Memory cache
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

