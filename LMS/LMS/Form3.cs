using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS
{
    public partial class Form3 : MaterialForm
    {
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;


        public Form3()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo500, MaterialSkin.Primary.Indigo500, MaterialSkin.Primary.Indigo200, MaterialSkin.Accent.Pink200,MaterialSkin.TextShade.WHITE);
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
