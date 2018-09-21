using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class Event
    {
	public Event()
	{
	    TeamsParticipating = new HashSet<EventTeam>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(25)]
	[StringLength(maximumLength: 25)]
	public string Name { get; set; }

	[MaxLength(250)]
	[StringLength(maximumLength: 250)]
	public string Description { get; set; }

	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }

	public int CreatorId { get; set; }
	public virtual User Creator { get; set; }

	public virtual ICollection<EventTeam> TeamsParticipating { get; set; }
    }
}
