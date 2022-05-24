using SchedulerGenerator.Models.ExternalApi.IGS;
using SchedulerGenerator.Models.Requests;
using SchedulerGenerator.Models.Response;

namespace SchedulerGenerator.Services.Interfaces
{
    public interface ISchedulerService
    {
        List<TrayResponse> TransformToScheduler(List<Recipe> recipes, List<TrayRequest> trays);
    }
}