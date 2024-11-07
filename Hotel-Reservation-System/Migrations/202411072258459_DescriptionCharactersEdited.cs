namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionCharactersEdited : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hotels", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hotels", "Description", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
