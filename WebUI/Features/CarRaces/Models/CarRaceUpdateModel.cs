namespace WebUI.Features.CarRaces.Models
{
    public class CarRaceUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Distance { get; set; }
        public int TimeLimit { get; set; }
    }
}
