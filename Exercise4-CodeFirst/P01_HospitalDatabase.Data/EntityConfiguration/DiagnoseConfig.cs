namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_HospitalDatabase.Data.Models;

    public class DiagnoseConfig : IEntityTypeConfiguration<Diagnose>
    {
	public void Configure(EntityTypeBuilder<Diagnose> builder)
	{
	    builder.ToTable("Diagnoses");

	    builder.HasKey(d => d.DiagnoseId);

	    builder.Property(d => d.Name)
		.HasMaxLength(50)
		.IsUnicode(true);

	    builder.Property(d => d.Comments)
		.HasMaxLength(250)
		.IsUnicode(true);
	}
    }
}
