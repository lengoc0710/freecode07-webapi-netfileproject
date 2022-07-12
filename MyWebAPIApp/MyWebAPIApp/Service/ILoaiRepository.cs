using MyWebAPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPIApp.Service
{
    public interface ILoaiRepository
    {
        List<LoaiVm> GetAll();
        LoaiVm GetById(int id);
        LoaiVm Add(LoaiModel loai);
        void Update(LoaiVm loai);
        void Delete(int id);
    }
}
