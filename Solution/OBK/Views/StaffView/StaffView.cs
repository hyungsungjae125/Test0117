using OBK.Forms.StaffForm;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.StaffView
{
    class StaffView
    {
        private Hashtable hashtable;
        private Draw draw;
        private Form parentForm, tagetForm;
        private Button btn1, btn2, btn3;
        private Panel head, contents;

        public StaffView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("type", "");
            hashtable.Add("size", new Size(684, 100));
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "head");
            head = draw.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(684, 341));
            hashtable.Add("point", new Point(0, 100));
            hashtable.Add("color", Color.Black);
            hashtable.Add("name", "menus");
            contents = draw.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(200, 90));
            hashtable.Add("point", new Point(30, 11));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn1");
            hashtable.Add("text", "주문목록");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)OrderList_Click);
            btn1 = draw.getButton1(hashtable, head);
            btn1.BackColor = Color.FromArgb(46,204,113);
            

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(200, 90));
            hashtable.Add("point", new Point(240, 11));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn2");
            hashtable.Add("text", "품절추가");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)SoldOut_Click);
            btn2 = draw.getButton1(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(200, 90));
            hashtable.Add("point", new Point(450, 11));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn3");
            hashtable.Add("text", "품절삭제(품절목록)");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)SoldOutDelete_Click);
            btn3 = draw.getButton1(hashtable, head);

            if (tagetForm != null) tagetForm.Dispose();
            tagetForm = draw.getMdiForm(parentForm, new OrderListForm(),contents);
            tagetForm.Show();

        }

        private void OrderList_Click(object sender, EventArgs e)
        {
            btn1.BackColor = Color.FromArgb(46, 204, 113);
            btn2.BackColor = Color.LightGray;
            btn3.BackColor = Color.LightGray;

            if (tagetForm != null) tagetForm.Dispose();

            tagetForm = draw.getMdiForm(parentForm,new OrderListForm(), contents);
            tagetForm.Show();
        }

        private void SoldOut_Click(object sender, EventArgs e)
        {
            btn1.BackColor = Color.LightGray;
            btn2.BackColor = Color.FromArgb(46, 204, 113);
            btn3.BackColor = Color.LightGray;

            if (tagetForm != null) tagetForm.Dispose();

            tagetForm = draw.getMdiForm(parentForm, new SoldoutAddForm(), contents);
            tagetForm.Show();
        }

        private void SoldOutDelete_Click(object sender, EventArgs e)
        {
            btn1.BackColor = Color.LightGray;
            btn2.BackColor = Color.LightGray;
            btn3.BackColor = Color.FromArgb(46, 204, 113);

            if (tagetForm != null) tagetForm.Dispose();

            tagetForm = draw.getMdiForm(parentForm, new SoldoutDeleteForm(), contents);
            tagetForm.Show();
        }
    }
}
