namespace PetClinic.DataProcessor
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.Data.DataTransferObjects;

    public class Serializer
    {
	public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
	{
	    var animals = context.Passports
		.Where(p => p.OwnerPhoneNumber == phoneNumber)
		.OrderBy(p => p.Animal.Age)
		.ThenBy(p => p.SerialNumber)
		.Select(p => new
		{
		    p.OwnerName,
		    AnimalName = p.Animal.Name,
		    p.Animal.Age,
		    p.SerialNumber,
		    RegisteredOn = p.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
		}).ToArray();
	    string output = JsonConvert.SerializeObject(animals, new JsonSerializerSettings()
	    {
		Formatting = Newtonsoft.Json.Formatting.Indented,
		NullValueHandling = NullValueHandling.Ignore
	    });
	    return output;
	}

	public static string ExportAllProcedures(PetClinicContext context)
	{
	    var procedures = context.Procedures
		.OrderBy(p => p.DateTime)
		.ThenBy(p => p.Animal.PassportSerialNumber)
		.Select(p => new ProcedureExportDto()
		{
		    PassportSerialNumber = p.Animal.PassportSerialNumber,
		    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
		    DateTime = p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
		    AnimalAids = p.ProcedureAnimalAids.Select(paa => new AnimalAidDto()
		    {
			Name = paa.AnimalAid.Name,
			Price = paa.AnimalAid.Price
		    }).ToArray(),
		    TotalPrice = p.ProcedureAnimalAids.Sum(paa => paa.AnimalAid.Price)
		}).ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(ProcedureExportDto[]), new XmlRootAttribute("Procedures"));
	    StringWriter writer = new StringWriter();
	    serializer.Serialize(writer, procedures, serializerNamespaces);
	    string output = writer.ToString();
	    return output;
	}
    }
}
