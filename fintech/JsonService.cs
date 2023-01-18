using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class JsonService
    {

        public List<User> GetUsersFromJSON()
        {
            if (File.Exists(@"C:\Users\yahya\source\repos\fintech/users.json"))
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\yahya\source\repos\fintech/users.json"))
                {
                    string JSONValue = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<User>>(JSONValue);
                }
            }
            return new();
            
        }
        public void WriteUsersToJSON(List<User> users)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\yahya\source\repos\fintech\users.json"))
            {
                string JSONValue = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                sw.Write(JSONValue);
            }
        }
    }
}
