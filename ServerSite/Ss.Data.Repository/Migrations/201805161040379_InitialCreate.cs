namespace Ss.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessPermissionDescription = c.String(nullable: false, maxLength: 50),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleAccessPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScopeView = c.Int(nullable: false),
                        Actflg = c.Int(nullable: false),
                        AccessPermission_Id = c.Int(),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccessPermissions", t => t.AccessPermission_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.AccessPermission_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        RoleDescription = c.String(),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        Permission = c.Int(nullable: false),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Actflg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleAccessPermissions", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleAccessPermissions", "AccessPermission_Id", "dbo.AccessPermissions");
            DropIndex("dbo.UserRole", new[] { "Role_Id" });
            DropIndex("dbo.UserRole", new[] { "User_Id" });
            DropIndex("dbo.RoleAccessPermissions", new[] { "Role_Id" });
            DropIndex("dbo.RoleAccessPermissions", new[] { "AccessPermission_Id" });
            DropTable("dbo.UserRole");
            DropTable("dbo.GroupUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleAccessPermissions");
            DropTable("dbo.AccessPermissions");
        }
    }
}
