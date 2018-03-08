namespace SurfboardQuiverConsoleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardStyle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Surfboard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuilderId = c.Int(nullable: false),
                        Model = c.String(nullable: false, maxLength: 50),
                        BoardStyleId = c.Int(nullable: false),
                        Length = c.Single(nullable: false),
                        Width = c.Single(nullable: false),
                        MaxFins = c.Int(nullable: false),
                        Rating = c.Single(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Builder", t => t.BuilderId, cascadeDelete: true)
                .ForeignKey("dbo.BoardStyle", t => t.BoardStyleId, cascadeDelete: true)
                .Index(t => t.BuilderId)
                .Index(t => t.BoardStyleId);
            
            CreateTable(
                "dbo.Builder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surfboard", "BoardStyleId", "dbo.BoardStyle");
            DropForeignKey("dbo.Surfboard", "BuilderId", "dbo.Builder");
            DropIndex("dbo.Surfboard", new[] { "BoardStyleId" });
            DropIndex("dbo.Surfboard", new[] { "BuilderId" });
            DropTable("dbo.Builder");
            DropTable("dbo.Surfboard");
            DropTable("dbo.BoardStyle");
        }
    }
}
