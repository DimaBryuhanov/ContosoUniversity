namespace ContosoUniversity.Models
{
    //This class creates a joint Table
    public class CourseAssignment
    {
        //The two Fkeys below create a complex PK
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
