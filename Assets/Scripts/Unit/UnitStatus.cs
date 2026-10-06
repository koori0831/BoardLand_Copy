using System;
using System.Collections.Generic;
using System.Text;

namespace Unit
{
    [Serializable]
    public record struct UnitStatus
    {
        // 체력
        public int Health { get; set; }
    }
}
