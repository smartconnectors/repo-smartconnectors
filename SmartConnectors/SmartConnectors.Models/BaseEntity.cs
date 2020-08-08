using System;

namespace SmartConnectors.Models
{
    public class BaseEntity
    {        
        public int Id { get; set; }

        public int IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
    }
}
