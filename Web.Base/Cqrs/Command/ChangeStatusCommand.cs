using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Web.Base.Cqrs.Command
{
    public class ChangeStatusCommand : ICommand
    {
        public int TaskId { get; set; } 
        public bool IsCompleted { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
