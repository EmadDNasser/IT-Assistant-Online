namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SectionPosts",
                c => new
                    {
                        Section_ID = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_ID, t.Post_ID })
                .ForeignKey("dbo.Sections", t => t.Section_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Section_ID)
                .Index(t => t.Post_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SectionPosts", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.SectionPosts", "Section_ID", "dbo.Sections");
            DropIndex("dbo.SectionPosts", new[] { "Post_ID" });
            DropIndex("dbo.SectionPosts", new[] { "Section_ID" });
            DropTable("dbo.SectionPosts");
            DropTable("dbo.Sections");
        }
    }
}
