using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UsernameAttribute : ValidationAttribute
    {
	private readonly int minLength;
	private readonly int maxLength;
	private string username;

	public UsernameAttribute(int minLength, int maxLength)
	{
	    this.minLength = minLength;
	    this.maxLength = maxLength;
	}

	public override bool IsValid(object value)
	{
	    username = value as string;
	    bool isValid = true;
	    if (String.IsNullOrWhiteSpace(username)) isValid = false;
	    if (username.Length < minLength || username.Length > maxLength) isValid = false;
	    if (username.Any(c => Char.IsUpper(c))) isValid = false;
	    if (username.Any(c => Char.IsSymbol(c))) isValid = false;
	    if (username.Any(c => Char.IsPunctuation(c))) isValid = false;
	    if (username.All(c => !Char.IsLetter(c))) isValid = false;
	    return isValid;
	}

	public override string FormatErrorMessage(string propertyName)
	{
	    return String.Format(Messages.UsernameNotValid, username,
		Constants.UserUsernameMinLength, Constants.UserUsernameMaxLength);
	}
    }
}
