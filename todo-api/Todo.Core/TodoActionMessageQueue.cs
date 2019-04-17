using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Core
{
    public class TodoActionMessageQueue
    {
        public string CorrelationId { get; set; }
        public int Id { get; set; }
        public int State { get; set; }
    }
}
