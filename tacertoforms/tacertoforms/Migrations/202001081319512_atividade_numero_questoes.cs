namespace tacertoforms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atividade_numero_questoes : DbMigration
    {
        public override void Up()
        {
            AddColumn("TaCerto.Atividade", "NumeroQuestoes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("TaCerto.Atividade", "NumeroQuestoes");
        }
    }
}
