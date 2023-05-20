using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos.Products
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSIze = 6;
        public int PageSize { get => _pageSIze; set => _pageSIze = (value > MaxPageSize) ? MaxPageSize : value; }
        private string _search;
        public string Search { 
            get => _search;
            set => _search = value.ToLower();
        }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        public string Sort { get; set; }
    }
}
