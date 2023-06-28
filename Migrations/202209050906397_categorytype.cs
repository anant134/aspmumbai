namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categorytype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Categories", "categoryTypeId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "categoryTypeId");
            DropTable("dbo.CategoryTypes");
        }
    }
}
