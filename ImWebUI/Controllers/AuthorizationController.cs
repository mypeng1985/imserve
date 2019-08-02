using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImWebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public string Ip => this.Request.Headers["X-Real-IP"].FirstOrDefault() ?? this.Request.HttpContext.Connection.RemoteIpAddress.ToString();

        public dynamic Get([FromForm] Guid? websocketId)
        {
            if (websocketId == null) websocketId = Guid.NewGuid();
            var wsserver = ImHelper.PrevConnectServer(websocketId.Value, this.Ip);
            return new
            {
                code = 0,
                server = wsserver,
                websocketId
            };
        }
    }
}