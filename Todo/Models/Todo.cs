using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models
{
    internal class Todo
    {
        public DateTime Date { get; set; }

        public string Path { get; set; }

        private string _title;
        public string Title {
            get { return _title; }
            set { _title = value.Split("/").Last().Split(".todo-text.txt").First(); }
        }
        public string Description { get; set; }

        public TodoStates State { get; set; }
    }
}
