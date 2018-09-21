namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class TagAttribute : ValidationAttribute
    {
	private const string TagPattern = "#[a-zA-Z0-9]{2,20}";

	public override bool IsValid(object value)
	{
	    if (!Regex.IsMatch(value.ToString(), TagPattern))
	    {
		return false;
	    }
	    return true;
	}
    }
}
