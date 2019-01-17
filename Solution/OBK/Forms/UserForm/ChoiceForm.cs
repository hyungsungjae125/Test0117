using OBK.Views;
using ObkLibrary;
using System.Windows.Forms;

namespace OBK.Forms
{
    public partial class ChoiceForm : Form
    {
        public ChoiceForm()
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += (a, b) => { new ChoiceView(load.GetHandler("choice")); };
        }

        public ChoiceForm(string mName)
        {
            InitializeComponent();
            Load load = new Load(this);
            Load += (a,b) => { new ChoiceView(load.GetHandler("choice"),mName); };
        }
    }
}
