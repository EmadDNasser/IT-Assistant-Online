namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcol : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogArticls", "Blog_id", "dbo.Blogs");
            DropForeignKey("dbo.BlogArticls", "Articl_id", "dbo.Articls");
            DropForeignKey("dbo.CategoryBlogs", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.CategoryBlogs", "Blog_id", "dbo.Blogs");
            DropForeignKey("dbo.PostBlogs", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.PostBlogs", "Blog_id", "dbo.Blogs");
            DropForeignKey("dbo.CommentBlogs", "Comment_ID", "dbo.Comments");
            DropForeignKey("dbo.CommentBlogs", "Blog_id", "dbo.Blogs");
            DropForeignKey("dbo.SectionBlogs", "Section_id", "dbo.Sections");
            DropForeignKey("dbo.SectionBlogs", "Blog_id", "dbo.Blogs");
            DropForeignKey("dbo.Articls", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Articls", new[] { "UserID" });
            DropIndex("dbo.BlogArticls", new[] { "Blog_id" });
            DropIndex("dbo.BlogArticls", new[] { "Articl_id" });
            DropIndex("dbo.CategoryBlogs", new[] { "Category_ID" });
            DropIndex("dbo.CategoryBlogs", new[] { "Blog_id" });
            DropIndex("dbo.PostBlogs", new[] { "Post_ID" });
            DropIndex("dbo.PostBlogs", new[] { "Blog_id" });
            DropIndex("dbo.CommentBlogs", new[] { "Comment_ID" });
            DropIndex("dbo.CommentBlogs", new[] { "Blog_id" });
            DropIndex("dbo.SectionBlogs", new[] { "Section_id" });
            DropIndex("dbo.SectionBlogs", new[] { "Blog_id" });
            DropTable("dbo.Articls");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogArticls");
            DropTable("dbo.CategoryBlogs");
            DropTable("dbo.PostBlogs");
            DropTable("dbo.CommentBlogs");
            DropTable("dbo.SectionBlogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SectionBlogs",
                c => new
                    {
                        Section_id = c.Int(nullable: false),
                        Blog_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_id, t.Blog_id });
            
            CreateTable(
                "dbo.CommentBlogs",
                c => new
                    {
                        Comment_ID = c.Int(nullable: false),
                        Blog_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_ID, t.Blog_id });
            
            CreateTable(
                "dbo.PostBlogs",
                c => new
                    {
                        Post_ID = c.Int(nullable: false),
                        Blog_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_ID, t.Blog_id });
            
            CreateTable(
                "dbo.CategoryBlogs",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Blog_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Blog_id });
            
            CreateTable(
                "dbo.BlogArticls",
                c => new
                    {
                        Blog_id = c.Int(nullable: false),
                        Articl_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Blog_id, t.Articl_id });
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Articls",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.SectionBlogs", "Blog_id");
            CreateIndex("dbo.SectionBlogs", "Section_id");
            CreateIndex("dbo.CommentBlogs", "Blog_id");
            CreateIndex("dbo.CommentBlogs", "Comment_ID");
            CreateIndex("dbo.PostBlogs", "Blog_id");
            CreateIndex("dbo.PostBlogs", "Post_ID");
            CreateIndex("dbo.CategoryBlogs", "Blog_id");
            CreateIndex("dbo.CategoryBlogs", "Category_ID");
            CreateIndex("dbo.BlogArticls", "Articl_id");
            CreateIndex("dbo.BlogArticls", "Blog_id");
            CreateIndex("dbo.Articls", "UserID");
            AddForeignKey("dbo.Articls", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SectionBlogs", "Blog_id", "dbo.Blogs", "id", cascadeDelete: true);
            AddForeignKey("dbo.SectionBlogs", "Section_id", "dbo.Sections", "id", cascadeDelete: true);
            AddForeignKey("dbo.CommentBlogs", "Blog_id", "dbo.Blogs", "id", cascadeDelete: true);
            AddForeignKey("dbo.CommentBlogs", "Comment_ID", "dbo.Comments", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PostBlogs", "Blog_id", "dbo.Blogs", "id", cascadeDelete: true);
            AddForeignKey("dbo.PostBlogs", "Post_ID", "dbo.Posts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CategoryBlogs", "Blog_id", "dbo.Blogs", "id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryBlogs", "Category_ID", "dbo.Categories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.BlogArticls", "Articl_id", "dbo.Articls", "id", cascadeDelete: true);
            AddForeignKey("dbo.BlogArticls", "Blog_id", "dbo.Blogs", "id", cascadeDelete: true);
        }
    }
}
