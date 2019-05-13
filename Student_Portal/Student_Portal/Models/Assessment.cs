using SQLite;

namespace Student_Portal.Models
{
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public AssessmentType Type { get; set; }
    }
}
