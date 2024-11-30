using RiraCRUD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Common.DTOs.Base
{
    public class GridFilterDto
    {
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
        public Dictionary<string, (FilterOperator Operator, object Value)> Filters { get; set; }
    }
}
