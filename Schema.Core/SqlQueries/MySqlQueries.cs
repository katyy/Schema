namespace Schema.Core.SqlQueries
{
    public class MySqlQueries : ISqlQueries
    {
        public string DbName { get; set; }

        public string SelectColumn
        {
            get
            {
                return @"SELECT c.TABLE_NAME,c.COLUMN_NAME,c.COLUMN_TYPE,c.CHARACTER_MAXIMUM_LENGTH , c.IS_NULLABLE,c.EXTRA,NULL
             FROM  INFORMATION_SCHEMA.COLUMNS c
             WHERE c.TABLE_SCHEMA ='"+DbName+@"'
             ORDER BY c.TABLE_NAME;";
            }
        }

        public string SelectFk //all key
        {
            get
            {
                return
                    @"SELECT `KU`.TABLE_NAME,`KU`.COLUMN_NAME,`RT`.CONSTRAINT_TYPE,`RT`.CONSTRAINT_NAME,`RT`.UNIQUE_CONSTRAINT_NAME,`RT`.DELETE_RULE,`RT`.UPDATE_RULE,`KU`.REFERENCED_TABLE_NAME,`KU`.REFERENCED_COLUMN_NAME
                        FROM(
                            SELECT `t`.CONSTRAINT_SCHEMA,`t`.CONSTRAINT_NAME,`t`.TABLE_NAME ,`t`.CONSTRAINT_TYPE, `r`.UNIQUE_CONSTRAINT_NAME, `r`.UPDATE_RULE, `r`.DELETE_RULE,  `r`.REFERENCED_TABLE_NAME
                            FROM (
                                SELECT  `TC`.CONSTRAINT_SCHEMA,`TC`.CONSTRAINT_NAME,`TC`.TABLE_NAME ,`TC`.CONSTRAINT_TYPE
                                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
                                 ) t
                        LEFT JOIN (
                               SELECT `RC`.CONSTRAINT_NAME, `RC`.UNIQUE_CONSTRAINT_NAME, `RC`.UPDATE_RULE, `RC`.DELETE_RULE, `RC`.TABLE_NAME, `RC`.REFERENCED_TABLE_NAME 
                               FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC
                               ) r 
                        ON r.CONSTRAINT_NAME=t.CONSTRAINT_NAME and r.TABLE_NAME=t.TABLE_NAME
                             )RT
                    LEFT JOIN (
                               SELECT `key_usage`.CONSTRAINT_NAME,`key_usage`.TABLE_NAME,`key_usage`.COLUMN_NAME,`key_usage`.REFERENCED_TABLE_NAME,`key_usage`.REFERENCED_COLUMN_NAME
                               FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE key_usage
                               )KU 
                    ON `KU`.TABLE_NAME=`RT`.TABLE_NAME and `KU`.CONSTRAINT_NAME=`RT`.CONSTRAINT_NAME  
                    WHERE `RT`.CONSTRAINT_SCHEMA='" + DbName + "';";
              
              


            }
        }

        public string SelectPk//fk
        {
            get
            {

                return "";

                //                    @"  SELECT `KU`.TABLE_NAME,`KU`.COLUMN_NAME,`t`.CONSTRAINT_TYPE,`fk`.CONSTRAINT_NAME,`fk`.UNIQUE_CONSTRAINT_NAME,`fk`.DELETE_RULE,`fk`.UPDATE_RULE,`KU`.REFERENCED_TABLE_NAME,`KU`.REFERENCED_COLUMN_NAME
                // FROM(
                //SELECT `RC`.CONSTRAINT_NAME, `RC`.UNIQUE_CONSTRAINT_NAME, `RC`.UPDATE_RULE, `RC`.DELETE_RULE, `RC`.TABLE_NAME, `RC`.REFERENCED_TABLE_NAME 
                //FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC
                //) fk
                //LEFT JOIN (
                //SELECT `TC`.TABLE_SCHEMA, `TC`.CONSTRAINT_NAME,`TC`.TABLE_NAME ,`TC`.CONSTRAINT_TYPE
                //FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
                //) t 
                //ON  `t`.TABLE_NAME=`fk`.TABLE_NAME and `t`.CONSTRAINT_NAME=`fk`.CONSTRAINT_NAME
                //LEFT JOIN (SELECT `key_usage`.CONSTRAINT_NAME,`key_usage`.TABLE_NAME,`key_usage`.COLUMN_NAME,`key_usage`.REFERENCED_TABLE_NAME,`key_usage`.REFERENCED_COLUMN_NAME
                //FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE key_usage
                //)KU 
                // ON `KU`.TABLE_NAME=`fk`.TABLE_NAME and `KU`.CONSTRAINT_NAME=`fk`.CONSTRAINT_NAME
                // WHERE `t`.TABLE_SCHEMA LIKE '" +  DbName + @"%';";

            }
        }

        public string SelectTrigger
        {
            get
            {
                return
                    @"SELECT `tr`.EVENT_OBJECT_TABLE ,`tr`.TRIGGER_NAME,`tr`.EVENT_MANIPULATION,`tr`.ACTION_ORIENTATION,`tr`.ACTION_TIMING
                    FROM INFORMATION_SCHEMA.TRIGGERS `tr`
                    WHERE `tr`.TRIGGER_SCHEMA='" + DbName + "';";
            }
        }

        public string SelectIndex
        {
            get
            {
                return 
                    @"SELECT DISTINCT `s`.TABLE_NAME, `s`.COLUMN_NAME,`s`.INDEX_NAME,`s`.INDEX_TYPE,`s`.NON_UNIQUE,`s`.COLLATION
                    FROM INFORMATION_SCHEMA.STATISTICS s
                    WHERE TABLE_SCHEMA = '"+ DbName + "';";
            }
        }

        public string SelectView//todo ?
        {
            get { return
                        @"SELECT `v`.TABLE_NAME,`v`.VIEW_DEFINITION,`v`.IS_UPDATABLE,`v`.SECURITY_TYPE,`v`.COLLATION_CONNECTION
                        FROM INFORMATION_SCHEMA.VIEWS v
                        WHERE `v`.TABLE_SCHEMA='" + DbName + "';";
            }
        }

        public string SelectProcedure
        {
            get
            {
                return
                        @"SELECT `r`.SPECIFIC_NAME ,`r`.ROUTINE_NAME,`r`.ROUTINE_TYPE,`r`.DTD_IDENTIFIER,`r`.ROUTINE_BODY,`r`.ROUTINE_DEFINITION,`r`.IS_DETERMINISTIC
                    FROM INFORMATION_SCHEMA.ROUTINES r
                    WHERE `r`.ROUTINE_SCHEMA=''" + DbName + @"' and  `r`.ROUTINE_TYPE like '%proc%';";
            }
        }

       
        public string SelectFunction
        {
            get
            {
                return 
                    @"SELECT `r`.SPECIFIC_NAME ,`r`.ROUTINE_NAME,`r`.ROUTINE_TYPE,`r`.DTD_IDENTIFIER,`r`.ROUTINE_BODY,`r`.ROUTINE_DEFINITION,`r`.IS_DETERMINISTIC
                    FROM INFORMATION_SCHEMA.ROUTINES r
                    WHERE `r`.ROUTINE_SCHEMA=''" + DbName + @"' and  `r`.ROUTINE_TYPE like '%fun%';";
            }
        }




        public string SelectViewTriggers//todo delete
        {
            get { throw new System.NotImplementedException(); }
        }

        public string SelectViewIndexes//todo delete
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
