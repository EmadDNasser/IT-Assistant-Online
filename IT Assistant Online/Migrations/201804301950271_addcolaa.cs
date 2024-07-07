namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolaa : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Sections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
