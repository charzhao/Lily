namespace Lily.Microservice.Microparts.Imp
{
    public class MicropartStatusInfo
    {
        public string MicroPartInstanceName { get; set; }
        public bool IsEnabled { get; set; }

        public bool IsInitSuccessed { get; set; }

        public bool IsWrokingNow { get; set; }

        public string Comments { get; set; }
    }
}
