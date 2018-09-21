namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_HospitalDatabase.Data.Models;

    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
	public void Configure(EntityTypeBuilder<Doctor> builder)
	{
	    builder.ToTable("Doctors");

	    builder.HasKey(d => d.DoctorId);

	    builder.Property(d => d.Name)
		.HasMaxLength(100)
		.IsUnicode(true);

	    builder.Property(d => d.Specialty)
		.HasMaxLength(100)
		.IsUnicode(true);

	    builder.HasMany(d => d.Visitations)
		.WithOne(v => v.Doctor)
		.HasForeignKey(v => v.DoctorId);
	}
    }
}
