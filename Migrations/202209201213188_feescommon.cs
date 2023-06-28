namespace aspm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feescommon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeeStructures", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.FeeStructures", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.FeeStructures", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.FeeStructures", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.FeeStructures", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeeStructures", "IsActive");
            DropColumn("dbo.FeeStructures", "ModifiedOn");
            DropColumn("dbo.FeeStructures", "ModifiedBy");
            DropColumn("dbo.FeeStructures", "CreatedBy");
            DropColumn("dbo.FeeStructures", "CreatedOn");
        }
    }
}
