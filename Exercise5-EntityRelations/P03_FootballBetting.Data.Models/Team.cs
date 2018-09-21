﻿using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public class Team
    {
	public Team()
	{
	    Players = new HashSet<Player>();
	    HomeGames = new HashSet<Game>();
	    AwayGames = new HashSet<Game>();
	}

	public int TeamId { get; set; }

	public string Name { get; set; }

	public string LogoUrl { get; set; }

	public string Initials { get; set; }

	public decimal Budget { get; set; }

	public int PrimaryKitColorId { get; set; }
	public virtual Color PrimaryKitColor { get; set; }

	public int SecondaryKitColorId { get; set; }
	public virtual Color SecondaryKitColor { get; set; }

	public int TownId { get; set; }
	public virtual Town Town { get; set; }

	public virtual ICollection<Player> Players { get; set; }
	public virtual ICollection<Game> HomeGames { get; set; }
	public virtual ICollection<Game> AwayGames { get; set; }
    }
}
