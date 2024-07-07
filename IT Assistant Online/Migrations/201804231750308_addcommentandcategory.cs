namespace IT_Assistant_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcommentandcategory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CategoryPosts", newName: "PostCategories");
            DropPrimaryKey("dbo.PostCategories");
            AddPrimaryKey("dbo.PostCategories", new[] { "Post_ID", "Category_ID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PostCategories");
            AddPrimaryKey("dbo.PostCategories", new[] { "Category_ID", "Post_ID" });
            RenameTable(name: "dbo.PostCategories", newName: "CategoryPosts");
        }
    }
}
