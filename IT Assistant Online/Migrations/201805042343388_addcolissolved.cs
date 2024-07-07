namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolissolved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsSolved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsSolved");
        }
    }
}
