using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameShared.Components
{
    public class Movement : Component
    {
        public Movement(float moveRate, float rotateRate)
        {
            this.moveRate = moveRate;
            this.rotateRate = rotateRate;
        }

        public float moveRate { get; private set; }
        public float rotateRate { get; private set; }
    }
}
