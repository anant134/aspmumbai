namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subcatgroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategories", "isGroup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategories", "isGroup");
        }
    }
}
