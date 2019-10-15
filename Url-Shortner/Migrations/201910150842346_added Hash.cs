namespace Url_Shortner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrlPairs", "urlHash", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrlPairs", "urlHash");
        }
    }
}
