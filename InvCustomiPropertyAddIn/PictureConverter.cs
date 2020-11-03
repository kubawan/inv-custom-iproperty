using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stdole;
using System.Windows.Forms;
using System.Drawing;

namespace InvAddIn
{
    internal class PictureConverter : AxHost
    {
        private PictureConverter() : base(string.Empty)
        {

        }
        public static IPictureDisp ImageToPictureDisp(Image image)
        {
            return (IPictureDisp)GetIPictureDispFromPicture(image);
        }
    }
}
