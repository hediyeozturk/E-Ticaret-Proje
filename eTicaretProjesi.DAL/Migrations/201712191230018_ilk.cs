namespace eTicaretProjesi.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ilk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Soyad", c => c.String(maxLength: 35));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Soyad");
        }
    }
}
