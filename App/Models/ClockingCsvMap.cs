using CsvHelper.Configuration;

namespace App.Models
{
    public class ClockingCsvMap : ClassMap<Clocking>
    {
        public ClockingCsvMap()
        {
            Map(x => x.Rut).Name("rut");
            Map(x => x.FechaHora).Name("fecha_hora");
            Map(x => x.Type).Name("tipo");
            Map(x => x.Origen).Name("origen");
        }
    }
}
