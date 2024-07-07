namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SectionPosts", "Section_ID", "dbo.Sections");
            DropForeignKey("dbo.SectionPosts", "Post_ID", "dbo.Posts");
            DropIndex("dbo.SectionPosts", new[] { "Section_ID" });
            DropIndex("dbo.SectionPosts", new[] { "Post_ID" });
            AddColumn("dbo.Posts", "sections_id", c => c.Int());
            CreateIndex("dbo.Posts", "sections_id");
            AddForeignKey("dbo.Posts", "sections_id", "dbo.Sections", "id");
            DropTable("dbo.SectionPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SectionPosts",
                c => new
                    {
                        Section_ID = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_ID, t.Post_ID });
            
            DropForeignKey("dbo.Posts", "sections_id", "dbo.Sections");
            DropIndex("dbo.Posts", new[] { "sections_id" });
            DropColumn("dbo.Posts", "sections_id");
            CreateIndex("dbo.SectionPosts", "Post_ID");
            CreateIndex("dbo.SectionPosts", "Section_ID");
            AddForeignKey("dbo.SectionPosts", "Post_ID", "dbo.Posts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SectionPosts", "Section_ID", "dbo.Sections", "ID", cascadeDelete: true);
        }
    }
}
