namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryCityPostalCodeAddedToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Country", c => c.String());
            AddColumn("dbo.Users", "City", c => c.String());
            AddColumn("dbo.Users", "PostalCode", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PostalCode");
            DropColumn("dbo.Users", "City");
            DropColumn("dbo.Users", "Country");
        }
    }
}
