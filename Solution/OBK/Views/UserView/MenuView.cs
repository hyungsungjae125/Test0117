using OBK.Forms;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views
{
    class MenuView
    {
        private Form parentForm, targetForm;
        private UserView uv;
        private Draw draw;
        private Hashtable hashtable;
        private int cNo;
        private WebAPI api;
        //private Timer timer = new Timer();

        public MenuView(Form parentForm, Form targetForm, int cNo, UserView uv)
        {
            this.parentForm = parentForm;
            this.targetForm = targetForm;
            this.uv = uv;
            this.cNo = cNo;
            draw = new Draw();
            //timer.Interval = 5000;
            //timer.Tick += new EventHandler(getView);
            //timer.Start();
            //parentForm.FormClosed += ParentForm_FormClosed;
            getView();
        }

        //private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    timer.Stop();
        //}

        private void getView()//(object o, EventArgs e)
        {
            api = new WebAPI();
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(170, 170));
            hashtable.Add("color", Color.BurlyWood);
            hashtable.Add("click", (EventHandler)menu_click);
            hashtable.Add("font", new Font("굴림", 12, FontStyle.Bold));
            Hashtable ht = new Hashtable();
            ht.Add("cNo", cNo);
            Hashtable hashtable2 = new Hashtable();
            hashtable2.Add("click", (EventHandler)menu_click);
            //targetForm.Controls.Clear();
            if (!api.MenuPrint(Program.serverUrl + "menu/select", ht, hashtable, hashtable2, targetForm))
            {
                MessageBox.Show("메뉴 불러오기 실패...");
            }
        }
        private void menu_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Menuclick(button.Name);
        }

        private void Menuclick(string name)
        {
            string mName = name.Substring(name.IndexOf("_") + 1);

            ChoiceForm choiceForm = new ChoiceForm(mName);
            choiceForm.StartPosition = FormStartPosition.Manual;
            choiceForm.Location = new Point(parentForm.Location.X + (parentForm.Width / 2) - (choiceForm.Width / 4), parentForm.Location.Y + (parentForm.Height / 2) - (choiceForm.Height / 2));
            choiceForm.ShowDialog();
            uv.tt();
        }

    }
}
