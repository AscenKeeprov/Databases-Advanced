using System;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    /// <summary>
    /// Specifies that the property this attribute is applied to and the
    /// property it points to canoot be NULL/NOT NULL at the same time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class XORNULLAttribute : ValidationAttribute
    {
	private const string BothNull = "{0} and {1} cannot be NULL at the same time!";
	private const string NoneNull = "Either {0} or {1} has to be NULL!";

	private string targetAttributeName;

	public XORNULLAttribute(string targetAttributeName)
	{
	    this.targetAttributeName = targetAttributeName;
	}

	protected override ValidationResult IsValid(object sourceAttribute, ValidationContext validationContext)
	{
	    object targetAttribute = validationContext.ObjectType
		.GetProperty(targetAttributeName)
		.GetValue(validationContext.ObjectInstance);
	    string errorMessage = String.Empty;
	    string sourceAttributeName = validationContext.MemberName;
	    if (sourceAttribute == null && targetAttribute == null)
	    {
		errorMessage = String.Format(BothNull, sourceAttributeName, targetAttributeName);
		return new ValidationResult(errorMessage);
	    }
	    if (sourceAttribute != null && targetAttribute != null)
	    {
		errorMessage = String.Format(NoneNull, sourceAttributeName, targetAttributeName);
		return new ValidationResult(errorMessage);
	    }
	    return ValidationResult.Success;
	}
    }
}
