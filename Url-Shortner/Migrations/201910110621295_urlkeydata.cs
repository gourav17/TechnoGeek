namespace Url_Shortner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlkeydata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrlPairs",
                c => new
                    {
                        urlID = c.Int(nullable: false, identity: true),
                        shortURL = c.String(),
                        longURL = c.String(),
                    })
                .PrimaryKey(t => t.urlID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UrlPairs");
        }
    }
}
