namespace id3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<id3.DataModel.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "id3.DataModel.LibraryContext";
        }

        protected override void Seed(id3.DataModel.LibraryContext context)
        {
        }
    }
}
