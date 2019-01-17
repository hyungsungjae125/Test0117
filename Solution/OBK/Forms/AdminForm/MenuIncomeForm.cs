using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class MenuIncomeForm : Form
    {
        public MenuIncomeForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new MenuIncomView(load.GetHandler("menuincome")); };
        }
    }
}
