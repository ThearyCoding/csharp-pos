using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginwithSqlServer
{
    public class People
    {
        public string id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string email { get; set; }


        public People(string id, string name, string gender, string phone, string email)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.phone = phone;
            this.email = email;
        }


    }
}
