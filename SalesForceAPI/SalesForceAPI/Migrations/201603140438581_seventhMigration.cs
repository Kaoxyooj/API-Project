namespace SalesForceAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seventhMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Yak2Inventory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductCode = c.String(),
                        Qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Yak2Inventory");
        }
    }
}
