using System;

namespace DataAccess.Models
{
    [Serializable]
    public class Project
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
