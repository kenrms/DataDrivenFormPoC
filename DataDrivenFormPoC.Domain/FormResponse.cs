﻿using System;

namespace DataDrivenFormPoC.Domain
{
    public class FormResponse
    {
        public int Id { get; set; }
        public Form Form { get; set; }
        public int FormId { get; set; }
        public DateTimeOffset DateSubmitted { get; set; }
    }
}