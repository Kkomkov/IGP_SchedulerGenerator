
namespace SchedulerGerenrator.Models.Response
{
    public class TrayResponse { 
        
        public int TrayNumber { get; private set; }
        
        public List<LightSchedulerRecord> Light { get; set; }
        public List<WaterSchedulerRecord> Water { get; set; }
        public string Exception { get;  set; }

        public TrayResponse(int trayNumber)
        {
            TrayNumber = trayNumber;           
            
        }
    }
}
