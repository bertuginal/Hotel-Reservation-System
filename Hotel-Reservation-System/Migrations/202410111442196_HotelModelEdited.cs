namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelModelEdited : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hotels", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hotels", "ImageUrl", c => c.String(nullable: false));
        }
    }
}
