using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayinTarlasi
{
    public class FButton: Button
    {
        public int Count { get; set; } = 0;
        public bool IsOpened { get; set; } = false;
        public bool IsMined { get; set; } = false;
        public bool IsFlagged { get; set; } = false;


    }
}
