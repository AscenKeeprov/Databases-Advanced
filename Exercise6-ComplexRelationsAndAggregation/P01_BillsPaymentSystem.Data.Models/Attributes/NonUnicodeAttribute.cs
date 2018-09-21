using System;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    /// <summary>
    /// Specifies that the property value cannot contain Unicode characters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NonUnicodeAttribute : ValidationAttribute
    {
	private const string HasUnicode = "{0} contains unicode characters where none are allowed!";

	protected override ValidationResult IsValid(object targetProperty, ValidationContext validationContext)
	{
	    string text = (string)targetProperty;
	    for (int i = 0; i < text.Length; i++)
	    {
		if (text[i] > 255)
		{
		    string errorMessage = String.Format(HasUnicode, validationContext.MemberName);
		    return new ValidationResult(errorMessage);
		}
	    }
	    return ValidationResult.Success;
	}
    }
}
