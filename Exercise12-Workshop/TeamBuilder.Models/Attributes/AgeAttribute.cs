using System;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AgeAttribute : ValidationAttribute
    {
	private readonly int minValue;
	private readonly int maxValue;
	private int? age;

	public AgeAttribute(int minValue, int maxValue)
	{
	    this.minValue = minValue;
	    this.maxValue = maxValue;
	}

	public override bool IsValid(object value)
	{
	    age = value as int?;
	    bool isValid = true;
	    if (age == null) isValid = false;
	    if (age < minValue || age > maxValue) isValid = false;
	    return isValid;
	}

	public override string FormatErrorMessage(string propertyName)
	{
	    return String.Format(Messages.AgeNotValidOutOfRange,
		Constants.UserAgeMinValue, Constants.UserAgeMaxValue);
	}
    }
}
