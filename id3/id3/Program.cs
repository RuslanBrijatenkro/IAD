using System.Data.Entity;
using id3.DataModel;

namespace id3
{
	class Program
	{
		static void Main(string[] args)
		{
			using (LibraryContext context = new LibraryContext())
			{
				context.SaveChanges();
			}
		}
	}
	public class InputData
	{
		public int Id { get; set; }
		public string Age { get; set; }
		public string SpectaclePrescription { get; set; }
		public string Asastigmatism { get; set; }
		public string Lenses { get; set; }
	}
}
