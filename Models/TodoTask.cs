using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETWebApplication.Models
{
    public class TodoTask
    {
        public TodoTask()
        {

        }

        public TodoTask(int id, string title, string description, string location, string addedDate)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Location = location;
            this.AddedDate = DateTime.Parse(addedDate);
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
