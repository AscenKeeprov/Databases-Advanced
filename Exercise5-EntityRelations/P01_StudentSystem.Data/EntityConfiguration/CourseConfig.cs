using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
	public void Configure(EntityTypeBuilder<Course> entityBuilder)
	{
	    entityBuilder.ToTable("Courses");

	    entityBuilder.HasKey(c => c.CourseId);

	    entityBuilder.Property(c => c.Name)
		.HasMaxLength(80)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(c => c.Description)
		.IsUnicode(true)
		.IsRequired(false);

	    entityBuilder.Property(c => c.StartDate)
		.IsRequired(true);

	    entityBuilder.Property(c => c.EndDate)
		.IsRequired(true);

	    entityBuilder.Property(c => c.Price)
		.IsRequired(true);

	    entityBuilder.HasMany(c => c.StudentsEnrolled)
		.WithOne(sc => sc.Course)
		.HasForeignKey(sc => sc.CourseId);

	    entityBuilder.HasMany(c => c.Resources)
		.WithOne(r => r.Course)
		.HasForeignKey(r => r.CourseId);

	    entityBuilder.HasMany(c => c.HomeworkSubmissions)
		.WithOne(h => h.Course)
		.HasForeignKey(h => h.CourseId);
	}
    }
}
