using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImWebUI.Model
{
    public class MineModel
    {
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public Guid Id { get; set; }
        public bool Mine { get; set; }
        public string Content { get; set; }
    }
}
