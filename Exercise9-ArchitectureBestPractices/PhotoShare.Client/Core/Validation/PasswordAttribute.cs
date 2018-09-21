namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class PasswordAttribute : ValidationAttribute
    {
	private const string SpecialSymbols = "!@#$%^&*()_+<>,.?";
	private readonly int minLength;
	private readonly int maxLength;

	public PasswordAttribute(int minLength, int maxLength)
	{
	    this.minLength = minLength;
	    this.maxLength = maxLength;
	}

	public override bool IsValid(object value)
	{
	    string password = value as string;
	    bool isPasswordValid = true;
	    if (String.IsNullOrWhiteSpace(password)) isPasswordValid = false;
	    if (password.Length < minLength || password.Length > maxLength) isPasswordValid = false;
	    if (!password.Any(c => Char.IsLower(c))) isPasswordValid = false;
	    if (!password.Any(c => Char.IsUpper(c))) isPasswordValid = false;
	    if (!password.Any(c => Char.IsDigit(c))) isPasswordValid = false;
	    if (!password.Any(c => SpecialSymbols.Contains(c))) isPasswordValid = false;
	    return isPasswordValid;
	}
    }
}
