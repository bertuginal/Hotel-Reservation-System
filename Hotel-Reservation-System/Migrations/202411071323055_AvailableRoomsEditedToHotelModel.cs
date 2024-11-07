namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvailableRoomsEditedToHotelModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "AvailableRooms", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "AvailableRooms");
        }
    }
}
