namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "sections_id", "dbo.Sections");
            DropIndex("dbo.Posts", new[] { "sections_id" });
            CreateTable(
                "dbo.SectionPosts",
                c => new
                    {
                        Section_id = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_id, t.Post_ID })
                .ForeignKey("dbo.Sections", t => t.Section_id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Section_id)
                .Index(t => t.Post_ID);
            
            AddColumn("dbo.Posts", "sectionID", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "sections_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "sections_id", c => c.Int());
            DropForeignKey("dbo.SectionPosts", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.SectionPosts", "Section_id", "dbo.Sections");
            DropIndex("dbo.SectionPosts", new[] { "Post_ID" });
            DropIndex("dbo.SectionPosts", new[] { "Section_id" });
            DropColumn("dbo.Posts", "sectionID");
            DropTable("dbo.SectionPosts");
            CreateIndex("dbo.Posts", "sections_id");
            AddForeignKey("dbo.Posts", "sections_id", "dbo.Sections", "id");
        }
    }
}
