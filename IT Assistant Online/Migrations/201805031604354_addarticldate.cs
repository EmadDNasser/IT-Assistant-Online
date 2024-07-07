namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addarticldate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articls", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articls", "Date");
        }
    }
}
