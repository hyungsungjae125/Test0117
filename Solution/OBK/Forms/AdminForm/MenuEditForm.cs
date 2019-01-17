using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class MenuEditForm : Form
    {
        public MenuEditForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new MenuEditView(load.GetHandler("menuedit")); };
        }
    }
}
