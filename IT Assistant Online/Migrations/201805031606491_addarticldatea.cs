namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addarticldatea : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articls", "Date", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articls", "Date", c => c.String());
        }
    }
}
