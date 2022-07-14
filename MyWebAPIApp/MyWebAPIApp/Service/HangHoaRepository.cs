using Microsoft.EntityFrameworkCore;
using MyWebAPIApp.Data;
using MyWebAPIApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebAPIApp.Service
{
    public class HangHoaRepository : IHangHoaRepository
    {
        static List<HangHoaModel> hangHoas = new List<HangHoaModel>
        {
            new HangHoaModel{TenHangHoa = "banhmipate" , DonGia = 10000 },
            new HangHoaModel{TenHangHoa = "banhmipatetrung" , DonGia = 12000 },
            new HangHoaModel{TenHangHoa = "banhmicha" , DonGia = 11000 },
            new HangHoaModel{TenHangHoa = "banhminuongmuoiot" , DonGia = 10000 },
            new HangHoaModel{TenHangHoa = "xoilac" , DonGia = 10000 },
            new HangHoaModel{TenHangHoa = "xoixiu" , DonGia = 15000 },
            new HangHoaModel{TenHangHoa = "xoixeo" , DonGia = 12000 },
            new HangHoaModel{TenHangHoa = "mitombo" , DonGia = 20000 },
            new HangHoaModel{TenHangHoa = "mitomxaxiu" , DonGia = 30000 },
            new HangHoaModel{TenHangHoa = "banhcuon" , DonGia = 18000 },
            new HangHoaModel{TenHangHoa = "suadau" , DonGia = 8000 },
            new HangHoaModel{TenHangHoa = "nuocmia" , DonGia = 10000 },
};
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public HangHoaRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<HangHoaModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProducts = _context.HangHoas.Include(hh => hh.Loai).AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(hh => hh.Tenhh.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia <= to);
            }
            #endregion
            #region Sorting
            //Default sort by Name (TenHh)
            allProducts = allProducts.OrderBy(hh => hh.Tenhh);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tenhh_desc": allProducts = allProducts.OrderByDescending(hh => hh.Tenhh); break;
                    case "gia_asc": allProducts = allProducts.OrderBy(hh => hh.DonGia); break;
                    case "gia_desc": allProducts = allProducts.OrderByDescending(hh => hh.DonGia); break;
                }
            }
            #endregion
            //#region Paging
            //allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            //#endregion

            //var result = allProducts.Select(hh => new HangHoaModel
            //{
            //    MaHangHoa = hh.MaHh,
            //    TenHangHoa = hh.TenHh,
            //    DonGia = hh.DonGia,
            //    TenLoai = hh.Loai.TenLoai
            //});

            //return result.ToList();

            var result = PaginatedList<MyWebAPIApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(hh => new HangHoaModel
            {
                MaHangHoa = hh.MaHangHoa,
                TenHangHoa = hh.Tenhh,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai?.TenLoai
            }).ToList();
            
        }
    }
}
