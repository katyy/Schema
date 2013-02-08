using System.IO;

namespace Shema.Server
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    using Models;

    public class ServerGetter
    {
        const string ServersFileName = "servers";

        public static List<string> GetServerNamesFromConfigFile()
        {
            var stream = new FileStream(ServersFileName, FileMode.OpenOrCreate, FileAccess.Read);
            var reader = new StreamReader(stream);
            var allServers = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return !string.IsNullOrEmpty(allServers) ? allServers.Split(';').Where(s => !string.IsNullOrEmpty(s)).ToList() : null;
        }

        private static void WriteServerNamesToFile(IEnumerable<string> serverNames )
        {
            var stream = new FileStream(ServersFileName, FileMode.Truncate, FileAccess.Write);
            var writer = new StreamWriter(stream);
            foreach (var serverName in serverNames)
            {
                writer.Write(string.Format("{0};",serverName));
            }

            writer.Close();
            stream.Close();
           
        }

        public static List<string> GetMsSqlServerNames()
        {
            var dataTable = SmoApplication.EnumAvailableSqlServers();
            var serverNames=(from DataRow dr in dataTable.Rows select dr["Name"].ToString()).ToList();
            WriteServerNamesToFile(serverNames);
            return serverNames;
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
            catch (Exception)
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
