using Microsoft.AspNetCore.Mvc;
using SchedulerGerenrator.Models.ExternalApi.IGS;

using SchedulerGerenrator.Models.Requests;
using SchedulerGerenrator.Models.Response;

using SchedulerGerenrator.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SchedulerGerenrator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchedulerController : ControllerBase
    {
        private readonly ILogger<SchedulerController> _logger;
        public readonly ISchedulerService _schedulerService;
        private readonly IRecipeService _recipeService;

        public SchedulerController(ILogger<SchedulerController> logger,
            ISchedulerService schedulerService,
            IRecipeService recipeService
            )
        {
            _logger = logger;
            _schedulerService = schedulerService;
            _recipeService = recipeService;
        }


        /// <summary>
        /// Return a scheduler for light and water for given trays
        /// </summary>
        /// <param name="trays">List of tray</param>
        /// <returns> IEnumerable<TrayResponse> </returns>
        [HttpPost(Name = "GetTrayScheduler")]
        public async Task<ActionResult<IEnumerable<TrayResponse>>> Get([Required, NotNull] List<TrayRequest> trays)
        {
            //get recipes
            List<Recipe> recipes = null;
            try
            {
                recipes = await  _recipeService.GetRecipesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Request recipe api service failed",ex);
                return StatusCode(StatusCodes.Status500InternalServerError,"Request recipe api service failed");
            }

            var scheduler = _schedulerService.TransformToScheduler(recipes, trays);
            
            return scheduler;

        }

         


    }

}