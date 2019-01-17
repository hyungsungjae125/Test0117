using OBK.Views.StaffView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.StaffForm
{
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
            StaffLoad load = new StaffLoad(this);
            Load += (a,b) => { new StaffView(load.GetHandler("staff")); };
        }
    }
}
