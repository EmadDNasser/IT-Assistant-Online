namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclassess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Contenet = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Contenet = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PostID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.PostID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.CategoryPosts",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Post_ID })
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Category_ID)
                .Index(t => t.Post_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.CategoryPosts", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.CategoryPosts", "Category_ID", "dbo.Categories");
            DropIndex("dbo.CategoryPosts", new[] { "Post_ID" });
            DropIndex("dbo.CategoryPosts", new[] { "Category_ID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropTable("dbo.CategoryPosts");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Posts");
        }
    }
}
