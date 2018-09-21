using P01_BillsPaymentSystem.Data.Models;

namespace P02_BillsPaymentSystem.Data.Initialization
{
    public class UserGenerator
    {
	public static User[] GenerateUsers()
	{
	    User[] users = new User[]
	    {
		new User() {FirstName = "Kubrat", LastName = "Dulov", Email = "kuber1977@abv.bg", Password = "#voluDtarbuK1977#"},
		new User() {FirstName = "John", LastName = "Doe", Email = "johnyD@gmail.com", Password = @"\_J!iOo0_/"},
		new User() {FirstName = "d'Nabû-kudurri-uṣur", LastName = "III", Email = "annointed@simurgh.ba", Password = "X>8B-L7L4W1=E"},
		new User() {FirstName = "小金 Xiao Jin", LastName = "谭 Tan", Email = "xijita@yahoo.hk", Password = "Aura_acacia(|)50ng"},
		new User() {FirstName = "Massimiliano", LastName = "Galardi", Email = "totti4ever@mail.com", Password = @"GooolGolGolGol\o/"},
		new User() {FirstName = "Jane", LastName = "Doe", Email = "jane.doe@hotmail.com", Password = "D8e,G-)9"}
	    };
	    return users;
	}
    }
}
