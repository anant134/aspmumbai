namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awesbook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AWESBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        Oldname = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AWESBooks");
        }
    }
}
