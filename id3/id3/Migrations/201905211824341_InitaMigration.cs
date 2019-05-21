namespace id3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitaMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InputDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Age = c.String(),
                        SpectaclePrescription = c.String(),
                        Asastigmatism = c.String(),
                        Lenses = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Books");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.BookId);
            
            DropTable("dbo.InputDatas");
        }
    }
}
