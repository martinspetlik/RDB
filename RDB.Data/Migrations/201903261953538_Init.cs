namespace RDB.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autobus",
                c => new
                    {
                        spz = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        znacka = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.spz)
                .ForeignKey("dbo.Znacka", t => t.znacka, cascadeDelete: true)
                .Index(t => t.znacka);
            
            CreateTable(
                "dbo.Znacka",
                c => new
                    {
                        znacka = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.znacka);
            
            CreateTable(
                "dbo.Klient",
                c => new
                    {
                        email = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        jmeno = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        prijmeni = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.email);
            
            CreateTable(
                "dbo.Kontakt",
                c => new
                    {
                        hodnota = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        typ = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        cislo_rp = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.hodnota)
                .ForeignKey("dbo.TypKontaktu", t => t.typ, cascadeDelete: true)
                .ForeignKey("dbo.Ridic", t => t.cislo_rp, cascadeDelete: true)
                .Index(t => t.typ)
                .Index(t => t.cislo_rp);
            
            CreateTable(
                "dbo.TypKontaktu",
                c => new
                    {
                        typ = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.typ);
            
            CreateTable(
                "dbo.Ridic",
                c => new
                    {
                        cislo_rp = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        jmeno = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        prijmeni = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.cislo_rp);
            
            CreateTable(
                "dbo.Jizda",
                c => new
                    {
                        cas = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        linka = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        spz = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        cislo_rp = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Pole = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.cas, t.linka })
                .ForeignKey("dbo.Autobus", t => t.spz, cascadeDelete: true)
                .ForeignKey("dbo.Ridic", t => t.cislo_rp, cascadeDelete: true)
                .ForeignKey("dbo.Trasy", t => t.linka, cascadeDelete: true)
                .Index(t => t.linka)
                .Index(t => t.spz)
                .Index(t => t.cislo_rp);
            
            CreateTable(
                "dbo.Trasy",
                c => new
                    {
                        linka = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        odkud = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        kam = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.linka)
                .ForeignKey("dbo.Lokalita", t => t.kam)
                .ForeignKey("dbo.Lokalita", t => t.odkud)
                .Index(t => t.odkud)
                .Index(t => t.kam);
            
            CreateTable(
                "dbo.Lokalita",
                c => new
                    {
                        nazev = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.nazev);
            
            CreateTable(
                "dbo.Mezizastavka",
                c => new
                    {
                        nazev = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        linka = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.nazev, t.linka })
                .ForeignKey("dbo.Lokalita", t => t.nazev, cascadeDelete: true)
                .ForeignKey("dbo.Trasy", t => t.linka, cascadeDelete: true)
                .Index(t => t.nazev)
                .Index(t => t.linka);
            
            CreateTable(
                "dbo.Jizdenka",
                c => new
                    {
                        cas = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        linka = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        cislo = c.Long(nullable: false, identity: true),
                        email = c.String(maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.cislo)
                .ForeignKey("dbo.Klient", t => t.email)
                .ForeignKey("dbo.Jizda", t => new { t.cas, t.linka })
                .Index(t => new { t.cas, t.linka })
                .Index(t => t.email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jizdenka", new[] { "cas", "linka" }, "dbo.Jizda");
            DropForeignKey("dbo.Jizdenka", "email", "dbo.Klient");
            DropForeignKey("dbo.Mezizastavka", "linka", "dbo.Trasy");
            DropForeignKey("dbo.Mezizastavka", "nazev", "dbo.Lokalita");
            DropForeignKey("dbo.Jizda", "linka", "dbo.Trasy");
            DropForeignKey("dbo.Trasy", "odkud", "dbo.Lokalita");
            DropForeignKey("dbo.Trasy", "kam", "dbo.Lokalita");
            DropForeignKey("dbo.Jizda", "cislo_rp", "dbo.Ridic");
            DropForeignKey("dbo.Jizda", "spz", "dbo.Autobus");
            DropForeignKey("dbo.Kontakt", "cislo_rp", "dbo.Ridic");
            DropForeignKey("dbo.Kontakt", "typ", "dbo.TypKontaktu");
            DropForeignKey("dbo.Autobus", "znacka", "dbo.Znacka");
            DropIndex("dbo.Jizdenka", new[] { "email" });
            DropIndex("dbo.Jizdenka", new[] { "cas", "linka" });
            DropIndex("dbo.Mezizastavka", new[] { "linka" });
            DropIndex("dbo.Mezizastavka", new[] { "nazev" });
            DropIndex("dbo.Trasy", new[] { "kam" });
            DropIndex("dbo.Trasy", new[] { "odkud" });
            DropIndex("dbo.Jizda", new[] { "cislo_rp" });
            DropIndex("dbo.Jizda", new[] { "spz" });
            DropIndex("dbo.Jizda", new[] { "linka" });
            DropIndex("dbo.Kontakt", new[] { "cislo_rp" });
            DropIndex("dbo.Kontakt", new[] { "typ" });
            DropIndex("dbo.Autobus", new[] { "znacka" });
            DropTable("dbo.Jizdenka");
            DropTable("dbo.Mezizastavka");
            DropTable("dbo.Lokalita");
            DropTable("dbo.Trasy");
            DropTable("dbo.Jizda");
            DropTable("dbo.Ridic");
            DropTable("dbo.TypKontaktu");
            DropTable("dbo.Kontakt");
            DropTable("dbo.Klient");
            DropTable("dbo.Znacka");
            DropTable("dbo.Autobus");
        }
    }
}
