using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Dictionary<String, int> Assets { get; set; }
        public Dictionary<String, double> Benchmark { get; set; }

    }
}
