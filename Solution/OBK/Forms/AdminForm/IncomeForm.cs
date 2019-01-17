using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class IncomeForm : Form
    {
        public IncomeForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new IncomeView(load.GetHandler("income")); };
        }
    }
}
