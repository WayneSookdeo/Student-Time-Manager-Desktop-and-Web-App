namespace CalculationsClass
{
    public class Calculations
    {
        private string sCode;
        private string sName;
        private double dHoursWeeks;
        private double dCredits;
        private double dtoCompleteStudy;
        private double dweeks;

        public string SCode { get => sCode; set => sCode = value; }
        public string SName { get => sName; set => sName = value; }
        public double DHoursWeeks { get => dHoursWeeks; set => dHoursWeeks = value; }
        public double DCredits { get => dCredits; set => dCredits = value; }
        public double DtoCompleteStudy { get => dtoCompleteStudy; set => dtoCompleteStudy = value; }
        public double DWeeks { get => dweeks; set => dweeks = value; }
        public double Hourstobestudied()
        {
            return (dCredits * 10 / DWeeks) -DHoursWeeks;
        }
    }
}