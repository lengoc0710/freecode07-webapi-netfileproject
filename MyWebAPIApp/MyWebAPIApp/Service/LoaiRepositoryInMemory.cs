using MyWebAPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPIApp.Service
{
    public class LoaiRepositoryInMemory : ILoaiRepository
    {

        static List<LoaiVm> loais = new List<LoaiVm>
        {
            new LoaiVm{MaLoai = 1, TenLoai = "Tivi"},
            new LoaiVm{MaLoai = 2, TenLoai = "Tủ lạnh"},
            new LoaiVm{MaLoai = 3, TenLoai = "Điều hòa"},
            new LoaiVm{MaLoai = 4, TenLoai = "Máy giặt"},
            new LoaiVm{MaLoai = 5, TenLoai = "Điện thoại"},
            new LoaiVm{MaLoai = 6, TenLoai = "Nồi cơm"},
        };

        public LoaiVm Add(LoaiModel loai)
        {
            var _loai = new LoaiVm
            {
                MaLoai = loais.Max(lo => lo.MaLoai) + 1,
                TenLoai = loai.TenLoai
            };
            loais.Add(_loai);
            return _loai;
        }

        public void Delete(int id)
        {
            var _loai = loais.SingleOrDefault(lo => lo.MaLoai == id);
            loais.Remove(_loai);
        }

        public List<LoaiVm> GetAll()
        {
            return loais;
        }

        public LoaiVm GetById(int id)
        {
            return loais.SingleOrDefault(lo => lo.MaLoai == id);
        }

        public void Update(LoaiVm loai)
        {
            var _loai = loais.SingleOrDefault(lo => lo.MaLoai == loai.MaLoai);
            if (_loai != null)
            {
                _loai.TenLoai = loai.TenLoai;
            }

        }
    }
    
}
