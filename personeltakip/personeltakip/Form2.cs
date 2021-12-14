using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
//regex kütüphanesinin tanımlanması.Görevi:Güvenli parola oluşturmayı sağlayan hazır kodları içeren kütüphane.
using System.Text.RegularExpressions;
//giriş çıkış işlemleri için kütüphane tanımlanması.Klasör işlemleri
using System.IO;



namespace personeltakip
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //provider:visual studio ile access veri tabanı arasında kullanılacak araçtır.Veritabanını visual studioya ekledik.StartupPath denilince bindeki debug klasörünü simgeliyoruz.

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.OleDb.12.0;Data Source= " + Application.StartupPath + "\\personel.accdb");

        private void kullanicilari_göster()
        {
            //veritabınındaki kullanıcıların güncel halini datagrieddviewe getirmeye yarayan metot
            try
            {
                //AS kullanımı ile acccess tablosundaki tcno,ad kısımlarının datagriedviewde gözükecek başlıklarını ayarladık.
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO], ad AS[ADI], soyad AS[SOYADI],yetki AS[YETKİ],kullaniciadi AS[KULLANICI ADI] , parola AS[PAROLA] from kullanicilar Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0]; //sorgunun sonucunda gelen ilk tablo
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }

        private void personelleri_göster()
        {
            //veritabınındaki personellerin güncel halini datagrieddviewe getirmeye yarayan metot
            try
            {
                //AS kullanımı ile acccess tablosundaki tcno,ad kısımlarının datagriedviewde gözükecek başlıklarını ayarladık.
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO], ad AS[ADI], soyad AS[SOYADI],cinsiyet AS[CİNSİYET],mezuniyet AS[MEZUNİYETİ] , dogumtarihi AS[DOĞUM TARİHİ],gorevi AS[GÖREVi], gorevyeri AS[GÖREV YERİ], maasi AS[MAAŞI] from personeller Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0]; //sorgunun sonucunda gelen ilk tablo
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /*  kullanicilari_göster();
              personelleri_göster(); */
            pictureBox1.Height = 150; // yüksekliği
            pictureBox1.Width = 150; // genişliği
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; //resim pictureboxa göre görünsün.

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");

                //hangi tc no ile giriş yapıldıysa onun resmi gelsin
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");
            }

            //KULLANICI İŞLEMLERİ 
            this.Text = "Yönetici İşlemleri";
            label11.ForeColor = Color.DarkRed;
            label11.Text = Form1.adi + " " + Form1.soyadi;
            textBox1.MaxLength = 11; //max karakter girilecek sayısı
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC Kİmlik No 11 Karakter Olmalı!"); //bu formun txt1 i 11 karakter olmalı şeklinde kullanıcıya uyarı verir üzerine mouse gelince
            radioButton1.Checked = true;
            textBox2.CharacterCasing = CharacterCasing.Upper;//küçük harfle de yazılsa büyük harfe dönüştürür.
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;//başlangıçta 0 olsun.

            kullanicilari_göster();




            //PERSONEL İŞLEMLERİ

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;
            pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D; //3 boyutlu görünsün.çerçeve
            maskedTextBox1.Mask = "00000000000"; //0 zorunlu rakam girişi demek 11 karakter girmek zorunludur
            maskedTextBox2.Mask = "LL????????????????????";//En az iki karakter girmek zorunda soru işaretlerinden fazla karakter girilmez
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000";
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper(); //girilen harfleri otomatik büyütür
            maskedTextBox3.Text.ToUpper();


            comboBox1.Items.Add("İlköğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");


            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Memur");
            comboBox2.Items.Add("Şoför");
            comboBox2.Items.Add("İşçi");


            comboBox3.Items.Add("ARGE");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Muhasebe");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Paketleme");
            comboBox3.Items.Add("Nakliye");

            //DATETİMEPİCKER
            DateTime zaman = DateTime.Now; //şimdiki zamanı aldık
            int yil = int.Parse(zaman.ToString("yyyy")); //zamanı stringe dönüştürdük.Yılı aldık
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);//en küçük zamanı
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePicker1.Format = DateTimePickerFormat.Short; //kısa tarih görünsün.
            radioButton3.Checked = true;

            personelleri_göster();


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
            {
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 Karakter Olmalı");
            }
            else
            {//hatanın temizlenmesini sağladık
                errorProvider1.Clear();

            }

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            if(textBox6.Text!= textBox5.Text)
            {
                errorProvider1.SetError(textBox6, "Parola tekrarı eşleşmiyor.");
            }
            else {
                errorProvider1.Clear();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // textBox1 üzerinde imleç yanarken:yani txt işlemdeyken demektir.Kullanıcının rakam dışında tuşa basmasını engelliyoruz.

            //e.keychar:klavyeden basılan tuşu almamızı sağlar.

            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
            {
                e.Handled = false; //tuşlara basılmasına izin verdik
            }
            else
            {
                e.Handled = true; //izin vermiyoruz.
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true) {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //txt4e her karakter yazıldığında ya da her karakter silindiğinde
            if (textBox4.Text.Length != 8)
            {
                errorProvider1.SetError(textBox4, "Kullanıcı adı 8 karakter olmalı");
            }
            else {

                errorProvider1.Clear();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true; //tuşları kapattık
            }

        }
        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            String parola_seviyesi = "";
            int kucuk_harf_skoru = 0;
            int buyuk_harf_skoru = 0;
            int rakam_skoru = 0;
            int sembol_skoru = 0;
            String sifre = textBox5.Text;

            //regex kütüphanesi ing kütüphanesi baz aldığından Türkçe karakterlerde sorun yaşamamak için şifre string ifadesindeki Türkçe karakterleri İngilizce karakterlere dönüştürmemiz gerekiyor.

            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            //replace : yer değiştir
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');

            if (sifre != duzeltilmis_sifre)
            { //yukarıdaki işlemlerden biriyle değişiklik yapıldıysa
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür");
            }

            //bir küçük harf 10 puan birden fazlaysa 20 puan
            //sifrenin toplam uzunluğundan küçük harflerin çıkmış halini yazarak küçük harf sayısını bulmuş oluyoruz.
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;
            //2den fazla olsa da 10 ile çarp
            int AZ_kakarakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_kakarakter_sayisi) * 10;

            //1 rakam 10 puan 2 ve üzeri 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            //1 sembol 10 puan 2 ve üzeri 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_kakarakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length == 9)
            {
                parola_skoru += 10;

            }
            else if (sifre.Length == 10)
            {
                parola_skoru += 20;
            }

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
            {
                label22.Text = "Büyük harf, küçük harf, rakam ve sembol mutlaka kullanmalısın";

            }
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
            {
                label22.Text = "";
            }
            if (parola_skoru < 70)
            {
                parola_seviyesi = "Kabul edilemez";

            }
            else if (parola_skoru == 70 || parola_skoru == 80)
            {
                parola_seviyesi = "Güçlü parola";
            }
           
            else if(parola_skoru==90 || parola_skoru==100){
                parola_seviyesi = "Çok güçlü";

            }

            label9.Text = "%" + Convert.ToString(parola_skoru);
            label10.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string yetki = "";
           
            {
                //böyle bir kayıta rastlanmamışsayı kontrol ediyoruz.
                //TC KİMLİK NO KONTROLÜ
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                {
                    label1.ForeColor = Color.Red;
                }
                else
                {
                    label1.ForeColor = Color.Black;


                }

                //isim
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                {
                    label2.ForeColor = Color.Red;
                }
                else
                {
                    label2.ForeColor = Color.Black;


                }
                //soyadı

                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                {
                    label3.ForeColor = Color.Red;
                }
                else
                {
                    label3.ForeColor = Color.Black;


                }
                //kullanıcı adı kontrolü
                if (textBox4.Text.Length < 8 || textBox4.Text == "")
                {
                    label5.ForeColor = Color.Red;
                }
                else
                {
                    label5.ForeColor = Color.Black;


                }

                //parola veri kontrolü
                if (textBox5.Text == "" || parola_skoru < 70)
                {
                    label6.ForeColor = Color.Red;
                }
                else
                {
                    label6.ForeColor = Color.Black;


                }

                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                {
                    label7.ForeColor = Color.Red;
                }
                else
                {
                    label7.ForeColor = Color.Black;


                }

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox4.Text.Length > 1 && textBox5.Text != "" && textBox5.Text.Length > 1 && textBox6.Text != "" && textBox6.Text.Length > 1 && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                    {
                        yetki = "Yönetici";

                    }

                    else if (radioButton2.Checked == true)
                    {
                        yetki = "Kullanıcı";
                    }
                    try
                    {
                        baglantim.Open();
                        OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',yetki='" + yetki + "' ,kullaniciadi='" + textBox4.Text + "',parola='" + textBox5.Text + "' where tcno='"+textBox1.Text+"'", baglantim);

                        guncellekomutu.ExecuteNonQuery(); //accesse işle komut sonuçlarını
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu", "SKY Takip Programoı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        baglantim.Close();
                        MessageBox.Show("Kullanıcı bilgileri güncellendi", "SKY personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        kullanicilari_göster();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message,"SKY personel takip programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        baglantim.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz", "SKY Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //kayıtkontrol=true ise yani böyle bir kayıt varsa
           
        }
    


    private void topPage1_temizle()
        {
            textBox1.Clear();textBox2.Clear();textBox3.Clear();textBox4.Clear(); textBox5.Clear();textBox6.Clear();
        }

        private void topPage2_temizle()
        {
            pictureBox2.Image = null;maskedTextBox1.Clear(); maskedTextBox2.Clear();maskedTextBox3.Clear();maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1; comboBox3.SelectedIndex = -1; //seçili olan indis hangisiyse hiçbiri görünmemesi için -1 diyoruz.

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false; //daha önceden böyle bir kayıt var mı diye bakıyoruz.Başlangıç olarak yok kabul ettik.

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);

            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader(); //select sorgusunun sonuçlarını buraya aktarıyoruz.
            while (kayitokuma.Read())
            {  //txt1e girilen tcye ait herhangi bir kayıta rastlandı mı ona bakıyoruz.
                kayitkontrol = true;
                break;
               
            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                //böyle bir kayıta rastlanmamışsayı kontrol ediyoruz.
                //TC KİMLİK NO KONTROLÜ
                if(textBox1.Text.Length<11 || textBox1.Text == "")
                {
                    label1.ForeColor = Color.Red;
                }
                else
                {
                    label1.ForeColor = Color.Black;
                   

                }

                //isim
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                {
                    label2.ForeColor = Color.Red;
                }
                else
                {
                    label2.ForeColor = Color.Black;


                } 
                //soyadı

                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                {
                    label3.ForeColor = Color.Red;
                }
                else
                {
                    label3.ForeColor = Color.Black;


                }
                //kullanıcı adı kontrolü
                if (textBox4.Text.Length < 8 || textBox4.Text == "")
                {
                    label5.ForeColor = Color.Red;
                }
                else
                {
                    label5.ForeColor = Color.Black;


                }

                //parola veri kontrolü
                if (textBox5.Text == "" || parola_skoru < 70)
                {
                    label6.ForeColor = Color.Red;
                }
                else
                {
                    label6.ForeColor = Color.Black;


                }

                if (textBox6.Text == "" || textBox5.Text!=textBox6.Text)
                {
                    label7.ForeColor = Color.Red;
                }
                else
                {
                    label7.ForeColor = Color.Black;


                }

                if(textBox1.Text.Length==11 && textBox1.Text!="" && textBox2.Text!=""&& textBox2.Text.Length>1 && textBox3.Text!=""&& textBox3.Text.Length>1 && textBox4.Text != "" && textBox4.Text.Length > 1 && textBox5.Text != "" && textBox5.Text.Length > 1 && textBox6.Text != "" && textBox6.Text.Length > 1 && textBox5.Text==textBox6.Text&& parola_skoru>=70 )
                {
                    if (radioButton1.Checked == true)
                    {
                        yetki = "Yönetici";

                    }

                    else if (radioButton2.Checked == true)
                    {
                        yetki = "Kullanıcı";
                    }
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values ('"+textBox1.Text+"', '" + textBox2.Text+"','" + textBox3.Text+"','"+yetki+"' , '"+ textBox4.Text+"','" + textBox5.Text+"')",baglantim);

                        eklekomutu.ExecuteNonQuery(); //accesse işle komut sonuçlarını
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu", "SKY Takip Programoı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                    }
                    catch(Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz", "SKY Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //kayıtkontrol=true ise yani böyle bir kayıt varsa
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası daha önceden kayıtlıdır", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ARA BUTONU
            bool kayit_arama_durumu = false; //başlangıçta yazılan tc ile kayıt olmadığını gösterir

            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                //kullanıcılar tablosundaki txt1deki kayıtları getir
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno= '" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())  //kayıtokumada eğer kayıt varsa
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString(); //access tablosundaki isim bilgisinin txt2ye yazılmasını sağladık
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    //yetksini aldırıyoruz.
                    if(kayitokuma.GetValue(3).ToString()== "Yönetici")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }

                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Aranan kayıt bulunamadı", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                baglantim.Close();


            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneli Bir TC Kimlik No Griniz ", "SKY PersonelTakip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                //tc ile girilen kayıt access tablosunda var mı sorguluyoruz
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete from kullanicilar where tcno='" + textBox1.Text+ "'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_göster();
                    topPage1_temizle();
                    break;
                }

                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglantim.Close();
                topPage1_temizle();
            }
           
            else {
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik No giriniz", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //butona tıklandığında kullanıcı resmi seçecek
            OpenFileDialog resimsec = new OpenFileDialog(); //resim için nesne oluşturduk
            resimsec.Title = "Personel resmi seçiniz";
            resimsec.Filter = " JPG Dosyalar(*.jpg)| *.jpg ";//sadece jpg dosyaları görecek kullanıcı
            
            if(resimsec.ShowDialog()==DialogResult.OK){
                //resimseçme dialoğu kullanıcıya gösterildiyse
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile()); //seçilen resmin picturebox2ye yüklenmesini sağladık


            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayitkontrol = false;  //girilen tc ile daha önceden personel kaydı var mı diye bakmak için

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();

            while (kayitokuma.Read())
            { //eğer kayıt okuma gerçekleşmişse böyle bir kayıt bulunduysa
                kayitkontrol = true;
                break;

            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                //girilen tc ile kayıtlı başka bir kayıt yoksa kayıt işlemi yapılır
                if (pictureBox2.Image == null) //gözattan fotograf seçilmediyse
                {
                    button6.ForeColor = Color.Red;
                }
                else
                {
                    button6.ForeColor = Color.Black;
                }
                if (maskedTextBox1.MaskCompleted==false) //tamamlandı mı, kurala uyuldu mu
                {
                    label13.ForeColor = Color.Red;
                }
                else
                {
                    label13.ForeColor = Color.Black;
                }
                if (maskedTextBox2.MaskCompleted == false) //tamamlandı mı, kurala uyuldu mu
                {
                    label14.ForeColor = Color.Red;
                }
                else
                {
                    label14.ForeColor = Color.Black;
                }
                if (maskedTextBox3.MaskCompleted == false) //tamamlandı mı, kurala uyuldu mu
                {
                    label15.ForeColor = Color.Red;
                }
                else
                {
                    label15.ForeColor = Color.Black;
                }
                if (comboBox1.Text=="") //mezuiyeti alanından seçim yapıldı mı
                {
                    label17.ForeColor = Color.Red;
                }
                else
                {
                    label17.ForeColor = Color.Black;
                }
                if (comboBox2.Text == "") //görevi alanından seçim yapıldı mı
                {
                    label19.ForeColor = Color.Red;
                }
                else
                {
                    label19.ForeColor = Color.Black;
                }
                if (comboBox3.Text == "") //görev yeri alanından seçim yapıldı mı
                {
                    label20.ForeColor = Color.Red;
                }
                else
                {
                    label20.ForeColor = Color.Black;
                }

                if (maskedTextBox4.MaskCompleted == false)
                {
                    label21.ForeColor = Color.Red;
                }
                else
                {
                    label21.ForeColor = Color.Black;
                }
                if (int.Parse(maskedTextBox4.Text) < 10000)
                {
                    label21.ForeColor = Color.Red;
                }
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton3.Checked == true)
                    {
                        cinsiyet = "Bay";
                    }
                    else if (radioButton4.Checked == true)
                        cinsiyet = "Bayan";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into personeller values('" + maskedTextBox1.Text + "', '" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "' , '" + cinsiyet + "', '" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox2.Text + "' ,'" + comboBox3.Text + "','" + maskedTextBox4.Text + "' )", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();

                        //girilen personeller resimlerini depolamak için
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler"))
                            //personelresimler diye klasör yoksa
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler");
                       
                            pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextBox1.Text + ".jpg"); //tc kimlik numarası ile kaydedildi
                            MessageBox.Show("Yeni personel kaydı oluşturuldu", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            personelleri_göster();
                            topPage2_temizle();
                            maskedTextBox4.Text = "0";

                         
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }

                }

                else
                   MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası daha önceden kayıtlıdır!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}

    
            
 

