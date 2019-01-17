using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class MenuDeleteForm : Form
    {
        public MenuDeleteForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new MenuDeleteView(load.GetHandler("menudelete")); };
        }
    }
}
