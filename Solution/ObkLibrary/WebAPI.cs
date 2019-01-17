using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.ListView;

namespace ObkLibrary
{
    public class WebAPI
    {
        private Draw draw;

        public int MaxoNum(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();
                int max = 0;
                max = Convert.ToInt32(result);

                return max;
            }
            catch
            {
                return -1;
            }
        }

        public string getPasswd(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();
                return result;
            }
            catch
            {
                return "";
            }
        }

        public bool ListView(string url, ListView listView)
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();
                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(result);
                listView.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    JArray jArray = (JArray)list[i];
                    string[] arr = new string[jArray.Count];
                    for (int j = 0; j < jArray.Count; j++)
                    {
                        arr[j] = jArray[j].ToString();
                    }
                    listView.Items.Add(new ListViewItem(arr));
                    listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                    listView.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
                }

                ListViewItemCollection col = listView.Items;    // listview subitems 글꼴 바꾸기
                for (int j = 0; j < col.Count; j++)
                {
                    for (int k = 0; k < col[j].SubItems.Count; k++)
                    {
                        col[j].SubItems[k].Font = new Font("맑은 고딕", 12, FontStyle.Regular);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SelectCategory(string url, ComboBox cb)
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();
                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(result);

                string[] arr = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    JArray j = (JArray)list[i];
                    arr[i] = j.Value<string>(1).ToString();
                }
                cb.Items.AddRange(arr);
                cb.Font = new Font("맑은 고딕", 13, FontStyle.Bold);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Post(string url, Hashtable ht)
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();

                foreach (DictionaryEntry data in ht)
                {
                    nameValue.Add(data.Key.ToString(), data.Value.ToString());
                }

                byte[] result = wc.UploadValues(url, "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                if ("1" == resultStr)
                {
                    //MessageBox.Show("DB 성공");
                }
                else
                {
                    MessageBox.Show("DB 실패");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PostListview(string url, Hashtable hashtable, ListView listView)
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();

                foreach (DictionaryEntry data in hashtable)
                {
                    nameValue.Add(data.Key.ToString(), data.Value.ToString());
                }

                byte[] result = wc.UploadValues(url, "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(resultStr);
                listView.Items.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    JArray jArray = (JArray)list[i];
                    string[] arr = new string[jArray.Count];
                    for (int j = 0; j < jArray.Count; j++)
                    {
                        arr[j] = jArray[j].ToString();

                    }
                    listView.Items.Add(new ListViewItem(arr));
                    listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                }

                ListViewItemCollection col = listView.Items;
                for (int j = 0; j < col.Count; j++)
                {
                    for (int k = 0; k < col[j].SubItems.Count; k++)
                    {
                        col[j].SubItems[k].Font = new Font("맑은 고딕", 12, FontStyle.Regular);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PostChart(string url, Hashtable hashtable, Chart chart)
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();

                foreach (DictionaryEntry data in hashtable)
                {
                    nameValue.Add(data.Key.ToString(), data.Value.ToString());
                }

                byte[] result = wc.UploadValues(url, "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(resultStr);
                chart.Series[0].Points.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    JArray jArray = (JArray)list[i];
                    chart.Series[0].Points.AddXY(jArray[0].ToString(), jArray[1].ToString());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PostChartPrice(string url, Hashtable hashtable, Chart chart)
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();

                foreach (DictionaryEntry data in hashtable)
                {
                    nameValue.Add(data.Key.ToString(), data.Value.ToString());
                }

                byte[] result = wc.UploadValues(url, "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(resultStr);
                chart.Series[0].Points.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    JArray jArray = (JArray)list[i];
                    chart.Series[0].Points.AddXY(jArray[0].ToString(), jArray[2].ToString());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PostChartCount(string url, Hashtable hashtable, Chart chart)
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();

                foreach (DictionaryEntry data in hashtable)
                {
                    nameValue.Add(data.Key.ToString(), data.Value.ToString());
                }

                byte[] result = wc.UploadValues(url, "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(resultStr);
                chart.Series[0].Points.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    JArray jArray = (JArray)list[i];
                    chart.Series[0].Points.AddXY(jArray[0].ToString(), jArray[1].ToString());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool MenuEdeitSelect(string url, Hashtable hashtable, TextBox txtMenu, TextBox txtPrice, TextBox txtImg, CheckBox cboxHot, CheckBox cboxSize, CheckBox cboxShot, CheckBox cboxWhip)  // 선택한 메뉴 select 해서 값 넣어주기
        {
            try
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();

                foreach (DictionaryEntry data in hashtable)
                {
                    nameValue.Add(data.Key.ToString(), data.Value.ToString());
                }

                byte[] result = wc.UploadValues(url, "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(resultStr);
                for (int i = 0; i < list.Count; i++)
                {
                    JArray jArray = (JArray)list[i];

                    txtMenu.Text = jArray[0].ToString();
                    txtMenu.Font = new Font("맑은 고딕", 12, FontStyle.Regular);
                    txtPrice.Text = jArray[1].ToString();
                    txtPrice.Font = new Font("맑은 고딕", 12, FontStyle.Regular);
                    txtImg.Text = jArray[2].ToString();
                    txtImg.Font = new Font("맑은 고딕", 12, FontStyle.Regular);

                    if (jArray[3].ToString() == "1")
                    {
                        cboxHot.CheckState = CheckState.Checked;
                    }
                    if (jArray[4].ToString() == "1")
                    {
                        cboxSize.CheckState = CheckState.Checked;
                    }
                    if (jArray[5].ToString() == "1")
                    {
                        cboxShot.CheckState = CheckState.Checked;
                    }
                    if (jArray[6].ToString() == "1")
                    {
                        cboxWhip.CheckState = CheckState.Checked;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        public ArrayList CategoryButton(string url, Hashtable hashtable)
        {
            try
            {
                ArrayList resultlist = new ArrayList();
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();
                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(result);
                draw = new Draw();
                //MessageBox.Show("count = "+list.Count);
                for (int i = 0; i < list.Count; i++)
                {
                    Hashtable ht = (Hashtable)hashtable.Clone();

                    JArray jArray = (JArray)list[i];
                    ht.Add("point", new Point(30 + (i * 190), 10));
                    ht.Add("name", "btn" + jArray[0].ToString());
                    ht.Add("text", jArray[1].ToString());
                    resultlist.Add(ht);
                }
                return resultlist;
            }
            catch
            {
                return new ArrayList();
            }
        }

        public bool MenuPrint(string url, Hashtable ht1, Hashtable ht2, Hashtable ht3, Form form)
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
                    //0 mNo 1 mName 2 mPrice 3 mImage
                    JArray jArray = (JArray)list[i];

                    Hashtable hashtable = (Hashtable)ht2.Clone();
                    hashtable.Add("point", new Point(20 + 190 * (i % 4), 20 + 190 * (i / 4)));
                    hashtable.Add("name", "_" + jArray[1].ToString());
                    hashtable.Add("text", "\n\n\n\n\n\n\n\n" + jArray[1].ToString() + "\n\\" + jArray[2].ToString());
                    Button btn = draw.getButton1(hashtable, form);
                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    //메뉴 이미지 픽쳐박스
                    hashtable = (Hashtable)ht3.Clone();
                    hashtable.Add("point", new Point(5, 5));
                    hashtable.Add("size", new Size(160, 125));
                    hashtable.Add("color", Color.White);
                    hashtable.Add("name", "image_" + jArray[1].ToString());
                    hashtable.Add("text", "");
                    Button imgbtn = draw.getButton(hashtable, btn);

                    nameValue = new NameValueCollection();

                    nameValue.Add("mName", btn.Name.Substring(btn.Name.IndexOf("_") + 1));

                    byte[] result2 = wc.UploadValues(Program.serverUrl + "menu/image", "POST", nameValue);
                    string resultStr2 = Encoding.UTF8.GetString(result2);
                    //MessageBox.Show(resultStr2);
                    imgbtn.BackgroundImage = Image.FromStream(wc.OpenRead(resultStr2));
                    imgbtn.BackgroundImageLayout = ImageLayout.Stretch;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
