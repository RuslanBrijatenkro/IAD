namespace id3.DataModel
{
	using System;
	using System.Data.Entity;
	using System.Linq;

	public class LibraryContext : DbContext
	{
		public LibraryContext()
			: base("name=LibraryContext")
		{
		}
		public DbSet<InputData> Data { get; set; }
	}
}