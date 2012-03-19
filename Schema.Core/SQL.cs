namespace Schema.Core
{
    public class SQL
    {
        public const string SelectColumn =
            @"SELECT col.table_name,col.column_name,col.type_name, col.max_length, col.is_nullable,col.is_identity, identy.increment_value 
              FROM(SELECT t.object_id,c.column_id, t.name table_name,c.name column_name,ty.name type_name, c.max_length, c.is_nullable,c.is_identity 
                   FROM sys.types ty ,sys.tables t,sys.columns c 
                   WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id) col 
              LEFT JOIN  sys.identity_columns identy 
              ON identy.object_id=col.object_id and identy.column_id=col.column_id;";
        
        public const string SelectPk =
            @"SELECT t.name,c.name, k.type,k.name,k.type_desc 
              FROM sys.key_constraints k, sys.all_columns c,sys.tables t 
              WHERE c.object_id=k.parent_object_id 
                   and c.column_id=k.unique_index_id 
                   and k.parent_object_id=t.object_id;";

        public const string SelectFk =
            @"SELECT foriegen.parent_table,parent_column,f.type,name,f.type_desc,f.delete_referential_action_desc,f.update_referential_action_desc, foriegen.referance_table, foriegen.referance_column
              FROM sys.foreign_keys f  
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

        public const string SelectTrigger =
            @"SELECT t.name table_name,triger.triger_name,triger.triger_event,triger.type,triger.type_desc 
              FROM 
                 (SELECT te.type_desc triger_event,tr.name triger_name, tr.parent_id,tr.type,tr.type_desc 
                  FROM sys.triggers tr 
                  LEFT OUTER JOIN sys.trigger_events te 
                  ON te.object_id=tr.object_id) triger ,sys.tables t 
            WHERE t.object_id=triger.parent_id;";

        public const string SelectIndex = 
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

        public const string SelectView =
            @"SELECT col.table_name,col.column_name,col.type_name, col.max_length, col.is_nullable,col.is_identity, identy.increment_value 
              FROM(SELECT t.object_id,c.column_id, t.name table_name,c.name column_name,ty.name type_name, c.max_length, c.is_nullable,c.is_identity 
                   FROM sys.types ty ,sys.views t,sys.columns c 
                   WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id) col 
              LEFT JOIN  sys.identity_columns identy 
              ON identy.object_id=col.object_id and identy.column_id=col.column_id;";
           
        public const string SelectProcedure =
            @"SELECT pr.procedure_name,pr.parametr_name,pr.type,pr.type_desc,ty.name,pr.max_length,pr.precision,pr.scale
              FROM
                  (SELECT p.name procedure_name,p.type ,p.type_desc, param.name parametr_name,param.system_type_id,param.max_length,param.precision,param.scale
                   FROM sys.procedures p
                   LEFT OUTER JOIN  sys.parameters param
                   ON param.object_id=p.object_id) pr
               LEFT OUTER JOIN sys.types ty 
               ON ty.system_type_id=pr.system_type_id;";

        public const string SelectViewTriggers =
            @"SELECT t.name table_name,triger.triger_name,triger.triger_event,triger.type,triger.type_desc 
              FROM 
                 (SELECT te.type_desc triger_event,tr.name triger_name, tr.parent_id,tr.type,tr.type_desc 
                  FROM sys.triggers tr 
                  LEFT OUTER JOIN sys.trigger_events te 
                  ON te.object_id=tr.object_id) triger ,sys.views t 
            WHERE t.object_id=triger.parent_id;";

        public const string SelectViewIndexes =
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
