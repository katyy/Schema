namespace Schema.Core.SqlQueries
{
    using Schema.Core.Keys;
    using Schema.Core.Names;

    public class MsSqlQueries : ISqlQueries
    {
        public string SelectColumn
        {
            get
            {
                return
                        @"SELECT col.table_name as " + ColumnKeys.TableName +
                                @", col.column_name as " + ColumnKeys.ColumnName +
                                @", col.type_name as " + ColumnKeys.TypeName + 
                                @", col.max_length as " + ColumnKeys.MaxLength + 
                                @", col.is_nullable as " + ColumnKeys.AllowNull + 
                                @", col.is_identity as " + ColumnKeys.IsIdentity +
                                @", identy.increment_value  as " + ColumnKeys.IdentityIncriment + 
                         @" FROM(
                                SELECT t.object_id,c.column_id, t.name table_name,c.name column_name,ty.name type_name, c.max_length, c.is_nullable,c.is_identity 
                                FROM sys.types ty ,sys.tables t,sys.columns c 
                                WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id
                                ) col 
                        LEFT JOIN  sys.identity_columns identy 
                        ON identy.object_id=col.object_id and identy.column_id=col.column_id;";
            }
        }

        public string SelectKey
        {
            get
            {
                return
                      @"SELECT foriegen.parent_table as " + KeyNames.TableName +
                              @", parent_column as " + KeyNames.ColumnName +
                              @", f.type as " + KeyNames.Type +
                              @",name as " + KeyNames.KeyName +
                              @", f.type_desc as " + KeyNames.TypeDescription +
                              @", f.delete_referential_action_desc as " + KeyNames.DeletRule +
                              @", f.update_referential_action_desc as " + KeyNames.UpdateRule +
                              @", foriegen.referance_table as " + KeyNames.ReferanceTable +
                              @", foriegen.referance_column as " + KeyNames.ReferanceColumn +
                      @" FROM sys.foreign_keys f  
                      LEFT OUTER JOIN 
                       (SELECT p.parent_table , p.parent_column,r.referance_table,r.referance_column,r.id 
                        FROM 
                           (SELECT t.name referance_table,c.name referance_column,fc.constraint_object_id id 
                            FROM sys.foreign_key_columns fc, sys.tables t,sys.all_columns c 
                            WHERE fc.referenced_object_id=t.object_id 
                              and fc.referenced_column_id=c.column_id 
                              and fc.referenced_object_id=c.object_id) r 
                         LEFT OUTER JOIN 
                               (SELECT t.name parent_table,c.name parent_column,fc.constraint_object_id id 
                                FROM sys.foreign_key_columns fc, sys.tables t,sys.all_columns c
                                WHERE fc.parent_object_id=t.object_id 
                                  and fc.parent_column_id=c.column_id 
                                  and fc.parent_object_id=c.object_id) p 
                         ON p.id=r.id) foriegen 
                        ON foriegen.id=f.object_id;";
            }
        }

        public string SelectPk
        {
            get
            {
                return
                     @"SELECT t.name as " + KeyNames.TableName +
                              @",c.name as " + KeyNames.ColumnName +
                              @", k.type as " + KeyNames.Type +
                              @",k.name as " + KeyNames.KeyName +
                              @",k.type_desc as " + KeyNames.TypeDescription +
                   @" FROM sys.key_constraints k, sys.all_columns c,sys.tables t 
                      WHERE c.object_id=k.parent_object_id 
                      and c.column_id=k.unique_index_id 
                      and k.parent_object_id=t.object_id;";

            }
        }

        public string SelectTrigger
        {
            get
            {
                return
                    @"SELECT t.name table_name,triger.triger_name,triger.triger_event,triger.type,triger.type_desc 
                      FROM 
                         (SELECT te.type_desc triger_event,tr.name triger_name, tr.parent_id,tr.type,tr.type_desc 
                          FROM sys.triggers tr 
                          LEFT OUTER JOIN sys.trigger_events te 
                          ON te.object_id=tr.object_id) triger ,sys.tables t 
                    WHERE t.object_id=triger.parent_id;";
            }
        }

        public string SelectIndex
        {
            get
            {
                return
                    @"SELECT index_table.table_name, c.name column_name,index_table.name,index_table.type_desc,index_table.is_unique,index_table.is_descending_key 
                      FROM
                         (SELECT t.name table_name,indexes.* 
                          FROM 
                             (SELECT i.object_id,i.name,i.type_desc,i.is_unique,ic.index_column_id,ic.column_id,ic.is_descending_key
                              FROM sys.indexes i,sys.index_columns ic 
                              WHERE i.object_id=ic.object_id 
                                and i.index_id=ic.index_id) indexes 
                          LEFT JOIN sys.tables t 
                          ON t.object_id=indexes.object_id 
                          WHERE t.name IS NOT NULL) index_table 
                          LEFT JOIN sys.columns c  
                          ON c.column_id=index_table.column_id 
                            and c.object_id=index_table.object_id ;";
            }
        }

        public string SelectView
        {
            get
            {
                return
                    @"SELECT col.table_name,col.column_name,col.type_name, col.max_length, col.is_nullable,col.is_identity, identy.increment_value 
                      FROM(SELECT t.object_id,c.column_id, t.name table_name,c.name column_name,ty.name type_name, c.max_length, c.is_nullable,c.is_identity 
                           FROM sys.types ty ,sys.views t,sys.columns c 
                           WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id) col 
                      LEFT JOIN  sys.identity_columns identy 
                      ON identy.object_id=col.object_id and identy.column_id=col.column_id;";
            }
        }

        public string SelectProcedure
        {
            get
            {
                return
                    @"SELECT pr.procedure_name,pr.parametr_name,pr.type,pr.type_desc,ty.name,pr.max_length,pr.precision,pr.scale
                      FROM
                          (SELECT p.name procedure_name,p.type ,p.type_desc, param.name parametr_name,param.system_type_id,param.max_length,param.precision,param.scale
                           FROM sys.procedures p
                           LEFT OUTER JOIN  sys.parameters param
                           ON param.object_id=p.object_id) pr
                       LEFT OUTER JOIN sys.types ty 
                       ON ty.system_type_id=pr.system_type_id;";
            }
        }

        public string SelectViewTriggers
        {
            get
            {
                return
                    @"SELECT t.name table_name,triger.triger_name,triger.triger_event,triger.type,triger.type_desc 
                      FROM 
                         (SELECT te.type_desc triger_event,tr.name triger_name, tr.parent_id,tr.type,tr.type_desc 
                          FROM sys.triggers tr 
                          LEFT OUTER JOIN sys.trigger_events te 
                          ON te.object_id=tr.object_id) triger ,sys.views t 
                      WHERE t.object_id=triger.parent_id;";
            }
        }

        public string SelectViewIndexes
        {
            get
            {
                return
                    @"SELECT index_table.table_name, c.name column_name,index_table.name,index_table.type_desc,index_table.is_unique,index_table.is_descending_key 
                     FROM
                     (SELECT t.name table_name,indexes.* 
                      FROM 
                         (SELECT i.object_id,i.name,i.type_desc,i.is_unique,ic.index_column_id,ic.column_id,ic.is_descending_key
                          FROM sys.indexes i,sys.index_columns ic 
                          WHERE i.object_id=ic.object_id 
                            and i.index_id=ic.index_id) indexes 
                      LEFT JOIN sys.views t 
                      ON t.object_id=indexes.object_id 
                      WHERE t.name IS NOT NULL) index_table 
                      LEFT JOIN sys.columns c  
                      ON c.column_id=index_table.column_id 
                        and c.object_id=index_table.object_id ;";
            }
        }

        public string SelectFunction
        {
            get
            {
                return
                    @"SELECT fun.function_name,fun.parametr_name,fun.type,fun.type_desc,ty.name,fun.max_length,fun.precision,fun.scale
                      FROM
                      (SELECT f.object_id, f.function_name,f.type ,f.type_desc, param.name parametr_name,param.system_type_id,param.max_length,param.precision,param.scale
                      FROM
                      (SELECT o.object_id, o.name function_name,o.type ,o.type_desc 
                       FROM sys.objects o
                       WHERE type_desc like '%fun%') f
                        LEFT JOIN sys.parameters param
                        ON param.object_id=f.object_id)fun
                        LEFT JOIN  sys.types ty 
                        ON ty.system_type_id=fun.system_type_id
                        WHERE fun.parametr_name!='';";
            }
        }
    }
}
