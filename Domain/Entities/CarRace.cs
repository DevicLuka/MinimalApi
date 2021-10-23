using System.Collections.Generic;

namespace Domain.Entities
{
    public class CarRace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Distance { get; set; }
        public int TimeLimit { get; set; }
        public string Status { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
