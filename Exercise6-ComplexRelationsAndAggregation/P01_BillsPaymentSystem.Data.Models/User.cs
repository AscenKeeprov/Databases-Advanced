using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P01_BillsPaymentSystem.Data.Models.Attributes;

namespace P01_BillsPaymentSystem.Data.Models
{
    [Table(nameof(User) + "s")]
    public class User : IComparable<User>
    {
	public User()
	{
	    PaymentMethods = new HashSet<PaymentMethod>();
	}

	[Key]
	public int UserId { get; set; }

	[Required]
	[MaxLength(50)]
	[Column(TypeName = "NVARCHAR(50)")]
	public string FirstName { get; set; }

	[Required]
	[MaxLength(50)]
	[Column(TypeName = "NVARCHAR(50)")]
	public string LastName { get; set; }

	[Required]
	[NonUnicode]
	[MaxLength(80)]
	[Column(TypeName = "VARCHAR(80)")]
	public string Email { get; set; }

	[Required]
	[NonUnicode]
	[MaxLength(25)]
	[Column(TypeName = "VARCHAR(25)")]
	public string Password { get; set; }

	public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }

	public int CompareTo(User other)
	{
	    return Email.CompareTo(other.Email);
	}
    }
}
