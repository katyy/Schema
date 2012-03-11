namespace Schema.Core
{
    public class SQL
    {
        public const string SelectColumn =
            "SELECT t.name,c.name,ty.name, c.max_length, c.is_nullable,c.is_identity  FROM sys.types ty ,sys.tables t,sys.columns c WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id;";
        public const string SelectPk =
            "SELECT t.name,c.name, k.type,k.name,k.type_desc FROM sys.key_constraints k,  sys.all_columns c,sys.tables t where c.object_id=k.parent_object_id and c.column_id=k.unique_index_id and k.parent_object_id=t.object_id;";
        public const string SelectFk =
            "SELECT foriegen.parent_table,parent_column,f.type,name,f.type_desc,f.delete_referential_action_desc,f.update_referential_action_desc, foriegen.referance_table, foriegen.referance_column from sys.foreign_keys f  LEFT OUTER JOIN (select p.parent_table , p.parent_column,r.referance_table,r.referance_column,r.id from (select t.name referance_table,c.name referance_column,fc.constraint_object_id id from sys.foreign_key_columns fc, sys.tables t,sys.all_columns c where fc.referenced_object_id=t.object_id and fc.referenced_column_id=c.column_id and fc.referenced_object_id=c.object_id) r LEFT OUTER JOIN (select t.name parent_table,c.name parent_column,fc.constraint_object_id id from sys.foreign_key_columns fc, sys.tables t,sys.all_columns c where fc.parent_object_id=t.object_id and fc.parent_column_id=c.column_id and fc.parent_object_id=c.object_id) p ON p.id=r.id) foriegen on foriegen.id=f.object_id;";
        public const string SelectTriger =
            "SELECT t.name table_name,triger.triger_name,triger.triger_event,triger.type,triger.type_desc FROM ( SELECT te.type_desc triger_event,tr.name triger_name, tr.parent_id,tr.type,tr.type_desc FROM sys.triggers tr LEFT OUTER JOIN sys.trigger_events te ON te.object_id=tr.object_id) triger ,sys.tables t WHERE t.object_id=triger.parent_id;";
        public const string SelectIndex = "SELECT index_table.table_name, c.name column_name,index_table.name,index_table.type_desc,index_table.is_unique,index_table.is_descending_key FROM(SELECT t.name table_name,indexes.* FROM (SELECT i.object_id,i.name,i.type_desc,i.is_unique,ic.index_column_id,ic.column_id,ic.is_descending_key  FROM sys.indexes i,sys.index_columns ic WHERE i.object_id=ic.object_id and i.index_id=ic.index_id) indexes LEFT JOIN sys.tables t ON t.object_id=indexes.object_id WHERE t.name IS NOT NULL) index_table LEFT JOIN sys.columns c  ON c.column_id=index_table.column_id and c.object_id=index_table.object_id ;";
        public const string SelectView = "SELECT v.name,v.type_desc FROM sys.views v;";
    }
}
