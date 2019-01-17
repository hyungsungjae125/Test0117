using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class MenuAddForm : Form
    {
        public MenuAddForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new MenuAddView(load.GetHandler("menuadd")); };
        }
    }
}
