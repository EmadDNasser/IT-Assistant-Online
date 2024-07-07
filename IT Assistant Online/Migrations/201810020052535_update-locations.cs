namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelocations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Message", c => c.String());
        }
    }
}
