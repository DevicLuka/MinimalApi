namespace WebUI.Features.MotorbikeRaces.Models
{
    public class MotorbikeRaceUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Distance { get; set; }
        public int TimeLimit { get; set; }
    }
}
