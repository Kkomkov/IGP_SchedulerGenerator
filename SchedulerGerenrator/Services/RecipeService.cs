using SchedulerGerenrator.Models.ExternalApi;
using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace SchedulerGerenrator.Services
{
    public class  RecipeServiceResponse
    {
        public RecipeServiceResponse(List<Recipe> recipes)
        {
            Recipes = recipes;
        }

        public RecipeServiceResponse([NotNull] string exceptionDescription)
        {
            ExceptionDescription = exceptionDescription;
            IsValid = false;            
        }

        public List<Recipe> Recipes { get; set; } = null;
        public String ExceptionDescription { get; set; } 
        public bool IsValid { get; set; } = true;
    }
    public class RecipeApiService : IRecipeService
    {
        private readonly ILogger<RecipeApiService> logger;
        private readonly RecipeAPISettings settings;

        public const string GetRecipeUrl = "recipe";
        public RecipeApiService( ILogger<RecipeApiService> logger , RecipeAPISettings settings)
        {
            this.logger = logger;
            this.settings = settings;
        }

        public RecipeServiceResponse GetRecipes()
        {
            return null;
        }
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            try
            {
                var client = new HttpClient() { BaseAddress = new UriBuilder() { Host = settings.Url, Port = settings.Port }.Uri };
                var response = await client.GetFromJsonAsync<List<Recipe>>(GetRecipeUrl);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Recipe api request exception", ex);
                throw;
            }
        }
    }
}
