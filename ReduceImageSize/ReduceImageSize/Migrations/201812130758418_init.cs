namespace ReduceImageSize.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Picture = c.Binary(),
                        UploadDate = c.DateTime(),
                        IsUseAsAvator = c.Boolean(),
                        MoodMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPictures");
        }
    }
}
