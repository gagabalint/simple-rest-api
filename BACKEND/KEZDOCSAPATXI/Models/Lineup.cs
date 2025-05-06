namespace KEZDOCSAPATXI.Models
{
    public class Lineup
    {
        public string Formation  { get; set; }
        public List<(Player,string AssignedPositions) > Players { get; set; }
        public int Rating { get; set; }
    }
}
