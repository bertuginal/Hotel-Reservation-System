namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacilityModelAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Facilities", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Facilities");
        }
    }
}
