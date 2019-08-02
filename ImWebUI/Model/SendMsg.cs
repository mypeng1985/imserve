using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImWebUI.Model
{

    public enum SendType
    {
        group,
        friend
    }
    public class SendContentModel
    {
         public Guid SendId { get; set; }
        public SendType Type { get; set; }
        public ContentModel Data { get; set; }
    }


    public class ContentModel
    {
        /// <summary>
        /// 消息来源用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        ///消息来源用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 嗨，你好！本消息系离线消息。
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        ///消息的来源ID（如果是私聊，则是用户id，如果是群聊，则是群组id）
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 服务端时间戳毫秒数。注意：如果你返回的是标准的 unix 时间戳，记得要 *1000
        /// </summary>
        public long Timestamp
        {
            get
            {
                DateTime dt1970 = new DateTime(1970, 1, 1, 8, 0, 0, 0);
                return (DateTime.Now.Ticks - dt1970.Ticks) / 10000;
            }
        }
    }

    public class SendGroupMsgModel: ContentModel
    {
        /// <summary>
        /// 聊天窗口来源类型，从发送消息传递的to里面获取
        /// </summary>
        public string Type { get; } = "group";

        /// <summary>
        ///消息id，可不传。除非你要对消息进行一些操作（如撤回）
        /// </summary>
        // public int Cid { get; set; }

        // public string Groupname { get; set; }
        /// <summary>
        ///是否我发送的消息，如果为true，则会显示在右方
        /// </summary>
        //   public bool Mine { get; set; }
        /// <summary>
        /// 消息的发送者id（比如群组中的某个消息发送者），可用于自动解决浏览器多窗口时的一些问题
        /// </summary>
        //  public Guid Fromid { get; set; }

    }
}
