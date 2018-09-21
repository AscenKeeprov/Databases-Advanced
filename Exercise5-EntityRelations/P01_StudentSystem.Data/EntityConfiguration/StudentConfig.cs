using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
	public void Configure(EntityTypeBuilder<Student> entityBuilder)
	{
	    entityBuilder.ToTable("Students");

	    entityBuilder.HasKey(s => s.StudentId);

	    entityBuilder.Property(s => s.Name)
		.HasMaxLength(100)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(s => s.PhoneNumber)
		.HasColumnType("CHAR(10)")
		.IsUnicode(false)
		.IsRequired(false);

	    entityBuilder.Property(s => s.RegisteredOn)
		.IsRequired(true);

	    entityBuilder.Property(s => s.Birthday)
		.HasColumnType("DATE")
		.IsRequired(false);

	    entityBuilder.HasMany(s => s.CourseEnrollments)
		.WithOne(sc => sc.Student)
		.HasForeignKey(sc => sc.StudentId);

	    entityBuilder.HasMany(s => s.HomeworkSubmissions)
		.WithOne(h => h.Student)
		.HasForeignKey(h => h.StudentId);
	}
    }
}
