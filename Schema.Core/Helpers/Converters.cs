using System;

namespace Schema.Core.Helpers
{
   public  class Converters
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
           }
           return (bool)boolValue;
       }

       public static bool AscDescToBool(object val)
       {
           switch (val.ToString().ToUpper())
           {
               case "A":
                   return false;
               case "D":
                   return true;
           }
           return (bool)val;
       }
    }
}
