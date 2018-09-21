using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Attributes;
using TeamBuilder.Models.Enumerations;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models
{
    public class User
    {
	public User()
	{
	    TeamsCreated = new HashSet<Team>();
	    InvitationsReceived = new HashSet<Invitation>();
	    TeamsJoined = new HashSet<UserTeam>();
	    EventsCreated = new HashSet<Event>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[Username(Constants.UserUsernameMinLength, Constants.UserUsernameMaxLength)]
	public string Username { get; set; }

	[PersonalName(Constants.UserPersonalNameMaxLength)]
	public string FirstName { get; set; }

	[PersonalName(Constants.UserPersonalNameMaxLength)]
	public string LastName { get; set; }

	[Required]
	[Password(Constants.PasswordMinLength, Constants.PasswordMaxLength)]
	public string Password { get; set; }

	public Gender Gender { get; set; }

	[Age(Constants.UserAgeMinValue, Constants.UserAgeMaxValue)]
	public int Age { get; set; }

	public bool IsDeleted { get; set; }

	public virtual ICollection<Team> TeamsCreated { get; set; }
	public virtual ICollection<Invitation> InvitationsReceived { get; set; }
	public virtual ICollection<UserTeam> TeamsJoined { get; set; }
	public virtual ICollection<Event> EventsCreated { get; set; }
    }
}
