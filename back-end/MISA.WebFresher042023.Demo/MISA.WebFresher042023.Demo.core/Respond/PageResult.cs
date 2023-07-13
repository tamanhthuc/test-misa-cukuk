using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Respond
{
    public class PageResult<TEntity>
    {
        public int TotalPage { get; set; }  

        public int TotalRecord { get; set; }

        public int CurrentPage { get; set; }    

        public int CurrentPageRecords { get; set; } 

        public List<TEntity> Data { get; set; }   

       
    }
}
