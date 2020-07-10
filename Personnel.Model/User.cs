using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Model
{
    public class User
    {
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
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Folk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }

        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Work_Time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Job { get; set; }
    }
}
