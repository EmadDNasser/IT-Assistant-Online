namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Location2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Message");
        }
    }
}
