namespace Schema.Core.SqlQueries
{
    public class MySqlQueries : ISqlQueries
    {
        public string SelectColumn
        {
            get
            {
                return @"SELECT c.TABLE_NAME,c.COLUMN_NAME,c.COLUMN_TYPE,c.CHARACTER_MAXIMUM_LENGTH , c.IS_NULLABLE,c.EXTRA,NULL
             FROM  INFORMATION_SCHEMA.COLUMNS c
             WHERE c.TABLE_SCHEMA LIKE 'blog%'
             ORDER BY c.TABLE_NAME;";
            }
        }

        public string SelectPk
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectFk
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectTrigger
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectIndex
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectView
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectProcedure
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectViewTriggers
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectViewIndexes
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectFunction
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
