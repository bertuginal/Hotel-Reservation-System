namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageAddedToRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "ImageUrl");
        }
    }
}
