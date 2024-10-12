namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelImageCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "ImageUrl");
        }
    }
}
