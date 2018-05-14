using Ss.Data.Enums;
using Ss.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            Prefix = string.Empty;
        }
        public BaseEntity(string prefix)
        {
            Prefix = prefix;
        }
        protected string Prefix { get; set; }

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Actflg Actflg { get; set; }

        //public virtual string Code
        //{
        //    get { return string.Format("{0}{1}", Prefix, Id.ToString("000000")); }
        //}
    }
}
