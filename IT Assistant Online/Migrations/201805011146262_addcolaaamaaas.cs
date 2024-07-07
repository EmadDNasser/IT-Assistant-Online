namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolaaamaaas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sections", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sections", "Name", c => c.String());
        }
    }
}
