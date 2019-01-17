using OBK.Forms;
using OBK.Forms.AdminForm;
using OBK.Forms.StaffForm;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views
{
    class MainView
    {
        private Draw draw;
        private Button btn1, btn2, btn3;
        private Form parentForm, tagetForm;
        private Hashtable hashtable;

        public MainView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }
        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 300));
            hashtable.Add("point", new Point(10, 10));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btn1");
            hashtable.Add("text", "사용자");
            hashtable.Add("click", (EventHandler)btn1_click);
            btn1 = draw.getButton(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 300));
            hashtable.Add("point", new Point(170, 10));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btn2");
            hashtable.Add("text", "매장");
            hashtable.Add("click", (EventHandler)btn2_click);
            btn2 = draw.getButton(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 300));
            hashtable.Add("point", new Point(330, 10));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btn3");
            hashtable.Add("text", "관리자");
            hashtable.Add("click", (EventHandler)btn3_click);
            btn3 = draw.getButton(hashtable, parentForm);
        }

        private void btn1_click(object o, EventArgs a)
        {
            parentForm.Visible = false;

            tagetForm = new UserForm();
            tagetForm.StartPosition = FormStartPosition.CenterParent;
            tagetForm.FormClosed += new FormClosedEventHandler(Exit_click);
            tagetForm.Show();
        }
        

        private void btn2_click(object o, EventArgs a)
        {
            parentForm.Visible = false;

            tagetForm = new StaffForm();
            tagetForm.StartPosition = FormStartPosition.CenterParent;
            tagetForm.FormClosed += new FormClosedEventHandler(Exit_click);
            tagetForm.Show();
        }

        private void btn3_click(object o, EventArgs a)
        {
            parentForm.Visible = false;

            tagetForm = new LoginForm();
            tagetForm.StartPosition = FormStartPosition.CenterParent;
            tagetForm.FormClosed += new FormClosedEventHandler(Exit_click);
            tagetForm.Show();
        }

        private void Exit_click(object sender, FormClosedEventArgs e)
        {
            parentForm.Close();
        }
    }
}
