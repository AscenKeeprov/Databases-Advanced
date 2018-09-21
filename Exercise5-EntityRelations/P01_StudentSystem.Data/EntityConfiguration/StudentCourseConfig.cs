using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
	public void Configure(EntityTypeBuilder<StudentCourse> entityBuilder)
	{
	    entityBuilder.ToTable("StudentCourses");

	    entityBuilder.HasKey(sc => new { sc.StudentId, sc.CourseId });

	    entityBuilder.Property(sc => sc.StudentId)
		.IsRequired(true);

	    entityBuilder.Property(sc => sc.CourseId)
		.IsRequired(true);

	    entityBuilder.HasOne(sc => sc.Student)
		.WithMany(s => s.CourseEnrollments)
		.HasForeignKey(sc => sc.StudentId);
	}
    }
}
