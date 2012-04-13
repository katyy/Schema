namespace Shema.Server
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Microsoft.SqlServer.Management.Smo;

    public class ServerGetter
    {
       public static Dictionary<string, List<string>> GetServices()
        {
            var serverNames = GetMsSqlServerNames();
            return GetDataBases(serverNames);
        }

       public static List<string> GetMsSqlServerNames()
       {
           var dataTable = SmoApplication.EnumAvailableSqlServers();
           return (from DataRow dr in dataTable.Rows select dr["Name"].ToString()).ToList();
       }

       public static Dictionary<string, List<string>> GetDataBases(List<string> serverNames)
       {
           var servers = new Dictionary<string, List<string>>();
           foreach (var serverName in serverNames)
           {
               var server = new Server(serverName);

               var database = (from Database db in server.Databases select db.Name).ToList();

               servers.Add(serverName, database);
           }

           return servers;
       }

        public static List<string> GetDataBases(string serverName)
        {
           var server = new Server(serverName);
           return (from Database db in server.Databases select db.Name).ToList();
        }
    }
}
