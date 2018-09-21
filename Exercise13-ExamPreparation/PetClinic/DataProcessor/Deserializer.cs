namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.Data.DataTransferObjects;
    using PetClinic.Models;

    public class Deserializer
    {
	private const string ErrorMessage = "Error: Invalid data.";
	private const string SuccessMessage = "Record{0} successfully imported.";

	public static string ImportAnimalAids(PetClinicContext context, string jsonString)
	{
	    StringBuilder output = new StringBuilder();
	    var animalAidDtos = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);
	    HashSet<AnimalAid> animalAids = new HashSet<AnimalAid>();
	    foreach (var animalAidDto in animalAidDtos)
	    {
		if (!IsObjectValid(animalAidDto) || animalAids.Any(aa => aa.Name == animalAidDto.Name))
		{
		    output.AppendLine(ErrorMessage);
		}
		else
		{
		    AnimalAid animalAid = Mapper.Map<AnimalAid>(animalAidDto);
		    animalAids.Add(animalAid);
		    output.AppendLine(String.Format(SuccessMessage, $" {animalAid.Name}"));
		}
	    }
	    context.AnimalAids.AddRange(animalAids);
	    context.SaveChanges();
	    return output.ToString().TrimEnd();
	}

	public static string ImportAnimals(PetClinicContext context, string jsonString)
	{
	    StringBuilder output = new StringBuilder();
	    var animalDtos = JsonConvert.DeserializeObject<AnimalImportDto[]>(jsonString);
	    HashSet<Animal> animals = new HashSet<Animal>();
	    foreach (var animalDto in animalDtos)
	    {
		if (!IsObjectValid(animalDto) || !IsObjectValid(animalDto.Passport)
		    || animals.Any(a => a.PassportSerialNumber == animalDto.Passport.SerialNumber))
		{
		    output.AppendLine(ErrorMessage);
		}
		else
		{
		    Animal animal = Mapper.Map<Animal>(animalDto);
		    animals.Add(animal);
		    output.AppendLine(String.Format(SuccessMessage, $" {animal.Name} Passport №: {animal.PassportSerialNumber}"));
		}
	    }
	    context.Animals.AddRange(animals);
	    context.SaveChanges();
	    return output.ToString().TrimEnd();
	}

	public static string ImportVets(PetClinicContext context, string xmlString)
	{
	    StringBuilder output = new StringBuilder();
	    var serializer = new XmlSerializer(typeof(VetImportDto[]), new XmlRootAttribute("Vets"));
	    var vetDtos = (VetImportDto[])serializer.Deserialize(new StringReader(xmlString));
	    HashSet<Vet> vets = new HashSet<Vet>();
	    foreach (var vetDto in vetDtos)
	    {
		if (!IsObjectValid(vetDto) || vets.Any(v => v.PhoneNumber == vetDto.PhoneNumber))
		{
		    output.AppendLine(ErrorMessage);
		}
		else
		{
		    Vet vet = Mapper.Map<Vet>(vetDto);
		    vets.Add(vet);
		    output.AppendLine(String.Format(SuccessMessage, $" {vet.Name}"));
		}
	    }
	    context.Vets.AddRange(vets);
	    context.SaveChanges();
	    return output.ToString().TrimEnd();
	}

	public static string ImportProcedures(PetClinicContext context, string xmlString)
	{
	    StringBuilder output = new StringBuilder();
	    var serializer = new XmlSerializer(typeof(ProcedureImportDto[]), new XmlRootAttribute("Procedures"));
	    var procedureDtos = (ProcedureImportDto[])serializer.Deserialize(new StringReader(xmlString));
	    HashSet<Procedure> procedures = new HashSet<Procedure>();
	    foreach (var procedureDto in procedureDtos)
	    {
		if (!IsObjectValid(procedureDto))
		{
		    output.AppendLine(ErrorMessage);
		    continue;
		}
		Vet vet = context.Vets.SingleOrDefault(v => v.Name == procedureDto.VetName);
		if (vet == null)
		{
		    output.AppendLine(ErrorMessage);
		    continue;
		}
		Animal animal = context.Animals.SingleOrDefault(a
		    => a.PassportSerialNumber == procedureDto.AnimalPassportSerialNumber);
		if (animal == null)
		{
		    output.AppendLine(ErrorMessage);
		    continue;
		}
		Procedure procedure = new Procedure()
		{
		    Vet = vet,
		    Animal = animal,
		    DateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture)
		};
		HashSet<ProcedureAnimalAid> procedureAnimalAids = new HashSet<ProcedureAnimalAid>();
		bool hasInvalidAnimalAids = false;
		foreach (var animalAidDto in procedureDto.AnimalAids)
		{
		    AnimalAid animalAid = context.AnimalAids.SingleOrDefault(aa => aa.Name == animalAidDto.Name);
		    if (animalAid == null || procedureAnimalAids.Any(paa => paa.AnimalAid.Name == animalAidDto.Name))
		    {
			hasInvalidAnimalAids = true;
			break;
		    }
		    else
		    {
			ProcedureAnimalAid procedureAnimalAid = new ProcedureAnimalAid()
			{
			    AnimalAid = animalAid,
			    Procedure = procedure
			};
			procedureAnimalAids.Add(procedureAnimalAid);
		    }
		}
		if (hasInvalidAnimalAids)
		{
		    output.AppendLine(ErrorMessage);
		    continue;
		}
		procedure.ProcedureAnimalAids = procedureAnimalAids;
		procedures.Add(procedure);
		output.AppendLine(String.Format(SuccessMessage, String.Empty));
	    }
	    context.Procedures.AddRange(procedures);
	    context.SaveChanges();
	    return output.ToString().TrimEnd();
	}

	private static bool IsObjectValid(object obj)
	{
	    var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
	    var validationResults = new List<ValidationResult>();
	    bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
	    return isValid;
	}
    }
}
