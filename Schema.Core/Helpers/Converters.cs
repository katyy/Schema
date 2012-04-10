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

       public static SortOrder OrderDirection(object val)
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

           return SortOrder.Ascending;
       }
    }
}
