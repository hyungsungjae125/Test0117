using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class AdminMenuForm : Form
    {
        public AdminMenuForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new AdminMenuView(load.GetHandler("adminmenu")); };
        }
    }
}
