using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class SoldoutListForm : Form
    {
        public SoldoutListForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new SoldoutListView(load.GetHandler("soldoutlist")); };
        }
    }
}
