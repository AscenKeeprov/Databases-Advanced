using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AcronymAttribute : ValidationAttribute
    {
	private readonly int length;
	private string acronym;

	public AcronymAttribute(int length)
	{
	    this.length = length;
	}

	public override bool IsValid(object value)
	{
	    acronym = value as string;
	    bool isValid = true;
	    if (String.IsNullOrWhiteSpace(acronym)) isValid = false;
	    if (acronym.Length != length) isValid = false;
	    if (acronym.Any(c => !Char.IsUpper(c))) isValid = false;
	    return isValid;
	}

	public override string FormatErrorMessage(string propertyName)
	{
	    return String.Format(Messages.AcronymNotValid, acronym,
		Constants.TeamAcronymLength);
	}
    }
}
