using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views
{
    class BillView
    {
        PictureBox pictureBox;
        private Draw draw;
        private Form parentForm;
        private Label label1, label2, lblnum, label3, label4, label5, label6;
        private ListView list;
        private Button button;
        private Hashtable hashtable;

        public BillView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            pictureBox = new PictureBox();
            pictureBox.Image = (Bitmap)OBK.Properties.Resources.ResourceManager.GetObject("logo2");
            pictureBox.Location = new Point(140, 5);
            pictureBox.Size = new Size(200, 93);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            parentForm.Controls.Add(pictureBox);

            hashtable = new Hashtable();
            hashtable.Add("text", "----------------------------------------------------------------------");
            hashtable.Add("point", new Point(18, 100));
            hashtable.Add("name", "선");
            label1 = draw.getLabel(hashtable, parentForm);

            WebAPI api = new WebAPI();
            Program.maxoNum = api.MaxoNum(Program.serverUrl + "orderlist/selectMaxoNum");
            Program.maxoNum--;
            hashtable = new Hashtable();
            hashtable.Add("text", string.Format("주문번호 : {0}",Program.maxoNum.ToString()));
            hashtable.Add("point", new Point(180, 117));
            hashtable.Add("font", new Font("고딕", 18, FontStyle.Bold));
            hashtable.Add("name", "oNumTxt");
            label2 = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("text", "----------------------------------------------------------------------");
            hashtable.Add("point", new Point(18, 150));
            hashtable.Add("name", "선");
            label3 = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(430, 110));
            hashtable.Add("point", new Point(15, 170));
            hashtable.Add("name", "주문리스트");
            list = draw.getListView(hashtable, parentForm);
            list.Columns.Add("", 0);
            list.Columns.Add("메뉴", 185, HorizontalAlignment.Center);
            list.Columns.Add("단가", 80, HorizontalAlignment.Center);
            list.Columns.Add("수량", 60, HorizontalAlignment.Center);
            list.Columns.Add("금액", 89, HorizontalAlignment.Center);
            list.ColumnWidthChanging += List_ColumnWidthChanging;
            Hashtable ht = new Hashtable();
            ht.Add("oNum",Program.maxoNum);
            api.PostListview(Program.serverUrl + "orderlist/selectBill",ht, list);

            hashtable = new Hashtable();
            hashtable.Add("text", "----------------------------------------------------------------------");
            hashtable.Add("point", new Point(18, 300));
            hashtable.Add("name", "선");
            label4 = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("text", "합계 :  ");
            hashtable.Add("point", new Point(300, 320));
            hashtable.Add("font", new Font("굴림", 12, FontStyle.Bold));
            hashtable.Add("name", "sum");
            label5 = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(100, 50));
            hashtable.Add("point", new Point(350, 390));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "ok");
            hashtable.Add("text", "확인");
            hashtable.Add("click", (EventHandler)btn_click);
            button = draw.getButton(hashtable, parentForm);

            BillResize();//주문목록의 주문갯수에 따라서 영수증의 크기를 맞춰주는 함수호출
            label5.Text += TotalPrice();
        }

        private string TotalPrice()
        {
            int total = 0;
            for (int i = 0; i < list.Items.Count; i++)
            {
                int price = Convert.ToInt32(list.Items[i].SubItems[4].Text);
                total += price;
            }
            return total.ToString();
        }

        private void BillResize()
        {
            if (list.Items.Count > 2)
            {
                int additem = list.Items.Count - 2;
                parentForm.Size = new Size(500, 500 + (30 * additem));
                list.Size = new Size(430, 110 + (30 * additem));
                label4.Location = new Point(18, 300 + (30 * additem));
                label5.Location = new Point(25, 320 + (30 * additem));
                button.Location = new Point(350, 390 + (30 * additem));
            }
        }

        private void List_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = list.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void btn_click(object o, EventArgs a)
        {
            parentForm.Close();
            MessageBox.Show("주문이 완료되었습니다.");
        }


    }
}
