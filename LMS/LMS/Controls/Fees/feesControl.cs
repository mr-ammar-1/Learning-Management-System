using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMS.Classes;

namespace LMS.Controls
{
    public partial class feesControl : UserControl
    {
        public feesControl()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            addFee RF = new addFee();
            mainControllerClass.showControls(RF, Content);
            Content.BringToFront();
        }
    }
}
