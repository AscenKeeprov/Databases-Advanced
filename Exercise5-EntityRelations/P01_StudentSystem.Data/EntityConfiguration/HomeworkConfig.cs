using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class HomeworkConfig : IEntityTypeConfiguration<Homework>
    {
	public void Configure(EntityTypeBuilder<Homework> entityBuilder)
	{
	    entityBuilder.ToTable("HomeworkSubmissions");

	    entityBuilder.HasKey(h => h.HomeworkId);

	    entityBuilder.Property(h => h.Content)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.Property(h => h.ContentType)
		.IsRequired(true);

	    entityBuilder.Property(h => h.SubmissionTime)
		.IsRequired(true);

	    entityBuilder.Property(h => h.StudentId)
		.IsRequired(true);

	    entityBuilder.Property(h => h.CourseId)
		.IsRequired(true);

	    entityBuilder.HasOne(h => h.Student)
		.WithMany(s => s.HomeworkSubmissions)
		.HasForeignKey(h => h.StudentId);

	    entityBuilder.HasOne(h => h.Course)
		.WithMany(c => c.HomeworkSubmissions)
		.HasForeignKey(h => h.CourseId);
	}
    }
}
