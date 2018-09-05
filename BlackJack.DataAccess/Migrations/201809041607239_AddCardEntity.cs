namespace BlackJack.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardName = c.Int(nullable: false),
                        CardType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerCards", "CardId", "dbo.Cards");
            DropTable("dbo.Cards");
        }
    }
}
