namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckOutDateUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "CheckOutDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "CheckOutDate", c => c.Int(nullable: false));
        }
    }
}
