namespace Shema.Server.Models
{
    using System.Collections.Generic;

    public class ServerModel
    {
        public string ServerName { get; set; }

        public List<string> DatabasesName { get; set; }
    }
}
