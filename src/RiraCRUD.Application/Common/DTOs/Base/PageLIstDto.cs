using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Common.DTOs.Base
{
    public class PageListDto<T>
    {
        public int DataCount { get; set; } = 0;
        public List<T> Data { get; set; } = new List<T>();
    }
}
