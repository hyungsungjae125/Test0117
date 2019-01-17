using OBK.Views.StaffView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.StaffForm
{
    public partial class SoldoutDeleteForm : Form
    {
        public SoldoutDeleteForm()
        {
            InitializeComponent();
            StaffLoad load = new StaffLoad(this);
            Load += (a,b) => { new SoldoutDeleteView(load.GetHandler("SoldoutDelete")); };
        }
    }
}
