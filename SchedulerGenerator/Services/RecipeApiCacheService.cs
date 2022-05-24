using Microsoft.Extensions.Caching.Memory;
using SchedulerGenerator.Models.ExternalApi.IGS;
using SchedulerGenerator.Services.Interfaces;

namespace SchedulerGenerator.Services
{
    public class RecipeApiCacheService : IRecipeApiService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IRecipeApiService recipeApiService;
        private readonly TimeSpan _cacheAbsoluteTTL = new TimeSpan(0, 0, 30);
        public RecipeApiCacheService(IMemoryCache memoryCache, RecipeApiService recipeApiService)
        {
            this.memoryCache = memoryCache;
            this.recipeApiService = recipeApiService;
        }


        private const string _keyPrefix = "RecipeApi_Recipe";
        protected string GetKey()
        {
            return _keyPrefix;
        }
        public async  Task<List<Recipe>> GetRecipesAsync()
        {
            List<Recipe> result;
            if (memoryCache == null)
            {
                result = await recipeApiService.GetRecipesAsync();
            }
            else
            {
                string key = GetKey();

                result =await  memoryCache.GetOrCreateAsync(key,async (cacheEntry) => //try read from cache if no then execute a method to conver
                {
                    var value = await recipeApiService.GetRecipesAsync();

                    cacheEntry.AbsoluteExpirationRelativeToNow = _cacheAbsoluteTTL;
                    cacheEntry.SetValue(value);

                    return value;
                });
            }

            return result;
        }
    }
}
