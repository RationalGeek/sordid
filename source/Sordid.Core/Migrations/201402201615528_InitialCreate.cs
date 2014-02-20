namespace Sordid.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aspects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        HeadingLabel = c.String(),
                        SubHeadingLabel = c.String(),
                        DescriptiveBlurb = c.String(),
                        EventsLabel = c.String(),
                        StoryTitleLabel = c.String(),
                        StarringLabel = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        ConcurrencyVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharacterPowers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PowerId = c.Int(nullable: false),
                        CharacterId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        ConcurrencyVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .ForeignKey("dbo.Powers", t => t.PowerId, cascadeDelete: true)
                .Index(t => t.CharacterId)
                .Index(t => t.PowerId);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlayerName = c.String(),
                        Appearance = c.String(),
                        Notes = c.String(),
                        StoryTitle = c.String(),
                        Starring = c.String(),
                        ImageUrl = c.String(),
                        MaxSkillPoints = c.Int(nullable: false),
                        BaseRefresh = c.Int(nullable: false),
                        PowerLevelId = c.Int(),
                        TemplateId = c.Int(),
                        PhysicalStress = c.Int(nullable: false),
                        MentalStress = c.Int(nullable: false),
                        SocialStress = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        ConcurrencyVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PowerLevels", t => t.PowerLevelId)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .ForeignKey("dbo.Users", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.PowerLevelId)
                .Index(t => t.TemplateId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.CharacterAspects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Events = c.String(),
                        Starring = c.String(),
                        StoryTitle = c.String(),
                        CharacterId = c.Int(nullable: false),
                        AspectId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        ConcurrencyVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aspects", t => t.AspectId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .Index(t => t.AspectId)
                .Index(t => t.CharacterId);
            
            CreateTable(
                "dbo.Consequences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        StressType = c.String(),
                        StressAmount = c.Int(nullable: false),
                        UserCreated = c.Boolean(nullable: false),
                        CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .Index(t => t.CharacterId);
            
            CreateTable(
                "dbo.PowerLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BaseRefresh = c.Int(nullable: false),
                        SkillPoints = c.Int(nullable: false),
                        MaxSkillRank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharacterSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        CharacterId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        ConcurrencyVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .Index(t => t.SkillId)
                .Index(t => t.CharacterId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Trappings = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        ConcurrencyVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Powers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        Name = c.String(),
                        Notes = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        ConcurrencyVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterPowers", "PowerId", "dbo.Powers");
            DropForeignKey("dbo.Characters", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Characters", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.CharacterSkills", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.CharacterSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.CharacterPowers", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Characters", "PowerLevelId", "dbo.PowerLevels");
            DropForeignKey("dbo.Consequences", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.CharacterAspects", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.CharacterAspects", "AspectId", "dbo.Aspects");
            DropIndex("dbo.CharacterPowers", new[] { "PowerId" });
            DropIndex("dbo.Characters", new[] { "ApplicationUserId" });
            DropIndex("dbo.UserClaims", new[] { "User_Id" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.Characters", new[] { "TemplateId" });
            DropIndex("dbo.CharacterSkills", new[] { "CharacterId" });
            DropIndex("dbo.CharacterSkills", new[] { "SkillId" });
            DropIndex("dbo.CharacterPowers", new[] { "CharacterId" });
            DropIndex("dbo.Characters", new[] { "PowerLevelId" });
            DropIndex("dbo.Consequences", new[] { "CharacterId" });
            DropIndex("dbo.CharacterAspects", new[] { "CharacterId" });
            DropIndex("dbo.CharacterAspects", new[] { "AspectId" });
            DropTable("dbo.Powers");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Templates");
            DropTable("dbo.Skills");
            DropTable("dbo.CharacterSkills");
            DropTable("dbo.PowerLevels");
            DropTable("dbo.Consequences");
            DropTable("dbo.CharacterAspects");
            DropTable("dbo.Characters");
            DropTable("dbo.CharacterPowers");
            DropTable("dbo.Aspects");
        }
    }
}
