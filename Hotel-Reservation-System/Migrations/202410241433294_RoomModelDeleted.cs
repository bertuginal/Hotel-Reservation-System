namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomModelDeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels");
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropTable("dbo.Rooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200),
                        PricePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Rooms", "HotelId");
            AddForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels", "Id", cascadeDelete: true);
        }
    }
}
