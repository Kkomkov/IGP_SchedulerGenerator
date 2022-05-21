using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SchedulerGerenrator.Models.Requests
{
    public class TrayRequest
    {
        [DisplayName("trayNumber")]
        [Required]
        public int TrayNumber { get; set; }

        [DisplayName("recipeName")]
        [Required]
        public string RecipeName { get; set; }

        [DisplayName("startDate")]
        public DateTime StartDate { get; set; }
    }
}
