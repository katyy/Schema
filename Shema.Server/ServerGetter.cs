namespace Shema.Server
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    using Shema.Server.Models;

    public class ServerGetter
    {
       //public static Dictionary<string, List<string>> GetServices()
       // {
       //     var serverNames = GetMsSqlServerNames();
       //     return GetDataBases(serverNames);
       // }

       public static List<string> GetMsSqlServerNames()
       {
           var dataTable = SmoApplication.EnumAvailableSqlServers();
           return (from DataRow dr in dataTable.Rows select dr["Name"].ToString()).ToList();
       }

       public static Dictionary<string, List<string>> GetDataBases(List<ServerModel> server)
       {
           var serverList = new Dictionary<string, List<string>>();
           foreach (var serverModel in server)
           {
               var oneServer = string.IsNullOrWhiteSpace(serverModel.UserName) ? 
                                new Server(serverModel.Name) : 
                                new Server(new ServerConnection(serverModel.Name, serverModel.UserName, serverModel.Password));

               var database = (from Database db in oneServer.Databases select db.Name).ToList();

               serverList.Add(serverModel.Name, database);
           }

           return serverList;
       }

       public static List<string> GetDataBases(ServerModel model)
       {
           Server server;
            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
            { 
                server = new Server(new ServerConnection(model.Name, model.UserName, model.Password));
            }
            else
            {
                server = new Server(model.Name);
            }

           try
           {
               return (from Database db in server.Databases select db.Name).ToList();
           }
           catch (Exception e)
           {
               return new List<string>();
           }
       }

        public static List<string> GetDataBases(string serverName)
        {
            var server = new Server(serverName);
            return (from Database db in server.Databases select db.Name).ToList();
        }
    }
}
