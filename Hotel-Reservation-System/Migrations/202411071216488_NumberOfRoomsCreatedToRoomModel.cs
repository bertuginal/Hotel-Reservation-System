namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberOfRoomsCreatedToRoomModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "NumberOfRooms", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "NumberOfRooms");
        }
    }
}
