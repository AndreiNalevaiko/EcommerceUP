namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarrinhoID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemVenda", "CarrinhoId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemVenda", "CarrinhoId");
        }
    }
}
