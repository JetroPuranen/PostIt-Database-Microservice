using PostIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostIt.Application.Dto
{
    public class PostDto
    {
        public Guid UserId { get; set; }
        public virtual Users User { get; set; }

        public byte[] ImageData { get; set; }
        public string Caption { get; set; }
    }
}
