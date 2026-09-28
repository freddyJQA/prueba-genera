using System.Globalization;

namespace App.Models
{
    public class ClockingCsv
    {
        public string? Rut { get; set; }

        public string? FechaHora {  get; set; }

        public string? Type { get; set; }

        public string? Origen {  get; set; }

        public static Clocking MapToClocking(ClockingCsv clockingCsv, int workerId)
        {
            return new Clocking()
            {
                WorkerId = workerId,
                DateAndTime = DateTime.ParseExact(
                    clockingCsv.FechaHora!, 
                    "yyyy-MM-dd HH:mm:ss", 
                    CultureInfo.InvariantCulture),
                Type = clockingCsv.Type!,
                Origen = clockingCsv.Origen!
            };
        }
    }
}
