using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObkLibrary;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace OBK.Views
{
    class ChoiceView
    {
        private Form parentForm;
        private Draw draw;
        private Hashtable hashtable;
        private Button countbtn1, countbtn2, hotbtn, icebtn, shotbtn1, shotbtn2, yesbtn, nobtn;
        private PictureBox Picture;
        private Label lb_name, lb_price, lb_count1, lb_count2, lb_size, lb_shot1, lb_shot2, lb_cream, lb_allprice, lb_select, lb_must;
        private ComboBox cb_size, cb_cream;
        private string mName = "";
        private WebAPI api;
        private int count = 0, price = 0, height = 180;
        private bool hotice = true;

        public ChoiceView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        public ChoiceView(Form parentForm, string mName)
        {
            this.parentForm = parentForm;
            this.mName = mName;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            Hashtable ht = new Hashtable();
            ht.Add("mName", mName);
            if (!ChoicePrint(Program.serverUrl+"menu/choice", ht))
            {
                MessageBox.Show("메뉴 상세내용 읽기 실패");
            }

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(30, 30));
            hashtable.Add("point", new Point(280, 100));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "countbtn1");
            hashtable.Add("text", "-");
            hashtable.Add("click", (EventHandler)countbtn1_click);
            countbtn1 = draw.getButton(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(30, 30));
            hashtable.Add("point", new Point(355, 100));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "countbtn2");
            hashtable.Add("text", "+");
            hashtable.Add("click", (EventHandler)countbtn2_click);
            countbtn2 = draw.getButton(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("text", "수량 : ");
            hashtable.Add("point", new Point(180, 105));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Regular));
            hashtable.Add("name", "lb_count1");
            lb_count1 = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("width", 35);
            hashtable.Add("text", "1");
            hashtable.Add("point", new Point(315, 105));
            hashtable.Add("font", new Font("굴림", 15, FontStyle.Bold));
            hashtable.Add("name", "lb_count2");
            lb_count2 = draw.getLabel1(hashtable, parentForm);
            lb_count2.TextAlign = ContentAlignment.MiddleCenter;
            //====================카운트========================
            count = Convert.ToInt32(lb_count2.Text);
            price = Convert.ToInt32(lb_price.Text.Substring(lb_price.Text.IndexOf(" ") + 1));

            hashtable = new Hashtable();
            hashtable.Add("text", "전체금액 : " + (count * price) + "원");
            height += 60;
            hashtable.Add("point", new Point(10, height + 10));
            hashtable.Add("font", new Font("맑은고딕", 16, FontStyle.Bold));
            hashtable.Add("name", "lb_allprice");
            lb_allprice = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(90, 50));
            hashtable.Add("point", new Point(330, height));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "yesbtn");
            hashtable.Add("text", "확인");
            hashtable.Add("click", (EventHandler)yesbtn_click);
            yesbtn = draw.getButton(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(90, 50));
            hashtable.Add("point", new Point(220, height));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "nobtn");
            hashtable.Add("text", "취소");
            hashtable.Add("click", (EventHandler)nobtn_click);
            nobtn = draw.getButton(hashtable, parentForm);
            parentForm.Height = height + 100;
        }

        private bool ChoicePrint(string url, Hashtable ht1)
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();

                foreach (DictionaryEntry data in ht1)
                {
                    nameValue.Add(data.Key.ToString(), data.Value.ToString());
                }
                byte[] result = wc.UploadValues(url, "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(resultStr);

                draw = new Draw();
                for (int i = 0; i < list.Count; i++)
                {
                    //0 mNo 1 mName 2 mPrice 3 mImage 4 DegreeYn 5 SizeYn 6 ShotYn 7 CreamYn
                    JArray jArray = (JArray)list[i];

                    hashtable = new Hashtable();
                    hashtable.Add("size", new Size(150, 150));
                    hashtable.Add("point", new Point(10, 20));
                    hashtable.Add("color", Color.White);
                    //=================이미지 넣어줘야하는부분===============
                    hashtable.Add("image", Image.FromStream(wc.OpenRead(jArray[3].ToString())));
                    //=====================================================
                    Picture = draw.getPictureBox(hashtable, parentForm);
                    Picture.BackgroundImageLayout = ImageLayout.Stretch;

                    hashtable = new Hashtable();
                    hashtable.Add("text", jArray[1].ToString());
                    hashtable.Add("point", new Point(180, 20));
                    hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
                    hashtable.Add("name", "lb_name");
                    lb_name = draw.getLabel(hashtable, parentForm);

                    hashtable = new Hashtable();
                    hashtable.Add("text", "\\ " + jArray[2].ToString());
                    hashtable.Add("point", new Point(180, 50));
                    hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Regular));
                    hashtable.Add("name", "lb_price");
                    lb_price = draw.getLabel(hashtable, parentForm);
                    if (jArray[4].ToString() != "0" || jArray[5].ToString() != "0")
                    {
                        hashtable = new Hashtable();
                        hashtable.Add("text", "---------------------- 필수사항 ----------------------");
                        hashtable.Add("point", new Point(5, height));
                        hashtable.Add("font", new Font("바탕", 10, FontStyle.Regular));
                        hashtable.Add("name", "lb_must");
                        lb_must = draw.getLabel(hashtable, parentForm);
                    }
                    if (jArray[4].ToString() == "1")
                    {
                        height += 30;
                        hashtable = new Hashtable();
                        hashtable.Add("size", new Size(200, 40));
                        hashtable.Add("point", new Point(10, height));
                        hashtable.Add("color", Color.IndianRed);
                        hashtable.Add("name", "countbtn1");
                        hashtable.Add("text", "HOT");
                        hashtable.Add("click", (EventHandler)hotbtn_click);
                        hotbtn = draw.getButton(hashtable, parentForm);
                        hotbtn.Font = new Font("맑은고딕", 12, FontStyle.Bold);
                        hotbtn.ForeColor = Color.White;

                        hashtable = new Hashtable();
                        hashtable.Add("size", new Size(200, 40));
                        hashtable.Add("point", new Point(220, height));
                        hashtable.Add("color", Color.White);
                        hashtable.Add("name", "countbtn2");
                        hashtable.Add("text", "ICED");
                        hashtable.Add("click", (EventHandler)icebtn_click);
                        icebtn = draw.getButton(hashtable, parentForm);
                        icebtn.Font = new Font("맑은고딕", 12, FontStyle.Bold);
                        icebtn.ForeColor = Color.LightSkyBlue;
                    }
                    if (jArray[5].ToString() == "1")
                    {
                        height += 60;
                        hashtable = new Hashtable();
                        hashtable.Add("text", "사이즈 : ");
                        hashtable.Add("point", new Point(145, height));
                        hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
                        hashtable.Add("name", "lb_size");
                        lb_size = draw.getLabel(hashtable, parentForm);

                        hashtable = new Hashtable();
                        hashtable.Add("width", 200);
                        hashtable.Add("point", new Point(220, height));
                        hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
                        hashtable.Add("name", "cb_size");
                        cb_size = draw.getComboBox(hashtable, parentForm);
                        cb_size.Items.AddRange(new object[] { "Regular", "Large(+500원)" });
                        cb_size.SelectedIndex = 0;
                        cb_size.SelectedIndexChanged += Cb_size_SelectedIndexChanged;
                    }
                    if (jArray[6].ToString() != "0" || jArray[7].ToString() != "0")
                    {
                        height += 50;
                        hashtable = new Hashtable();
                        hashtable.Add("text", "---------------------- 선택사항 ----------------------");
                        hashtable.Add("point", new Point(5, height));
                        hashtable.Add("font", new Font("바탕", 10, FontStyle.Regular));
                        hashtable.Add("name", "lb_select");
                        lb_select = draw.getLabel(hashtable, parentForm);
                        height += 30;
                    }
                    if (jArray[6].ToString() == "1")
                    {
                        hashtable = new Hashtable();
                        hashtable.Add("size", new Size(30, 30));
                        hashtable.Add("point", new Point(110, height));
                        hashtable.Add("color", Color.LightGray);
                        hashtable.Add("name", "shotbtn1");
                        hashtable.Add("text", "-");
                        hashtable.Add("click", (EventHandler)shotbtn1_click);
                        shotbtn1 = draw.getButton(hashtable, parentForm);

                        hashtable = new Hashtable();
                        hashtable.Add("size", new Size(30, 30));
                        hashtable.Add("point", new Point(185, height));
                        hashtable.Add("color", Color.LightGray);
                        hashtable.Add("name", "shotbtn2");
                        hashtable.Add("text", "+");
                        hashtable.Add("click", (EventHandler)shotbtn2_click);
                        shotbtn2 = draw.getButton(hashtable, parentForm);

                        hashtable = new Hashtable();
                        hashtable.Add("text", " 샷추가 : \n(500원)");
                        hashtable.Add("point", new Point(10, height + 3));
                        hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Regular));
                        hashtable.Add("name", "lb_shot1");
                        lb_shot1 = draw.getLabel(hashtable, parentForm);
                        lb_shot1.TextAlign = ContentAlignment.MiddleCenter;

                        hashtable = new Hashtable();
                        hashtable.Add("width", 35);
                        hashtable.Add("text", "0");
                        hashtable.Add("point", new Point(145, height + 3));
                        hashtable.Add("font", new Font("굴림", 15, FontStyle.Bold));
                        hashtable.Add("name", "lb_shot2");
                        lb_shot2 = draw.getLabel1(hashtable, parentForm);
                        lb_shot2.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    if (jArray[7].ToString() == "1")
                    {
                        hashtable = new Hashtable();
                        hashtable.Add("text", "휘핑 : ");
                        hashtable.Add("point", new Point(240, height + 3));
                        hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
                        hashtable.Add("name", "lb_cream");
                        lb_cream = draw.getLabel(hashtable, parentForm);

                        hashtable = new Hashtable();
                        hashtable.Add("width", 120);
                        hashtable.Add("point", new Point(300, height));
                        hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
                        hashtable.Add("name", "cb_cream");
                        cb_cream = draw.getComboBox(hashtable, parentForm);
                        cb_cream.Items.AddRange(new object[] { "없음", "보통", "많이" });
                        cb_cream.SelectedIndex = 1;
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Price_Calculate()
        {
            if (cb_size.SelectedItem.ToString() == "Large(+500원)")
            {
                count = Convert.ToInt32(lb_count2.Text);
                price = Convert.ToInt32(lb_price.Text.Substring(lb_price.Text.IndexOf(" ") + 1));
                price += 500;
                if (lb_shot2 != null)
                {
                    int shot = Convert.ToInt32(lb_shot2.Text);
                    price += 500 * shot;
                }
                int allprice = (count * price);
                lb_allprice.Text = "전체금액 : " + allprice + "원";
            }
            else if(cb_size.SelectedItem.ToString() == "Regular")
            {
                count = Convert.ToInt32(lb_count2.Text);
                price = Convert.ToInt32(lb_price.Text.Substring(lb_price.Text.IndexOf(" ") + 1));
                if(lb_shot2 != null)
                {
                    int shot = Convert.ToInt32(lb_shot2.Text);
                    price += 500 * shot;
                }
                int allprice = (count * price);
                lb_allprice.Text = "전체금액 : " + allprice + "원";
            }
        }

        private void Cb_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            Price_Calculate();
        }

        private void yesbtn_click(object sender, EventArgs e)
        {
            string oSize = "";
            string oShot = "-1";
            string oCream = "";
            string hi = "";

            if (hotbtn != null && icebtn != null)
            {
                if (hotice) hi = "Hot";
                else hi = "Iced";
            }
            if (cb_size != null)
            {
                if(cb_size.SelectedItem.ToString()== "Large(+500원)")
                {
                    oSize = cb_size.SelectedItem.ToString().Substring(0,5);
                    //MessageBox.Show(oSize);
                }
                else
                {
                    oSize = cb_size.SelectedItem.ToString();
                }
            }
            if (cb_cream != null)
                oCream = cb_cream.SelectedItem.ToString();
            if (lb_shot2 != null)
                oShot = lb_shot2.Text;

            //MessageBox.Show(mName + ">>" + lb_count2.Text + "개\n" + hi + "\n" + oSize + "\n" + oCream + "\n" + oShot + "샷");
            api = new WebAPI();
            Program.maxoNum = api.MaxoNum(Program.serverUrl + "orderlist/selectMaxoNum");
            Hashtable ht = new Hashtable();
            ht.Add("mName", mName);
            ht.Add("oNum", Program.maxoNum);
            ht.Add("oCount", lb_count2.Text);
            ht.Add("oDegree", hi);
            ht.Add("oSize", oSize);
            ht.Add("oShot", oShot);
            ht.Add("oCream", oCream);

            api.Post(Program.serverUrl+"orderlist/insert", ht);
            parentForm.Close();
        }

        private void nobtn_click(object sender, EventArgs e)
        {
            parentForm.Close();
        }

        private void countbtn1_click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lb_count2.Text) > 1)
            {
                int minus = Convert.ToInt32(lb_count2.Text) - 1;
                lb_count2.Text = minus.ToString();

                Price_Calculate();
            }
        }

        private void countbtn2_click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lb_count2.Text) < 10)
            {
                int plus = Convert.ToInt32(lb_count2.Text) + 1;
                lb_count2.Text = plus.ToString();

                Price_Calculate();
            }
        }

        private void hotbtn_click(object sender, EventArgs e)
        {
            hotbtn.ForeColor = Color.White;
            hotbtn.BackColor = Color.IndianRed;
            icebtn.ForeColor = Color.LightSkyBlue;
            icebtn.BackColor = Color.White;
            hotice = true;
        }

        private void icebtn_click(object sender, EventArgs e)
        {
            hotbtn.ForeColor = Color.IndianRed;
            hotbtn.BackColor = Color.White;
            icebtn.ForeColor = Color.White;
            icebtn.BackColor = Color.LightSkyBlue;
            hotice = false;
        }

        private void shotbtn1_click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lb_shot2.Text) > 0)
            {
                int minus = Convert.ToInt32(lb_shot2.Text) - 1;
                lb_shot2.Text = minus.ToString();

                Price_Calculate();
            }
        }

        private void shotbtn2_click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lb_shot2.Text) < 10)
            {
                int plus = Convert.ToInt32(lb_shot2.Text) + 1;
                lb_shot2.Text = plus.ToString();

                Price_Calculate();
            }
        }
    }
}
