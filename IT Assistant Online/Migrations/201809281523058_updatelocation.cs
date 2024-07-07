namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "IsRead");
        }
    }
}
