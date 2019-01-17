using OBK.Views.StaffView;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms.StaffForm
{
    public partial class OrderListForm : Form
    {
        public OrderListForm()
        {
            InitializeComponent();
            StaffLoad load = new StaffLoad(this);
            Load += (a,b) => { new OrderListView(load.GetHandler("OrderList")); };
        }
    }
}
