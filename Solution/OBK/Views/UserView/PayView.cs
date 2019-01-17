using OBK.Forms;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views
{
    class PayView
    {
        private WebAPI api;
        private Hashtable hashtable;
        private Form parentForm;
        private Draw draw;
        private ListView listOrderList;
        private Label lblMessage, lb_total;
        private Button btnMoney, btnCard, btnCancel;

        public PayView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            //hashtable = new Hashtable();
            //hashtable.Add("color", Color.White);
            //hashtable.Add("size", new Size(800, 300));
            //hashtable.Add("point", new Point(0, 20));
            //hashtable.Add("name", "주문리스트");
            //listOrderList = draw.getListView(hashtable, parentForm);
            //listOrderList.Columns.Add("", 0);
            //listOrderList.Columns.Add("메뉴이름", 170, HorizontalAlignment.Center);
            //listOrderList.Columns.Add("샷추가", 110, HorizontalAlignment.Center);
            //listOrderList.Columns.Add("휘핑", 136, HorizontalAlignment.Center);
            //listOrderList.Columns.Add("단가", 136, HorizontalAlignment.Center);
            //listOrderList.Columns.Add("수량", 90, HorizontalAlignment.Center);
            //listOrderList.Columns.Add("금액", 136, HorizontalAlignment.Center);
            //listOrderList.ColumnWidthChanging += ListOrderList_ColumnWidthChanging;
            //api = new WebAPI();
            //api.ListView(Program.serverUrl + "orderlist/select", listOrderList);

            hashtable = new Hashtable();
            hashtable.Add("text", "결제 수단을 선택해 주세요.");
            hashtable.Add("point", new Point(70, 30));
            hashtable.Add("font", new Font("맑은 고딕", 20, FontStyle.Bold));
            hashtable.Add("name", "lblMessage");
            lblMessage = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(190, 100));
            hashtable.Add("point", new Point(30, 100));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("name", "btnCard");
            hashtable.Add("text", "카드결제");
            hashtable.Add("click", (EventHandler)btnCard_click);
            btnCard = draw.getButton1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(190, 100));
            hashtable.Add("point", new Point(260, 100));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("name", "btnMoney");
            hashtable.Add("text", "현금 결제");
            hashtable.Add("click", (EventHandler)btnMoney_click);
            btnMoney = draw.getButton1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(100, 50));
            hashtable.Add("point", new Point(350, 290));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btnCancel");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("text", "취소");
            hashtable.Add("click", (EventHandler)btnCancel_click);
            btnCancel = draw.getButton1(hashtable, parentForm);

            //hashtable = new Hashtable();
            //hashtable.Add("text", "총 가격 :          ");
            //hashtable.Add("width", 610);
            //hashtable.Add("point", new Point(130, 320));
            //hashtable.Add("name", "totalprice");
            //hashtable.Add("font", new Font("맑은 고딕", 18, FontStyle.Regular));
            //lb_total = draw.getLabel1(hashtable, parentForm);
            //ListShotCalc();//주문목록 각각에 대한 샷추가를 가격에 샷당 500원 추가해주는 함수호출
            //lb_total.Text += ListTotalPrice();//총 가격 계산
            //lb_total.Height = 35;
        }

        private void ListShotCalc()
        {
            for (int i = 0; i < listOrderList.Items.Count; i++)
            {
                int price = Convert.ToInt32(listOrderList.Items[i].SubItems[5].Text);
                int shot = Convert.ToInt32(listOrderList.Items[i].SubItems[2].Text);
                price += shot * 500;
                listOrderList.Items[i].SubItems[5].Text = price.ToString();
            }
        }

        private string ListTotalPrice()
        {
            int total = 0;
            for (int i = 0; i < listOrderList.Items.Count; i++)
            {
                int price = Convert.ToInt32(listOrderList.Items[i].SubItems[5].Text);
                int count = Convert.ToInt32(listOrderList.Items[i].SubItems[4].Text);
                total += price * count;
            }
            return total.ToString();
        }

        private void btnCard_click(object o, EventArgs a)
        {
            api = new WebAPI();
            hashtable.Add("oNum", Program.maxoNum);
            api.Post(Program.serverUrl + "orderlist/orderYn", hashtable);

            BillForm billForm = new BillForm();
            billForm.StartPosition = FormStartPosition.Manual;
            billForm.Location = new Point(parentForm.Location.X, parentForm.Location.Y + (parentForm.Height / 2) - (billForm.Height / 2));
            billForm.FormClosed += new FormClosedEventHandler(exit_click);
            billForm.ShowDialog();
        }

        private void btnMoney_click(object o, EventArgs a)
        {
            api = new WebAPI();
            hashtable.Add("oNum", Program.maxoNum);
            api.Post(Program.serverUrl + "orderlist/orderYn", hashtable);

            BillForm billForm = new BillForm();
            //Point p = new Point((parentForm.Width - billForm.Width) / 2, (parentForm.Height - billForm.Height) / 2);
            //billForm.StartPosition = FormStartPosition.CenterParent;
            billForm.StartPosition = FormStartPosition.Manual;
            billForm.Location = new Point(parentForm.Location.X , parentForm.Location.Y + (parentForm.Height / 2) - (billForm.Height / 2));
            billForm.FormClosed += new FormClosedEventHandler(exit_click);
            billForm.ShowDialog();
        }

        private void btnCancel_click(object o, EventArgs a) // 취소 클릭 이벤트
        {
            api = new WebAPI();
            hashtable = new Hashtable();
            hashtable.Add("oNum", Program.maxoNum);
            api.Post(Program.serverUrl + "orderlist/deleteOrderAll", hashtable);

            parentForm.Close();
        }

        private void exit_click(object sender, FormClosedEventArgs e)
        {
            parentForm.Close();
        }

        private void ListOrderList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)   // listView 칼럼사이즈 고정
        {
            e.NewWidth = listOrderList.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
