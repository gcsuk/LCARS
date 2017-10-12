namespace LCARS.ViewModels.Environments
{
    public class Environment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string StatusColour
        {
            get
            {
                switch (Status)
                {
                    case "OK":
                        return "statusBlue";
                    case "ISSUES":
                        return "statusAmber";
                    case "DOWN":
                        return "statusRed";
                    default:
                        return "statusWhite";
                }
            }
        }
    }
}