using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.StaffView
{
    class OrderListView
    {
        private Draw draw;
        private Form parentForm;
        private Hashtable hashtable;
        private ListView list;
        private Button btnOk;
        private WebAPI api;
        private Timer timer = new Timer();
        public OrderListView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
            timer.Interval = 5000;
            timer.Tick += new EventHandler(Orderlist_Select);
            timer.Start();
            parentForm.FormClosed += ParentForm_FormClosed;
        }

        private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
        }

        private void Orderlist_Select(object o,EventArgs e)
        {
            api = new WebAPI();
            api.ListView(Program.serverUrl + "orderlist/selectstaff", list);
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(665, 250));
            hashtable.Add("point", new Point(10, 10));
            hashtable.Add("name", "주문리스트");
            list = draw.getListView(hashtable, parentForm);
            list.Columns.Add("", 25, HorizontalAlignment.Center);
            list.Columns.Add("주문번호", 110, HorizontalAlignment.Center);
            list.Columns.Add("메뉴", 200, HorizontalAlignment.Center);
            list.Columns.Add("샷추가", 108, HorizontalAlignment.Center);
            list.Columns.Add("휘핑", 90, HorizontalAlignment.Center);
            list.Columns.Add("수량", 100, HorizontalAlignment.Center);
            list.Columns.Add("주문번호기본키", 0, HorizontalAlignment.Center);
            list.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
            list.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            list.ColumnWidthChanging += List_ColumnWidthChanging;
            list.ItemCheck += List_ItemCheck;
            //_____________________________________________________________________
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(160, 60));
            hashtable.Add("point", new Point(510, 270));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "ok");
            hashtable.Add("text", "확인");
            hashtable.Add("font", new Font("맑은 고딕", 12, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btn_click);
            btnOk = draw.getButton1(hashtable, parentForm);
            api = new WebAPI();
            api.ListView(Program.serverUrl + "orderlist/selectstaff", list);
        }

        private void List_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            timer.Stop();
        }

        private void List_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = list.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void btn_click(object o, EventArgs a)
        {
            timer.Start();
            api = new WebAPI();
            bool one = true;

            foreach (ListViewItem listitem in list.Items)
            {
                if (list.Items.Count > 0)
                {
                    for (int i = list.Items.Count - 1; i >= 0; i--)
                    {
                        if (list.Items[i].Checked == true)
                        {
                            Hashtable ht = new Hashtable();
                            ht.Add("oNo", list.Items[i].SubItems[6].Text);
                            api.Post(Program.serverUrl + "orderlist/comYn", ht);
                            list.Items[i].Remove();
                            if (one)
                            {
                                MessageBox.Show("주문 확인");
                                one = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
