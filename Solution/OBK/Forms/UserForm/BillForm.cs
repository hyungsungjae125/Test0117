using OBK.Views;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms
{
    public partial class BillForm : Form
    {
        public BillForm()
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += (a,b) => { new BillView(load.GetHandler("bill")); };
        }
    }
}
