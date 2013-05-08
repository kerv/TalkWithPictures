namespace TalkWithPictures.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageStores", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageStores", "Index");
        }
    }
}
