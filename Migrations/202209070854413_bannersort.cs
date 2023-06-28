namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bannersort : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "SortId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "SortId");
        }
    }
}
