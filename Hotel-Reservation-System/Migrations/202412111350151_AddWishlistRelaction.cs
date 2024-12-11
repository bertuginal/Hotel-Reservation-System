namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWishlistRelaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserHotels",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Hotel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Hotel_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Hotel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserHotels", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.UserHotels", "User_Id", "dbo.Users");
            DropIndex("dbo.UserHotels", new[] { "Hotel_Id" });
            DropIndex("dbo.UserHotels", new[] { "User_Id" });
            DropTable("dbo.UserHotels");
        }
    }
}
