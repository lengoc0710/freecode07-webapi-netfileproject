using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> HangHoas = new List<HangHoa>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(HangHoas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var HangHoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (HangHoa == null)
                {
                    return NotFound();
                }
                return Ok(HangHoa);

            }
            catch
            {
                return BadRequest();
            }

                }
        [HttpPost]
        public IActionResult Create(HangHoaVm hangHoaVm)
        {
            var hanghoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVm.TenHangHoa,
                DonGia = hangHoaVm.DonGia

          };
            HangHoas.Add(hanghoa);
            return Ok(new
            {
                Success = true, Data = hanghoa
            });
        }
        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa HangHoaEdit)
        {
            try
            {
                var HangHoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (HangHoa == null)
                {
                    return NotFound();
                }
                if(id != HangHoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                HangHoa.TenHangHoa = HangHoaEdit.TenHangHoa;
                HangHoa.DonGia = HangHoaEdit.DonGia;
                return Ok();

            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                var HangHoa = HangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (HangHoa == null)
                {
                    return NotFound();
                }
                HangHoas.Remove(HangHoa);
                return Ok();
            }
            catch
            {
                return BadRequest();

            }
        }
        }
}
