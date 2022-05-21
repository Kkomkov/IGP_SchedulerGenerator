using SchedulerGerenrator.Models.ExternalApi;
using SchedulerGerenrator.Services;
using SchedulerGerenrator.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISchedulerService, SchedulerService>();

var recipeApiSettings = builder.Configuration.GetSection("ExternalServices")
                                             .GetSection("RecipeApi")
                                             .Get<RecipeAPISettings>();

builder.Services.AddSingleton<RecipeAPISettings>(recipeApiSettings);
builder.Services.AddScoped<IRecipeService,RecipeApiService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

