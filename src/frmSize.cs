using System.Windows.Forms;

namespace DrawEA
{
    public partial class frmSize : Form
    {
        public frmSize()
        {
            InitializeComponent();
        }

        public int Value
        {
            get { return (int)nudSize.Value; }
            set { nudSize.Value = value; }
        }
    }
}