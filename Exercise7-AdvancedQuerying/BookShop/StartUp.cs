using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new BookShopContext())
	    {
		//DbInitializer.ResetDatabase(context);

		#region /* Problem 1: Age Restriction */
		//string ageRestriction = Console.ReadLine();
		//string bookTitles = GetBooksByAgeRestriction(context, ageRestriction);
		//if (!String.IsNullOrWhiteSpace(bookTitles))
		//    Console.WriteLine(bookTitles);
		#endregion

		#region /* Problem 2: Golden Books */
		//string bookTitles = GetGoldenBooks(context);
		//Console.WriteLine(bookTitles);
		#endregion

		#region /* Problem 3: Books by Price */
		//string booksPrices = GetBooksByPrice(context);
		//Console.WriteLine(booksPrices);
		#endregion

		#region /* Problem 4: Not Released In */
		//int year = int.Parse(Console.ReadLine());
		//string bookTitles = GetBooksNotRealeasedIn(context, year);
		//Console.WriteLine(bookTitles);
		#endregion

		#region /* Problem 5: Book Titles by Category */
		//string input = Console.ReadLine();
		//string bookTitles = GetBooksByCategory(context, input);
		//Console.WriteLine(bookTitles);
		#endregion

		#region /* Problem 6: Released Before Date */
		//string date = Console.ReadLine();
		//string bookInfo = GetBooksReleasedBefore(context, date);
		//Console.WriteLine(bookInfo);
		#endregion

		#region /* Problem 7: Author Search */
		//string input = Console.ReadLine();
		//string authorNames = GetAuthorNamesEndingIn(context, input);
		//Console.WriteLine(authorNames);
		#endregion

		#region /* Problem 8: Book Search */
		//string input = Console.ReadLine();
		//string bookTitles = GetBookTitlesContaining(context, input);
		//Console.WriteLine(bookTitles);
		#endregion

		#region /* Problem 9: Book Search by Author */
		//string input = Console.ReadLine();
		//string booksAuthors = GetBooksByAuthor(context, input);
		//Console.WriteLine(booksAuthors);
		#endregion

		#region /* Problem 10: Count Books */
		//int lengthCheck = int.Parse(Console.ReadLine());
		//int booksCount = CountBooks(context, lengthCheck);
		//Console.WriteLine(booksCount);
		#endregion

		#region /* Problem 11: Total Book Copies */
		//string authorsCopies = CountCopiesByAuthor(context);
		//Console.WriteLine(authorsCopies);
		#endregion

		#region /* Problem 12: Profit by Category */
		//string categoriesProfits = GetTotalProfitByCategory(context);
		//Console.WriteLine(categoriesProfits);
		#endregion

		#region /* Problem 13: Most Recent Books */
		string categoriesTopBooks = GetMostRecentBooks(context);
		Console.WriteLine(categoriesTopBooks);
		#endregion

		#region /* Problem 14: Increase Prices */
		//IncreasePrices(context);
		#endregion

		#region /* Problem 15: Remove Books */
		//int numberOfBooksRemoved = RemoveBooks(context);
		//Console.WriteLine($"{numberOfBooksRemoved} books were deleted");
		#endregion
	    }
	}

	public static string GetBooksByAgeRestriction(BookShopContext context, string command)
	{
	    if (Enum.TryParse(typeof(AgeRestriction), command, true, out object ageRestriction))
	    {
		string[] bookTitles = context.Books
		.Where(b => b.AgeRestriction == (AgeRestriction)ageRestriction)
		.OrderBy(b => b.Title)
		.Select(b => b.Title)
		.ToArray();
		return String.Join(Environment.NewLine, bookTitles);
	    }
	    else return String.Empty;
	}

	public static string GetGoldenBooks(BookShopContext context)
	{
	    string[] bookTitles = context.Books
		.Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
		.OrderBy(b => b.BookId)
		.Select(b => b.Title)
		.ToArray();
	    return String.Join(Environment.NewLine, bookTitles);
	}

	public static string GetBooksByPrice(BookShopContext context)
	{
	    var books = context.Books
		.Where(b => b.Price > 40)
		.OrderByDescending(b => b.Price)
		.Select(b => new
		{
		    b.Title,
		    b.Price
		})
		.ToArray();
	    StringBuilder booksPrices = new StringBuilder();
	    foreach (var book in books)
	    {
		booksPrices.AppendLine($"{book.Title} - ${book.Price:F2}");
	    }
	    return booksPrices.ToString().TrimEnd();
	}

	public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
	{
	    string[] bookTitles = context.Books
		.Where(b => b.ReleaseDate.Value.Year != year)
		.OrderBy(b => b.BookId)
		.Select(b => b.Title)
		.ToArray();
	    return String.Join(Environment.NewLine, bookTitles);
	}

	public static string GetBooksByCategory(BookShopContext context, string input)
	{
	    List<string> categories = input.ToUpper().Split().ToList();
	    string[] bookTitles = context.Books
		.Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToUpper())))
		.OrderBy(b => b.Title)
		.Select(b => b.Title)
		.ToArray();
	    return String.Join(Environment.NewLine, bookTitles);
	}

	public static string GetBooksReleasedBefore(BookShopContext context, string date)
	{
	    DateTime targetDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
	    var booksBeforeDate = context.Books
		.Where(b => b.ReleaseDate < targetDate)
		.OrderByDescending(b => b.ReleaseDate)
		.Select(b => new
		{
		    b.Title,
		    b.EditionType,
		    b.Price
		})
		.ToArray();
	    StringBuilder bookInfo = new StringBuilder();
	    foreach (var book in booksBeforeDate)
	    {
		bookInfo.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
	    }
	    return bookInfo.ToString().TrimEnd();
	}

	public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
	{
	    var authors = context.Authors
		.Where(a => EF.Functions.Like(a.FirstName, $"%{input}"))
		.OrderBy(a => a.FirstName).ThenBy(a => a.LastName)
		.Select(a => new { FullName = $"{a.FirstName} {a.LastName}" })
		.ToArray();
	    StringBuilder authorNames = new StringBuilder();
	    foreach (var author in authors)
	    {
		authorNames.AppendLine(author.FullName);
	    }
	    return authorNames.ToString().TrimEnd();
	}

	public static string GetBookTitlesContaining(BookShopContext context, string input)
	{
	    var books = context.Books
		.Where(b => EF.Functions.Like(b.Title, $"%{input}%"))
		.OrderBy(b => b.Title)
		.Select(b => new { b.Title })
		.ToArray();
	    StringBuilder bookTitles = new StringBuilder();
	    foreach (var book in books)
	    {
		bookTitles.AppendLine(book.Title);
	    }
	    return bookTitles.ToString().TrimEnd();
	}

	public static string GetBooksByAuthor(BookShopContext context, string input)
	{
	    string[] booksAuthors = context.Books
		.Where(b => EF.Functions.Like(b.Author.LastName, $"{input}%"))
		.OrderBy(b => b.BookId)
		.Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
		.ToArray();
	    return String.Join(Environment.NewLine, booksAuthors);
	}

	public static int CountBooks(BookShopContext context, int lengthCheck)
	{
	    int booksCount = context.Books
		.Where(b => b.Title.Length > lengthCheck)
		.Count();
	    return booksCount;
	}

	public static string CountCopiesByAuthor(BookShopContext context)
	{
	    var authorsBooks = context.Authors
		.Select(a => new
		{
		    FullName = $"{a.FirstName} {a.LastName}",
		    CopiesPublished = a.Books.Sum(b => b.Copies)
		})
		.OrderByDescending(a => a.CopiesPublished)
		.ToArray();
	    StringBuilder authorsCopies = new StringBuilder();
	    foreach (var author in authorsBooks)
	    {
		authorsCopies.AppendLine($"{author.FullName} - {author.CopiesPublished}");
	    }
	    return authorsCopies.ToString().TrimEnd();
	}

	public static string GetTotalProfitByCategory(BookShopContext context)
	{
	    var profitsByCategory = context.Categories
		.Select(c => new
		{
		    c.Name,
		    Profits = c.CategoryBooks.Sum(cb => cb.Book.Price * cb.Book.Copies)
		})
		.OrderByDescending(c => c.Profits)
		.ThenBy(c => c.Name)
		.ToArray();
	    StringBuilder categoriesProfits = new StringBuilder();
	    foreach (var category in profitsByCategory)
	    {
		categoriesProfits.AppendLine($"{category.Name} ${category.Profits:F2}");
	    }
	    return categoriesProfits.ToString().TrimEnd();
	}

	public static string GetMostRecentBooks(BookShopContext context)
	{
	    var mostRecentBooksByCategory = context.Categories
		.Select(c => new
		{
		    c.Name,
		    TopBooks = c.CategoryBooks.Select(cb => new
		    {
			cb.Book.Title,
			cb.Book.ReleaseDate
		    })
		    .OrderByDescending(b => b.ReleaseDate)
		    .Take(3)
		})
		.OrderBy(c => c.Name)
		.ToArray();
	    StringBuilder categoriesTopBooks = new StringBuilder();
	    foreach (var category in mostRecentBooksByCategory)
	    {
		categoriesTopBooks.AppendLine($"--{category.Name}");
		foreach (var book in category.TopBooks)
		{
		    categoriesTopBooks.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
		}
	    }
	    return categoriesTopBooks.ToString().TrimEnd();
	}

	public static void IncreasePrices(BookShopContext context)
	{
	    var books = context.Books
		.Where(b => b.ReleaseDate.Value.Year < 2010)
		.ToArray();
	    foreach (var book in books)
	    {
		book.Price += 5M;
	    }
	    context.SaveChanges();
	}

	public static int RemoveBooks(BookShopContext context)
	{
	    var booksToRemove = context.Books
		.Where(b => b.Copies < 4200)
		.ToArray();
	    context.Books.RemoveRange(booksToRemove);
	    int numberOfBooksRemoved = context.ChangeTracker.Entries()
		.Where(e => e.State == EntityState.Deleted).Count();
	    context.SaveChanges();
	    return numberOfBooksRemoved;
	}
    }
}
