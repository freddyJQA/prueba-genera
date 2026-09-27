namespace App.Models
{
    public class ClockingCsvValidatorResult
    {
        public ClockingCsv ClockingCsv { get; }

        public List<string> Errors { get; } = [];

        public bool IsValid => Errors.Count == 0;

        public ClockingCsvValidatorResult(ClockingCsv clockingCsv) => ClockingCsv = clockingCsv;
    }
}
