namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeeStructures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        SubCategoryId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Offrs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JCOs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Civil = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeeStructures");
        }
    }
}
