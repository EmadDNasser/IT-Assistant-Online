namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SectionPosts", "Section_id", "dbo.Sections");
            DropForeignKey("dbo.SectionPosts", "Post_ID", "dbo.Posts");
            DropIndex("dbo.SectionPosts", new[] { "Section_id" });
            DropIndex("dbo.SectionPosts", new[] { "Post_ID" });
            DropTable("dbo.SectionPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SectionPosts",
                c => new
                    {
                        Section_id = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_id, t.Post_ID });
            
            CreateIndex("dbo.SectionPosts", "Post_ID");
            CreateIndex("dbo.SectionPosts", "Section_id");
            AddForeignKey("dbo.SectionPosts", "Post_ID", "dbo.Posts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SectionPosts", "Section_id", "dbo.Sections", "id", cascadeDelete: true);
        }
    }
}
