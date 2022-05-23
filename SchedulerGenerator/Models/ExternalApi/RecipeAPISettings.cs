namespace SchedulerGerenrator.Models.ExternalApi
{
    public class RecipeAPISettings
    {
        public bool UseHttps { get; set; } = true;
        public string Host { get; set; }
        public int Port { get; set; }

        public override string ToString()
        {
            return $" UseHttps:{UseHttps}, Host:{Host},Port:{Port}";
        }
    }
}
