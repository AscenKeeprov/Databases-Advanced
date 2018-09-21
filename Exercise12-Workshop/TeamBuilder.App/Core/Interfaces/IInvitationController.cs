namespace TeamBuilder.App.Core.Interfaces
{
    public interface IInvitationController
    {
	void AddInvitation(int teamId, int userId);
	bool InvitationExists(int teamId, int userId);
	void RemoveInvitation(int teamId, int userId);
    }
}
