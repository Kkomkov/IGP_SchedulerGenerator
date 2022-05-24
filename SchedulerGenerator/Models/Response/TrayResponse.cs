
namespace SchedulerGenerator.Models.Response
{
    public class TrayResponse { 
        
        public int TrayNumber { get; private set; }
        
        public List<LightingSchedulerRecord> Lighting { get; set; }
        public List<WateringSchedulerRecord> Watering { get; set; }
        public string Exception { get;  set; }

        public TrayResponse(int trayNumber)
        {
            TrayNumber = trayNumber;           
            
        }
    }
}
