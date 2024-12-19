namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstNameandLastNameAddedToPaymentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Payments", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "LastName");
            DropColumn("dbo.Payments", "FirstName");
        }
    }
}
