using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class MenuSettingForm : Form
    {
        public MenuSettingForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new MenuSettingView(load.GetHandler("menusetting")); };
        }
    }
}
