namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelModelEdited1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hotels", "Location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hotels", "Location", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
