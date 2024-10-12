namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelTitleCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Title");
        }
    }
}
