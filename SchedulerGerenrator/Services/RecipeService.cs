using SchedulerGerenrator.Models.ExternalApi;
using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Services.Interfaces;

namespace SchedulerGerenrator.Services
{

    public class RecipeApiService : IRecipeService
    {
        private readonly ILogger<RecipeApiService> logger;
        private readonly RecipeAPISettings settings;

        public const string _RecipeMethodUrl = "recipe";
        public RecipeApiService(ILogger<RecipeApiService> logger, RecipeAPISettings settings)
        {
            this.logger = logger;
            this.settings = settings;
        }


        public async Task<List<Recipe>> GetRecipesAsync()
        {
            try
            {
                var url = new UriBuilder(settings.UseHttps?"https":"http", settings.Host, settings.Port).Uri;
               // { Host = settings.Url, Port = settings.Port }.Uri;
                var client = new HttpClient() { BaseAddress = url};
                var jsonOptions = new System.Text.Json.JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    
                } ;
                var response = await client.GetFromJsonAsync<Recipes>(_RecipeMethodUrl, jsonOptions);
                
                return response.recipes;
            }
            catch (Exception ex)
            {
                logger.LogError("Recipe api request exception", ex);
                throw;
            }
        }
    }
}
