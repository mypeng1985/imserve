using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImWebUI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImWebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromForm]IFormCollection file)
        {
            var result = new
            {
                code = 1,
                msg = "请选择需要上传的图片",
                data = new { src = "" }
            };
            string dir = Path.Combine("upload", "image", DateTime.Now.ToString("yyyyMM"));
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", dir);
            if (file.Files.Count <= 0)
            {
                return Ok(result);
            }
            var f = file.Files.First();
            var x = f.FileName.LastIndexOf('.') + 1;
            var ext = f.FileName.Substring(x);
            var exts = new[] { "jpg", "png", "bmp" };
            if (!exts.Contains(ext))
            {
                result = new
                {
                    code = 1,
                    msg = "上传图片类型不正确，只允许" + string.Join(",", exts),
                    data = new { src = "" }
                };
                return Ok(result);
            }
            if (f.Length > 1024 * 512)
            {
                result = new
                {
                    code = 1,
                    msg = "上传图片超过大小限制",
                    data = new { src = "" }
                };
                return Ok(result);

            }
            if (!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var sr = f.OpenReadStream();
            byte[] buff = new byte[sr.Length];
            var ret = sr.ReadAsync(buff, 0, buff.Length).Result;

            string fileName = $"i_{DateTime.Now.ToFileTimeUtc()}.{ext}";
            path = Path.Combine(path, fileName);
            System.IO.File.WriteAllBytes(path, buff);
            result = new
            {
                code = 0,
                msg = "ok",
                data = new { src = $"{dir.Replace("\\", "/")}/{fileName}" }
            };
            return Ok(result);
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromForm]IFormCollection file)
        {
            var result = new
            {
                code = 1,
                msg = "请选择需要上传的文件",
                data = new { src = "" }
            };
            string dir = Path.Combine("upload", "file", DateTime.Now.ToString("yyyyMM"));
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", dir);
            if (file.Files.Count <= 0)
            {
                return Ok(result);
            }
            var f = file.Files.First();
            var x = f.FileName.LastIndexOf('.') + 1;
            var ext = f.FileName.Substring(x);
            if (f.Length > 1024 * 1024 * 10)
            {
                result = new
                {
                    code = 1,
                    msg = "上传文件超过大小限制",
                    data = new { src = "" }
                };
                return Ok(result);

            }
            if (!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var sr = f.OpenReadStream();
            byte[] buff = new byte[sr.Length];
            var ret = sr.ReadAsync(buff, 0, buff.Length).Result;

            string fileName = $"i_{DateTime.Now.ToFileTimeUtc()}.{ext}";
            path = Path.Combine(path, fileName);
            System.IO.File.WriteAllBytes(path, buff);
            result = new
            {
                code = 0,
                msg = "ok",
                data = new { src = $"{dir.Replace("\\", "/")}/{fileName}" }
            };
            return Ok(result);
        }
    }
}