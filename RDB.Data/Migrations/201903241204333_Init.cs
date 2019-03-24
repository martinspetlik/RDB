namespace RDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jizdenka", new[] { "linka", "cas" }, "dbo.Jizda");
            DropIndex("dbo.Jizdenka", new[] { "linka", "cas" });
            DropPrimaryKey("dbo.Jizda");
            AlterColumn("dbo.Jizda", "cas", c => c.Binary(nullable: false, maxLength: 128));
            AlterColumn("dbo.Jizdenka", "cas", c => c.Binary(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Jizda", new[] { "linka", "cas" });
            CreateIndex("dbo.Jizdenka", new[] { "linka", "cas" });
            AddForeignKey("dbo.Jizdenka", new[] { "linka", "cas" }, "dbo.Jizda", new[] { "linka", "cas" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jizdenka", new[] { "linka", "cas" }, "dbo.Jizda");
            DropIndex("dbo.Jizdenka", new[] { "linka", "cas" });
            DropPrimaryKey("dbo.Jizda");
            AlterColumn("dbo.Jizdenka", "cas", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterColumn("dbo.Jizda", "cas", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddPrimaryKey("dbo.Jizda", new[] { "linka", "cas" });
            CreateIndex("dbo.Jizdenka", new[] { "linka", "cas" });
            AddForeignKey("dbo.Jizdenka", new[] { "linka", "cas" }, "dbo.Jizda", new[] { "linka", "cas" });
        }
    }
}
