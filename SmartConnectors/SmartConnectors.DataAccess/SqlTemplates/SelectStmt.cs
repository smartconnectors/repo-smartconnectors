namespace SmartConnectors.DataAccess.SqlTemplates
{
    public static class SQLStatements
    {
        public static string TemplateGetAll = "SELECT * FROM dbo.{0}s";
        public static string TemplateGetById = "SELECT * FROM dbo.{0}s WHERE Id = {1}";
        public static string TemplateGetBySingleParentId = "SELECT * FROM dbo.{0}s WHERE {1}Id = {2}";
        public static string TemplateGetByMultiParentId = "SELECT * FROM dbo.{0}s WHERE {1}Id = {2} AND {3}Id = {4}";

        public static string TemplateDelete = "Delete FROM dbo.{0}s WHERE Id = {1}";

    }
}
