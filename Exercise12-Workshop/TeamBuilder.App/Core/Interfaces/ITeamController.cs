using TeamBuilder.Data.DataTransferObjects;

namespace TeamBuilder.App.Core.Interfaces
{
    public interface ITeamController
    {
	bool AcronymInUse(string acronym);
	void AddMember(int teamId, int userId);
	void CreateTeam(string name, string acronym, string description, UserDto creator);
	void DisbandTeam(int teamId);
	TeamDto GetTeam(string teamName);
	string GetTeamInfo(TeamDto teamDto);
	bool IsCreator(TeamDto teamDto, int userId);
	bool IsMember(TeamDto teamDto, int userId);
	void RemoveMember(int teamId, int userId);
	bool TeamExists(string teamName);
    }
}
