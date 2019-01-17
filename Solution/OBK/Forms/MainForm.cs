using OBK.Views;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += (a, b) => { new MainView(load.GetHandler("main")); };   
        }
    }
}
