namespace Url_Shortner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrlPairs", "DateCreate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrlPairs", "DateCreate");
        }
    }
}
