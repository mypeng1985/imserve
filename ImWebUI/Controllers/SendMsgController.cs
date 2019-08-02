using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImWebUI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImWebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMsgController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody] Chat chat)
        {

            var sendContent = new SendContentModel
            {
                Type = SendType.group,
                SendId = chat.Mine.Id,                
                Data = new SendGroupMsgModel
                {
                    Username = chat.Mine.UserName,
                    Avatar = chat.Mine.Avatar,
                    Content = chat.Mine.Content,
                    Id = chat.To.Id
                }
            };
     
            ImHelper.SendChanMessage(chat.Mine.Id, chat.To.Id, sendContent);
            return Ok("send ok");
        }
    }
}