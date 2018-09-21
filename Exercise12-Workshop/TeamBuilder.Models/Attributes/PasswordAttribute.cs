using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : ValidationAttribute
    {
	private readonly int minLength;
	private readonly int maxLength;
	private string password;

	public PasswordAttribute(int minLength, int maxLength)
	{
	    this.minLength = minLength;
	    this.maxLength = maxLength;
	}

	public override bool IsValid(object value)
	{
	    password = value as string;
	    bool isValid = true;
	    if (String.IsNullOrWhiteSpace(password)) isValid = false;
	    if (password.Length < minLength || password.Length > maxLength) isValid = false;
	    if (!password.Any(c => Char.IsLower(c))) isValid = false;
	    if (!password.Any(c => Char.IsUpper(c))) isValid = false;
	    if (!password.Any(c => Char.IsDigit(c))) isValid = false;
	    return isValid;
	}

	public override string FormatErrorMessage(string propertyName)
	{
	    return String.Format(Messages.PasswordNotValid, password,
		Constants.PasswordMinLength, Constants.PasswordMaxLength);
	}
    }
}
