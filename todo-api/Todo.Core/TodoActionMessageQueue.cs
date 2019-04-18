using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Core
{
    public class TodoActionMessageQueue
    {
        public string CorrelationId { get; set; }
        public string TodoName { get; set; }
    }
}
