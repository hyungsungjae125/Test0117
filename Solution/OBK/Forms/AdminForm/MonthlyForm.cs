using OBK.Views.AdminView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.AdminForm
{
    public partial class MonthlyForm : Form
    {
        public MonthlyForm()
        {
            InitializeComponent();
            AdminLoad load = new AdminLoad(this);
            Load += (a,b) => { new MonthlyView(load.GetHandler("monthly")); };
        }
    }
}
