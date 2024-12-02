namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacilityAddedToRoomModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Facilities", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Facilities");
        }
    }
}
