namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noticeboard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoticeBoards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        Oldname = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: true),
                        ModifiedOn = c.DateTime(nullable: true),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NoticeBoards");
        }
    }
}
