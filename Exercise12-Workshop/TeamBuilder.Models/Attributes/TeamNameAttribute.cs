using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TeamNameAttribute : ValidationAttribute
    {
	private readonly int maxLength;
	private string name;

	public TeamNameAttribute(int maxLength)
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
	    if (name.Any(c => Char.IsSymbol(c))) isValid = false;
	    if (name.Any(c => Char.IsPunctuation(c))) isValid = false;
	    if (name.All(c => !Char.IsLetter(c))) isValid = false;
	    return isValid;
	}

	public override string FormatErrorMessage(string propertyName)
	{
	    return String.Format(Messages.TeamNameNotValid, name,
		Constants.TeamNameMaxLength);
	}
    }
}
