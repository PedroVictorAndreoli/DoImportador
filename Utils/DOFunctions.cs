using DoImportador.Connection;


namespace DoImportador.Utils
{
    public class DOFunctions
    {
        public static DOConn DOConnTrans = null;
        public static string ParseSQLToPostgreSQL(string sSQL)
        {
            //como a maioria das consultas foi feita em SQL SERVER, é normal vir 'dbo.Nome_Tabela' nas consultas...
            //então tenho que trocar o 'dbo.' por 'public.', que é o padrão do PostgreSql
            sSQL = sSQL.Replace("dbo.", "public.", comparisonType: StringComparison.InvariantCultureIgnoreCase);
            //tenho que trocar [] por "" no nome das colunas
            sSQL = sSQL.Replace("[", "\"").Replace("]", "\"");
            //troca o '+' por '||' para concatenar textos
            sSQL = sSQL.Replace("+", "||");
            //por padrão, o LIKE do SQL É insensitive case... o do postgree É Sensitive... tenho que trocar o LIKE por ILIKE
            sSQL = sSQL.Replace(" LIKE ", " ILIKE ", comparisonType: StringComparison.InvariantCultureIgnoreCase);
            //para tentar tirar aquele monte de /r/n/t que tem nas sql do explorer_systens
            sSQL = sSQL.Replace("\r", "", comparisonType: StringComparison.InvariantCultureIgnoreCase);
            sSQL = sSQL.Replace("\t", "", comparisonType: StringComparison.InvariantCultureIgnoreCase);
            sSQL = sSQL.Replace("\n", "", comparisonType: StringComparison.InvariantCultureIgnoreCase);
            return sSQL;
        }
    }
}
