namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videolink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Videolink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Videolink");
        }
    }
}
