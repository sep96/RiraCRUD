using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Domain.Base
{
    public abstract class BaseEntity
    {
        public long Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        protected BaseEntity()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
