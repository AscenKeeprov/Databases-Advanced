using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PersonalNameAttribute : ValidationAttribute
    {
	private readonly int maxLength;
	private string name;

	public PersonalNameAttribute(int maxLength)
	{
	    this.maxLength = maxLength;
	}

	public override bool IsValid(object value)
	{
	    name = value as string;
	    bool isValid = true;
	    if (String.IsNullOrWhiteSpace(name)) isValid = false;
	    if (name.Length > maxLength) isValid = false;
	    if (!Char.IsUpper(name[0])) isValid = false;
	    if (name.Skip(1).Any(c => Char.IsUpper(c))) isValid = false;
	    if (name.Any(c => Char.IsDigit(c))) isValid = false;
	    if (name.Any(c => Char.IsSymbol(c))) isValid = false;
	    if (name.All(c => !Char.IsLetter(c))) isValid = false;
	    return isValid;
	}

	public override string FormatErrorMessage(string propertyName)
	{
	    return String.Format(Messages.PersonalNameNotValid, name,
		Constants.UserPersonalNameMaxLength);
	}
    }
}
