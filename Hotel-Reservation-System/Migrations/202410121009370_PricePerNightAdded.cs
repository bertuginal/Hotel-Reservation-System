namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricePerNightAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "PricePerNight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "PricePerNight");
        }
    }
}
