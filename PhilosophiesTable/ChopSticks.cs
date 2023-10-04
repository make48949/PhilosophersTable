using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilosophiesTable
{
    public class ChopSticks
    {
        private bool available = true;

        public bool IsAvailable() 
        {
            return available;
        }
        
        public void SetAvailable (bool value) 
        {
            available = value;
        }
    }
}
