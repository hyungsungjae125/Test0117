using OBK.Views;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += (a,b) => { new UserView(load.GetHandler("user")); };
        }
    }
}
