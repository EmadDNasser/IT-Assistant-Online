namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addarticlemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articls",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        ArticleImage = c.String(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articls", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Articls", new[] { "UserID" });
            DropTable("dbo.Articls");
        }
    }
}
