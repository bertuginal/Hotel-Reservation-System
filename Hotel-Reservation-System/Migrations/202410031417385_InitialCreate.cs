namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                        CreatedDate = c.DateTime(),
                        Address = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.Int(nullable: false),
                        NumberOfGuests = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Location = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 200),
                        Rating = c.Int(nullable: false),
                        AvailableRooms = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Users");
            DropIndex("dbo.Reservations", new[] { "HotelId" });
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropTable("dbo.Hotels");
            DropTable("dbo.Reservations");
            DropTable("dbo.Users");
        }
    }
}
