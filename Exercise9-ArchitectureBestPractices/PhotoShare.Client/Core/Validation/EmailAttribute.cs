namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class EmailAttribute : ValidationAttribute
    {
	private const string EmailPattern = @"^[^@]*\@[^@]*$";

	public override bool IsValid(object value)
	{
	    string email = value as string;
	    bool isEmailValid = true;
	    if (String.IsNullOrEmpty(email)) isEmailValid = false;
	    if (!Regex.IsMatch(email, EmailPattern)) isEmailValid = false;
	    return isEmailValid;
	}
    }
}
