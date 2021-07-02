using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ShopBridge
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }


        [StringLength(500)]
        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime AddedDateTime { get; set; }
    }
}
