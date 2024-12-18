namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentModelUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "CardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "ExpirationDate", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "CVV", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "CVV", c => c.String());
            AlterColumn("dbo.Payments", "ExpirationDate", c => c.String());
            AlterColumn("dbo.Payments", "CardNumber", c => c.String());
        }
    }
}
