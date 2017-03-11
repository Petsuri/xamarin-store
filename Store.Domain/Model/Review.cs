using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public class Review
    {

        public int? Id { get; private set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public byte[] Photo { get; set; }
        
        public Review(int? id)
        {
            Id = id;
        }

    }
}
