namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomIdAddedToReservation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "HotelId", "dbo.Hotels");
            DropIndex("dbo.Reservations", new[] { "HotelId" });
            AddColumn("dbo.Reservations", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "RoomId");
            AddForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
            DropColumn("dbo.Reservations", "HotelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "HotelId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropColumn("dbo.Reservations", "RoomId");
            CreateIndex("dbo.Reservations", "HotelId");
            AddForeignKey("dbo.Reservations", "HotelId", "dbo.Hotels", "Id", cascadeDelete: true);
        }
    }
}
