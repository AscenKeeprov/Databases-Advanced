using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Controllers
{
    public class TeamController : ITeamController
    {
	private readonly TeamBuilderDbContext dbContext;

	public TeamController(TeamBuilderDbContext dbContext)
	{
	    this.dbContext = dbContext;
	}

	public bool AcronymInUse(string acronym)
	{
	    return dbContext.Teams.Where(t => !t.IsDisbanded)
		.Any(t => t.Acronym == acronym);
	}

	public void AddMember(int teamId, int userId)
	{
	    UserTeam membership = new UserTeam()
	    {
		UserId = userId,
		TeamId = teamId
	    };
	    dbContext.UserTeams.Add(membership);
	    dbContext.SaveChanges();
	}

	public void CreateTeam(string name, string acronym, string description, UserDto creator)
	{
	    Team team = new Team()
	    {
		Name = name,
		Acronym = acronym,
		Description = description,
		CreatorId = creator.Id
	    };
	    if (IsValid(team))
	    {
		dbContext.Teams.Add(team);
		dbContext.SaveChanges();
		AddMember(team.Id, creator.Id);
		dbContext.SaveChanges();
	    }
	}

	public void DisbandTeam(int teamId)
	{
	    Team team = dbContext.Teams.Find(teamId);
	    team.IsDisbanded = true;
	    UserTeam[] memberships = dbContext.UserTeams
		.Where(m => m.TeamId == teamId).ToArray();
	    dbContext.UserTeams.RemoveRange(memberships);
	    dbContext.SaveChanges();
	}

	public TeamDto GetTeam(string teamName)
	{
	    Team team = dbContext.Teams.FirstOrDefault(t
		=> t.Name == teamName && !t.IsDisbanded);
	    if (team != null)
	    {
		TeamDto teamDto = new TeamDto()
		{
		    Id = team.Id,
		    Name = team.Name,
		    IsDisbanded = team.IsDisbanded
		};
		return teamDto;
	    }
	    else return null;
	}

	public string GetTeamInfo(TeamDto teamDto)
	{
	    Team team = dbContext.Teams.Find(teamDto.Id);
	    StringBuilder teamInfo = new StringBuilder();
	    teamInfo.AppendLine($"{team.Name} [{team.Acronym}]");
	    teamInfo.Append("Members:");
	    if (team.Members.Count > 0)
	    {
		teamInfo.AppendLine();
		foreach (var membership in team.Members)
		{
		    teamInfo.AppendLine($"--{membership.User.Username}");
		}
	    }
	    else teamInfo.AppendLine(" None");
	    return teamInfo.ToString().TrimEnd();
	}

	public bool IsCreator(TeamDto teamDto, int userId)
	{
	    Team team = dbContext.Teams.Find(teamDto.Id);
	    return team.CreatorId == userId;
	}

	public bool IsMember(TeamDto teamDto, int userId)
	{
	    Team team = dbContext.Teams.Find(teamDto.Id);
	    return team.Members.Any(m => m.UserId == userId);
	}

	public void RemoveMember(int teamId, int userId)
	{
	    UserTeam membership = dbContext.UserTeams
		.SingleOrDefault(m => m.TeamId == teamId && m.UserId == userId);
	    dbContext.UserTeams.Remove(membership);
	    dbContext.SaveChanges();
	}

	public bool TeamExists(string teamName)
	{
	    return dbContext.Teams.Any(t => t.Name == teamName && !t.IsDisbanded);
	}

	private static bool IsValid(Team team)
	{
	    var validationContext = new ValidationContext(team);
	    var validationResults = new List<ValidationResult>();
	    bool isValid = Validator.TryValidateObject(team, validationContext, validationResults, true);
	    if (!isValid) throw new ArgumentException(validationResults.First().ErrorMessage);
	    return isValid;
	}
    }
}
