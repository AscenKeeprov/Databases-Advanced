using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Attributes;
using TeamBuilder.Utilities;

namespace TeamBuilder.Models
{
    public class Team
    {
	public Team()
	{
	    InvitationsSent = new HashSet<Invitation>();
	    Members = new HashSet<UserTeam>();
	    EventsAttended = new HashSet<EventTeam>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[TeamName(Constants.TeamNameMaxLength)]
	public string Name { get; set; }

	[MaxLength(32)]
	[StringLength(maximumLength: 32)]
	public string Description { get; set; }

	[Required]
	[Acronym(Constants.TeamAcronymLength)]
	public string Acronym { get; set; }

	public bool IsDisbanded { get; set; }

	public int CreatorId { get; set; }
	public virtual User Creator { get; set; }

	public virtual ICollection<Invitation> InvitationsSent { get; set; }
	public virtual ICollection<UserTeam> Members { get; set; }
	public virtual ICollection<EventTeam> EventsAttended { get; set; }
    }
}
