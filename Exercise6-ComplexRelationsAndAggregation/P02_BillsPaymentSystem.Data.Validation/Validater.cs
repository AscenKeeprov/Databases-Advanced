using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace P02_BillsPaymentSystem.Data.Validation
{
    public class Validater
    {
	public static bool IsEntityValid(object entity)
	{
	    var validationContext = new ValidationContext(entity);
	    var validationResults = new List<ValidationResult>();
	    bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);
	    if (!isValid)
	    {
		var failures = validationResults.Where(r => !String.IsNullOrEmpty(r.ErrorMessage));
		foreach (var failure in failures)
		{
		    Console.WriteLine(failure.ErrorMessage);
		}
	    }
	    return isValid;
	}
    }
}
