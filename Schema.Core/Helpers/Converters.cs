namespace Schema.Core.Helpers
{
    using System;

    using Schema.Core.Enums;

    public class Converters
    {
        public static int? ToInt(object intValue)
        {
            int? value = null;

            if (intValue != DBNull.Value)
            {
                value = Convert.ToInt32(intValue);
            }

            return value;
        }

        public static bool ToBool(object boolValue)
        {
            switch (boolValue.ToString().ToUpper())
            {
                case "YES":
                    return true;
                case "NO":
                    return false;
                case "":
                    return false;
            }

            return (bool)boolValue;
        }

        public static SortOrder? OrderDirection(object val)
        {
            switch (val.ToString().ToUpper())
            {
                case "A":
                    return SortOrder.Ascending;
                case "D":
                    return SortOrder.Descending;
                case "0":
                    return SortOrder.Ascending;
                case "1":
                    return SortOrder.Descending;
            }

            return null;
        }


        public static KeyType? ConstraintType(object val)
        {
            switch (val.ToString().ToUpper())
            {
                case "PRIMARY KEY":
                    return KeyType.PrimaryKey;

                case "FOREIGN KEY":
                    return KeyType.ForeigenKey;

                case "UNIQUE":
                    return KeyType.Unique;

                case "PRIMARY_KEY_CONSTRAINT":
                    return KeyType.PrimaryKey;

                case "FOREIGN_KEY_CONSTRAINT":
                    return KeyType.ForeigenKey;

                case "UNIQUE_CONSTRAINT":
                    return KeyType.Unique;
            }

            return null;
        }

        public static EventRule UpdateDeleteRule(object val)
        {
            switch (val.ToString().ToUpper())
            {
                case "CASCADE":
                    return EventRule.Cascade;

                case "RESTRICT":
                    return EventRule.Restrict;

                case "NO ACTION":
                    return EventRule.NoAction;

                case "SET NULL":
                    return EventRule.SetNull;

                case "SET DEFAULT":
                    return EventRule.SetDefault;

                case "NO_ACTION":
                    return EventRule.NoAction;

                case "SET_NULL":
                    return EventRule.SetNull;

                case "SET_DEFAULT":
                    return EventRule.SetDefault;
            }

            return EventRule.NoAction;
        }

        public static TriggerEvent? TriggerEventManipulation(object val)
        {
            switch (val.ToString().ToUpper())
            {
                case "Insert":
                    return TriggerEvent.Insert;
                case "UPDATE":
                    return TriggerEvent.Update;
                case "Delete":
                    return TriggerEvent.Delete;
            }

            return null;
        }
        
        public static IndexType? IndexTypeDescription(object val)
        {
            switch (val.ToString().ToUpper())
            {
                case "CLUSTERED":
                    return IndexType.Clustered;

                case "NONCLUSTERED":
                    return IndexType.NonClustered;

                case "BTREE":
                    return IndexType.Btree;
            }
            return null;
        }
    }
}
