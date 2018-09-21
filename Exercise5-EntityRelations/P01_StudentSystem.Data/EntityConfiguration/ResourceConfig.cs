using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
	public void Configure(EntityTypeBuilder<Resource> entityBuilder)
	{
	    entityBuilder.ToTable("Resources");

	    entityBuilder.HasKey(r => r.ResourceId);

	    entityBuilder.Property(r => r.Name)
		.HasMaxLength(50)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(r => r.Url)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.Property(r => r.ResourceType)
		.IsRequired(true);

	    entityBuilder.Property(r => r.CourseId)
		.IsRequired(true);

	    entityBuilder.HasOne(r => r.Course)
		.WithMany(c => c.Resources)
		.HasForeignKey(r => r.ResourceId);
	}
    }
}
