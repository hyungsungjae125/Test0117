using OBK.Views.StaffView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.StaffForm
{
    public partial class SoldoutAddForm : Form
    {
        public SoldoutAddForm()
        {
            InitializeComponent();
            StaffLoad load = new StaffLoad(this);
            Load += (a,b) => { new SoldoutAddView(load.GetHandler("SoldoutAdd")); };
        }
    }
}
