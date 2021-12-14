using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.Data.OleDb kütüphanesinin eklenmesi
using System.Data.OleDb;

namespace personeltakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //provider:visual studio ile access veri tabanı arasında kullanılacak araçtır.Veritabanını visual studioya ekledik.StartupPath denilince bindeki debug klasörünü simgeliyoruz.

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.OleDb.12.0;Data Source= " + Application.StartupPath + "\\personel.accdb");
        //Formlar arası veri aktarımında kullanılacak değişkenler
        public static string tcno, adi, soyadi, yetki;

        //yerel değişkenler yani sadece bu formda geçerli olacak değişkenler

        private int hak = 3; public bool durum = false; // böyle bir kullanıcı var mı diye bakarken.varsa (yoksa?) bool: false olmalı.
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak !=0)
            {
                   
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader(); //selectsorgunun sonuçlarını kayıtokuma da sakla.

                while (kayitokuma.Read()) //true değeri dönerse.Eğer herhangi bir bilgi varsa true değeri döner.
                {
                    if (radioButton1.Checked == true)
                    {
                        //yönetici girişi ise
                        if(kayitokuma["kullaniciadi"].ToString()==textBox1.Text && kayitokuma["parola"].ToString()==textBox2.Text && kayitokuma["yetki"].ToString()=="Yönetici")
                        {
                            //kayitokumaya access alanları yukarıda eşitlenmişti.Eğer txtlere girilen değer accessdeki değerlerle aynıysa 
                        durum = true; //doğru değerler girildiği için
                        tcno = kayitokuma.GetValue(0).ToString(); //get:1.değeri elde et. Kayıt okumanın alanlarına ekledik doğru durumları.kayıtokumanın başarılı olan kaydının 0.alanını tcye eşitle vs
                        adi = kayitokuma.GetValue(1).ToString();
                        soyadi = kayitokuma.GetValue(2).ToString();
                        yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide(); //1.form başarılı olduğu için gizliyoruz. 
                            Form2 frm2 = new Form2(); //frm2 adlı form2 nesnesi oluşturduk
                            frm2.Show(); // form2yi aktif ettik
                            break; //while döngüsünden çıkmasını sağladık.

                        }
                    }
                    if (radioButton2.Checked == true)
                    {
                        //kullanıcı girişi ise
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            //kayitokumaya access alanları yukarıda eşitlenmişti.Eğer txtlere girilen değer accessdeki değerlerle aynıysa 
                            durum = true; //doğru değerler girildiği için
                            tcno = kayitokuma.GetValue(0).ToString(); //get:1.değeri elde et. Kayıt okumanın alanlarına ekledik doğru durumları.kayıtokumanın başarılı olan kaydının 0.alanını tcye eşitle vs
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide(); //1.form başarılı olduğu için gizliyoruz. 
                            Form3 frm3 = new Form3(); //frm2 adlı form2 nesnesi oluşturduk
                            frm3.Show(); // form2yi aktif ettik
                            break; //while döngüsünden çıkmasını sağladık.

                        }
                        
                    }

                }
                //hak 0a eşit değilse ifinden devam ediyoruz


                if (durum ==false)
                {
                    //hala doğru giriş sağlanmadıysa
                    hak--;
                    //label5.Text = Convert.ToString(hak);
                    baglantim.Close();
                   }

                label5.Text = Convert.ToString(hak);
                if (hak == 0)
                {
                    //hak kalmadıysa giriş butonunun erişilebilirliğini false yap.

                    button1.Enabled = false;
                    MessageBox.Show("Giriş hakkı kalmadı", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();

                }
            }

            //baglantim.Close();
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        } 

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Text = "Kullanıcı Girişi"; //Formun başlangıç kısmında yazılacak yazı.
            this.AcceptButton = button1;  this.CancelButton = button2;   //enter tuşuna basıldığında hangi butona basılmış gibi işlem yapılsın.2)esc tuşu
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterParent;  //ekranın merkezinde gelsin.
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow; //tam ekran yapma tuşlarını vs. pasif yaptık.
            }
}

    }

