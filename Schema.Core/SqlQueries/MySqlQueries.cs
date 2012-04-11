namespace Schema.Core.SqlQueries
{
    using Schema.Core.Names;

    public class MySqlQueries : ISqlQueries
    {
        public string DbName { get; set; }

        public string SelectColumn // todo remove view
        {
            get
            {
                return 
                    @"SELECT c.TABLE_NAME as " + ColumnKeys.TableName + 
                                @", c.COLUMN_NAME as " + ColumnKeys.ColumnName +
                                @", c.COLUMN_TYPE as " + ColumnKeys.TypeName + 
                                @", c.CHARACTER_MAXIMUM_LENGTH as" + ColumnKeys.MaxLength + 
                                @", c.IS_NULLABLE as" + ColumnKeys.AllowNull +
                                @", c.EXTRA  as " + ColumnKeys.IsIdentity +
                               /* @", NULL as " + ColumnKeys.IdentityIncriment +*/
                     @"FROM  INFORMATION_SCHEMA.COLUMNS c
                     WHERE c.TABLE_SCHEMA ='" + this.DbName + @"'
                     ORDER BY c.TABLE_NAME;";
            }
        }

        public string SelectKey 
        {
            get
            {
                return
                    @"SELECT `KU`.TABLE_NAME as " + KeyNames.TableName +
                              @",`KU`.COLUMN_NAME as " + KeyNames.ColumnName +
                              @",`RT`.CONSTRAINT_TYPE as " + KeyNames.TypeDescription +
                              @",`RT`.CONSTRAINT_NAME as " + KeyNames.KeyName +
                              /*@",`RT`.UNIQUE_CONSTRAINT_NAME as " + KeyNames.Type +*/
                              @",`RT`.DELETE_RULE as " + KeyNames.DeletRule +
                              @",`RT`.UPDATE_RULE as " + KeyNames.UpdateRule +
                              @",`KU`.REFERENCED_TABLE_NAME as " + KeyNames.ReferanceTable +
                              @",`KU`.REFERENCED_COLUMN_NAME as " + KeyNames.ReferanceColumn +
                       @" FROM(
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
                    WHERE `RT`.CONSTRAINT_SCHEMA='" + this.DbName + "';";
           }
        }

        public string SelectPk
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public string SelectTrigger
        {
            get
            {
                return
                    @"SELECT `tr`.EVENT_OBJECT_TABLE as " + TriggerNames.TableName +
                             @",`tr`.TRIGGER_NAME as " + TriggerNames.TriggerName +
                             @",`tr`.EVENT_MANIPULATION as " + TriggerNames.TriggerEvent + /*",`tr`.ACTION_ORIENTATION,`tr`.ACTION_TIMING,`tr`.ACTION_STATEMENT*/
                  @" FROM INFORMATION_SCHEMA.TRIGGERS `tr`
                    WHERE `tr`.TRIGGER_SCHEMA='" + this.DbName + "';";
            }
        }

        public string SelectIndex
        {
            get
            {
                return 
                    @"SELECT DISTINCT `s`.TABLE_NAME as " + IndexNames.TableName +
                                  @", `s`.COLUMN_NAME as " + IndexNames.ColumnName +
                                  @",`s`.INDEX_NAME as " + IndexNames.IndexName +
                                  @",`s`.INDEX_TYPE as " + IndexNames.IndexType + 
                                  @",`s`.NON_UNIQUE as " + IndexNames.Unique +
                                  @",`s`.COLLATION as " + IndexNames.SortOrder + 
                   @" FROM INFORMATION_SCHEMA.STATISTICS s
                    WHERE TABLE_SCHEMA = '" + this.DbName + "';";
            }
        }

        public string SelectView// todo 
        {
            get
            {
                return
                    @"SELECT c.TABLE_NAME as " + ColumnKeys.TableName +
                          @", c.COLUMN_NAME as " + ColumnKeys.ColumnName +
                          @", c.COLUMN_TYPE as " + ColumnKeys.TypeName +
                          @", c.CHARACTER_MAXIMUM_LENGTH as " + ColumnKeys.MaxLength +
                          @", c.IS_NULLABLE as " + ColumnKeys.AllowNull +
                          @", c.EXTRA as " + ColumnKeys.IsIdentity +
                          @" FROM INFORMATION_SCHEMA.COLUMNS c
                            RIGHT JOIN
                                      (SELECT `v`.TABLE_NAME,`v`.VIEW_DEFINITION,`v`.IS_UPDATABLE,`v`.SECURITY_TYPE,`v`.COLLATION_CONNECTION
                                       FROM INFORMATION_SCHEMA.VIEWS v) view 
                            ON `c`.TABLE_NAME=`view`.TABLE_NAME
                            WHERE `c`.TABLE_SCHEMA='" + this.DbName + "';";

//                        @"SELECT `v`.TABLE_NAME,`v`.VIEW_DEFINITION,`v`.IS_UPDATABLE,`v`.SECURITY_TYPE,`v`.COLLATION_CONNECTION
//                        FROM INFORMATION_SCHEMA.VIEWS v
//                        WHERE `v`.TABLE_SCHEMA='" + DbName + "';"; todo old

//SELECT c.TABLE_NAME,c.COLUMN_NAME,c.COLUMN_TYPE,c.CHARACTER_MAXIMUM_LENGTH , c.IS_NULLABLE,c.EXTRA,NULL,`view`.IS_UPDATABLE,`view`.SECURITY_TYPE,`view`.COLLATION_CONNECTION
//FROM INFORMATION_SCHEMA.COLUMNS c
//RIGHT JOIN
//          (SELECT `v`.TABLE_NAME,`v`.VIEW_DEFINITION,`v`.IS_UPDATABLE,`v`.SECURITY_TYPE,`v`.COLLATION_CONNECTION
//           FROM INFORMATION_SCHEMA.VIEWS v) view 
//ON `c`.TABLE_NAME=`view`.TABLE_NAME
//WHERE `c`.TABLE_SCHEMA='blog';
            }
        }

        public string SelectProcedure
        {
            get
            {
                return
                        @"SELECT `r`.SPECIFIC_NAME as " +
                              @",`r`.ROUTINE_NAME as " +
                              @",`r`.ROUTINE_TYPE as " +
                              @",`r`.DTD_IDENTIFIER as " +
                              @",`r`.ROUTINE_BODY as " +
                              @",`r`.ROUTINE_DEFINITION as " +
                              @",`r`.IS_DETERMINISTIC as " +
                   @" FROM INFORMATION_SCHEMA.ROUTINES r
                    WHERE `r`.ROUTINE_SCHEMA='" + this.DbName + @"' and  `r`.ROUTINE_TYPE like '%proc%';";
            }
        }
        
        public string SelectFunction
        {
            get
            {
                return 
                    @"SELECT `r`.SPECIFIC_NAME as " +
                        @", `r`.ROUTINE_NAME as " +
                        @", `r`.ROUTINE_TYPE as " +
                        @", `r`.DTD_IDENTIFIER as " +
                        @", `r`.ROUTINE_BODY as " +
                        @", `r`.ROUTINE_DEFINITION as " +
                        @",`r`.IS_DETERMINISTIC as " +
                    @" FROM INFORMATION_SCHEMA.ROUTINES r
                    WHERE `r`.ROUTINE_SCHEMA='" + this.DbName + @"' and  `r`.ROUTINE_TYPE like '%fun%';";
            }
        }
    }
}
