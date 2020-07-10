using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Model
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Folk { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public int Work_Time { get; set; }
        public string Job { get; set; }
       
        public User(){}

        public User(int Id, string Name, string Folk, DateTime Birthday, int Age,string Phone,int Work_Time,string Job)
        {
            this.Id = Id;
            this.Name = Name;
            this.Folk = Folk;
            this.Birthday = Birthday;
            this.Age=Age;
            this.Phone=Phone;
            this.Work_Time=Work_Time;
            this.Job=Job;
        }
       
    }
}
