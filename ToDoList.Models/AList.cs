﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class AList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Completion_Date { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public string User { get; set; }
        public bool SentMail { get; set; }

    }
}
