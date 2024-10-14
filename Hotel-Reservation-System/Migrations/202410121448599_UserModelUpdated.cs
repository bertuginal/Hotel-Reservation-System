namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EditedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "EditedDate");
        }
    }
}
