using SchedulerGerenrator.Models.ExternalApi.IGS;
using SchedulerGerenrator.Models.Requests;
using SchedulerGerenrator.Models.Response;

namespace SchedulerGerenrator.Services.Interfaces
{
    public interface ISchedulerService
    {
        List<TrayResponse> TransformToScheduler(List<Recipe> recipes, List<TrayRequest> trays);
    }
}