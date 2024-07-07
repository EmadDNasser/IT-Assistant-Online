namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Location3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Section", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Section");
        }
    }
}
