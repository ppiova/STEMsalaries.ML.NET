using Microsoft.ML.Data;

namespace STEMsalaries
{
    public class Salary
    {
        [ColumnName("Label"), LoadColumn(0)]
        public float AnualCompensation { get; set; }

        [LoadColumn(1)]
        public string Company { get; set; }

        [LoadColumn(2)]
        public string Title { get; set; }

        [LoadColumn(3)]
        public float YearsExperience { get; set; }

        [LoadColumn(4)]
        public float YearsCompany { get; set; }

        [LoadColumn(5)]
        public string Gender { get; set; }

        [LoadColumn(6)]
        public string Location { get; set; }

        [LoadColumn(7)]
        public string Race { get; set; }

        [LoadColumn(8)]
        public string Education { get; set; }
    }
}
