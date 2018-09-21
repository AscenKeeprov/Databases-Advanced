using TeamBuilder.App.Core;

namespace TeamBuilder.App
{
    public class Startup
    {
	public static void Main()
	{
	    Server server = new Server();
	    server.Run();
	}
    }
}
