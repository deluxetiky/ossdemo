using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Model.Dto.Base
{
    public class DtoBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
