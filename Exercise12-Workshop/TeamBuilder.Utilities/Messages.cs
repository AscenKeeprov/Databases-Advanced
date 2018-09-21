namespace TeamBuilder.Utilities
{
    public static class Messages
    {
	#region /* ERROR MESSAGES */
	public const string AcronymNotValid =
	    " Acronym '{0}' is not valid! Acronym must consist of {1} upper-case letters.";
	public const string AcronymTaken = " Acronym '{0}' is already taken!";
	public const string AgeNotValidNotNumber = " Age not valid! Age must be a whole number!";
	public const string AgeNotValidOutOfRange = " Age not valid! Age must be between {0} and {1}";
	public const string CommandNotSupported = " Command '{0}' is not supported!";
	public const string GenderNotValid = " Gender not valid! Gender must be either 'Male' or 'Female'!";
	public const string IncorrectPassword = " Incorrect password!";
	public const string InvalidArgumentsCount = " Invalid arguments count!";
	public const string InvitationExists = " Team '{0}' has already invited user '{1}' to join them!";
	public const string InvitationNotExist = " There is no invitation from team '{0}' for user '{1}'!";
	public const string LoginFirst = " You should login first!";
	public const string LogoutFirst = " You should logout first!";
	public const string PasswordMismatch = " Passwords do not match!";
	public const string PasswordNotValid =
	    " Password '{0}' is not valid! Password must follow these rules:\r\n" +
	    " -Contains at least one lower-case letter\r\n" +
	    " -Contains at least one upper-case letter\r\n" +
	    " -Contains at least one digit\r\n" +
	    " -Length is between {1} and {2} characters";
	public const string PersonalNameNotValid =
	    " Name '{0}' is not valid! Name must follow these rules:\r\n" +
	    " -Starts with an upper-case letter, followed by lower-case letters\r\n" +
	    " -Contains no special symbols except dashes and apostrophes\r\n" +
	    " -Contains no digits\r\n" +
	    " -Length cannot exceed {1} characters";
	public const string TeamExists = " Team '{0}' already exists!";
	public const string TeamNameNotValid =
	    " Name '{0}' is not valid! Name must follow these rules:\r\n" +
	    " -Starts with an upper-case letter, followed by lower-case letters and digits\r\n" +
	    " -Contains no special symbols or punctuation signs\r\n" +
	    " -Length cannot exceed {1} characters";
	public const string TeamNotExist = " Team '{0}' does not exist!";
	public const string UserAlreadyMember = " User '{0}' is already a member of team '{1}'!";
	public const string UserCannotDisbandTeam =
	    " User '{0}' is not allowed to disband team '{1}'! Only team creator can do that.";
	public const string UserCannotKickMembers =
	    " User '{0}' is not allowed to kick members of team '{1}'! Only team creator can do that.";
	public const string UserCannotKickSelf =
	    " Inappropriate command! Leader '{0}' trying to kick themselves from team '{1}'.\r\n" +
	    " Please use command 'DisbandTeam' instead.";
	public const string UserCannotSendInvite =
	    " User '{0}' is not allowed to send invitations on behalf of team '{1}'! They are not part of the team.";
	public const string UsernameNotValid =
	    " Username '{0}' is not valid! Username must follow these rules:\r\n" +
	    " -Consists of lower-case letters and digits only\r\n" +
	    " -Length is between {1} and {2} characters";
	public const string UsernameTaken = " Username '{0}' is already taken!";
	public const string UserNotExist = " User '{0}' does not exist!";
	#endregion
	#region /* SUCCESS MESSAGES */
	public const string InvitationAccepted = " User '{0}' joined team '{1}'!";
	public const string InvitationDeclined = " User '{0}' declined an invitation from team '{1}'!";
	public const string InvitationSent = " Team '{0}' invited '{1}'!";
	public const string TeamCreated = " Team '{0}' created successfully!";
	public const string TeamDisbanded = " Team '{0}' has disbanded!";
	public const string TeamMemberKicked = " User '{0}' was kicked from team '{1}'!";
	public const string UserDeleted = " User '{0}' deleted successfully!";
	public const string UserRegistered = " User '{0}' registered successfully!";
	public const string UserLoggedIn = " User '{0}' successfully logged in!";
	public const string UserLoggedOut = " User '{0}' successfully logged out!";
	#endregion
    }
}
