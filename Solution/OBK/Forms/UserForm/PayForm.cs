using OBK.Views;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms
{
    public partial class PayForm : Form
    {
        public PayForm()
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += (a,b) => { new PayView(load.GetHandler("pay")); };
        }
    }
}
