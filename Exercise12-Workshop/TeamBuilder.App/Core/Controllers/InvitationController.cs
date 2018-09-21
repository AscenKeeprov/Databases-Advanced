using System.Linq;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Controllers
{
    public class InvitationController : IInvitationController
    {
	private readonly TeamBuilderDbContext dbContext;

	public InvitationController(TeamBuilderDbContext dbContext)
	{
	    this.dbContext = dbContext;
	}

	public void AddInvitation(int teamId, int userId)
	{
	    Invitation invitation = new Invitation()
	    {
		TeamId = teamId,
		InvitedUserId = userId,
		IsActive = true
	    };
	    dbContext.Invitations.Add(invitation);
	    dbContext.SaveChanges();
	}

	public bool InvitationExists(int teamId, int userId)
	{
	    return dbContext.Invitations.Where(i => i.IsActive)
		.Any(i => i.TeamId == teamId && i.InvitedUserId == userId);
	}

	public void RemoveInvitation(int teamId, int userId)
	{
	    Invitation invitation = dbContext.Invitations.Where(i => i.IsActive)
		.SingleOrDefault(i => i.TeamId == teamId && i.InvitedUserId == userId);
	    invitation.IsActive = false;
	    dbContext.SaveChanges();
	}
    }
}
