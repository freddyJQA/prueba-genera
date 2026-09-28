namespace App.Models
{
    public class Clocking
    {
        public int WorkerId { get; set; }

        public DateTime DateAndTime {  get; set; }

        public required string Type { get; set; }

        public required string Origen {  get; set; }
    }
}
