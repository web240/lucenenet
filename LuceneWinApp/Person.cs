using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuceneWinApp
{
    [Serializable]
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
