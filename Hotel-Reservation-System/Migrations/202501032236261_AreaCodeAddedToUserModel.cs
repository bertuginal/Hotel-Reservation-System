namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AreaCodeAddedToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AreaCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AreaCode");
        }
    }
}
