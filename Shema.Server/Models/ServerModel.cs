namespace Shema.Server.Models
{
    using System.Collections.Generic;
    


    public class ServerModel
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<string> DatabasesName { get; set; }
    }
}
