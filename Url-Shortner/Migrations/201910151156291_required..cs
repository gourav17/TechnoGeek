namespace Url_Shortner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UrlPairs", "shortURL", c => c.String(nullable: false));
            AlterColumn("dbo.UrlPairs", "longURL", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UrlPairs", "longURL", c => c.String());
            AlterColumn("dbo.UrlPairs", "shortURL", c => c.String());
        }
    }
}
