using OBK.Views;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms
{
    public partial class MenuForm : Form
    {
        public MenuForm(Form parentForm,int mNo, UserView uv)
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += (a,b) => { new MenuView(parentForm,load.GetHandler("menu"), mNo, uv); };
        }
    }
}
