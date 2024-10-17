using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scout_Otomasyonu_Framework.sınıflar;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scout_Otomasyonu_Framework
{
    public partial class textBox10 : Form
    {
        public textBox10()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            LoadOrRefreshPage();
        }

        public void LoadOrRefreshPage()
        {
            SqlCommand commandList = new SqlCommand("Select * from Oyuncular", SqlOp.connection);

            SqlOp.Checkconnection(SqlOp.connection);

            SqlDataAdapter da = new SqlDataAdapter(commandList);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            textboxedit_no.Text = (dataGridView1.CurrentRow.Cells["Oyuncu_no"].Value).ToString();
            textboxedit_adi.Text = (dataGridView1.CurrentRow.Cells["Oyuncu_adi"].Value).ToString();
            textboxedit_soyadi.Text = (dataGridView1.CurrentRow.Cells["Oyuncu_soyadi"].Value).ToString();
            textboxedit_piyasa.Text = (dataGridView1.CurrentRow.Cells["piyasa_degeri"].Value).ToString();
            textboxedit_yas.Text = (dataGridView1.CurrentRow.Cells["Oyuncu_yasi"].Value).ToString();
            textboxedit_boy.Text = (dataGridView1.CurrentRow.Cells["Oyuncu_boyu"].Value).ToString();
            textboxedit_ayak.Text = (dataGridView1.CurrentRow.Cells["ayak_id"].Value).ToString();
            textboxedit_ülke.Text = (dataGridView1.CurrentRow.Cells["Ulke_no"].Value).ToString();
            textboxedit_takim.Text = (dataGridView1.CurrentRow.Cells["takim_no"].Value).ToString();
            textboxedit_mevki.Text = (dataGridView1.CurrentRow.Cells["mevki_id"].Value).ToString();


            try
            {
                int selectid = Convert.ToInt32(textboxedit_no.Text);
                labeledit.Text = selectid.ToString();
            }
            catch (Exception)
            {


            }

        }

        private void btn_mevkisorgula_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mevki_Box1.Text))
            {
                MessageBox.Show("Lütfen mevki no giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int sorguid = Convert.ToInt32(mevki_Box1.Text);

            SqlCommand commandSorgula = new SqlCommand("Select mevki_adi from Pozisyon WHERE mevki_id = @mevki", SqlOp.connection);

            SqlOp.Checkconnection(SqlOp.connection);

            commandSorgula.Parameters.AddWithValue("@mevki", sorguid);

            try
            {
                SqlOp.Checkconnection(SqlOp.connection);

                object result = commandSorgula.ExecuteScalar();

                if (result != null)
                {
                    MessageBox.Show("Mevki adı: " + result.ToString());
                }
                else
                {
                    MessageBox.Show("Böyle bir mevki bulunamadı");
                }
            }
            catch
            {

            }
        }

        // int girilen yere kelime girmesini engelliyor
        private void mevki_Box1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Girilen karakterin sayı olup olmadığını kontrol et
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer sayı değilse, olayı işleme
                e.Handled = true;

                // Kullanıcıya hata mesajını göster
                MessageBox.Show("Lütfen sadece sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void kupasorgula_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(kupaBox.Text))
            {
                MessageBox.Show("hata.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int OyuncuNo = Convert.ToInt32(kupaBox.Text);

            // Oyuncunun aldığı kupaları ve miktarını gösteren sorgu
            string sorgu = "SELECT k.kupa_adi, kk.kackez_aldi " +
                           "FROM Kupa_Kazananlar kk " +
                           "JOIN Kupalar k ON kk.kupa_id = k.kupa_id " +
                           "WHERE kk.Oyuncu_no = @oyuncuNo";

            using (SqlCommand komut = new SqlCommand(sorgu, SqlOp.connection))
            {
                komut.Parameters.AddWithValue("@oyuncuNo", OyuncuNo);

                SqlOp.Checkconnection(SqlOp.connection);

                try
                {
                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string kupaAdi = reader["kupa_adi"].ToString();
                                int kacKereAldi = Convert.ToInt32(reader["kackez_aldi"]);

                                MessageBox.Show($"Oyuncu {OyuncuNo} - Kupa Adı: {kupaAdi}, Kaç Kere Aldı: {kacKereAldi}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Oyuncunun hiç kupa kazanımı yok.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
        }

        private void btn_takimara_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(takim_AraBox.Text))
            {
                MessageBox.Show("lütfen takim id giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int searchId = Convert.ToInt32(takim_AraBox.Text);

            string query = "SELECT takim_adi FROM Takımlar WHERE takim_no = @takimNo";

            using (SqlCommand commandSearch = new SqlCommand(query, SqlOp.connection))
            {
                // Parametre ekleyin
                commandSearch.Parameters.AddWithValue("@takimNo", searchId);

                try
                {
                    // Bağlantıyı açın
                    SqlOp.Checkconnection(SqlOp.connection);

                    // Sorguyu çalıştırın
                    object result = commandSearch.ExecuteScalar();

                    // Eğer sonuç varsa ekrana yazdırın, yoksa bulunamadı mesajını gösterin
                    if (result != null)
                    {
                        MessageBox.Show("Takım adı: " + result.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Takım bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox11.Text))
            {
                MessageBox.Show("Hatalı silme islemi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectId = Convert.ToInt32(textBox11.Text);

            SqlCommand commandDelete = new SqlCommand("Delete from Oyuncular where Oyuncu_no=@no", SqlOp.connection);

            SqlOp.Checkconnection(SqlOp.connection);

            commandDelete.Parameters.AddWithValue("@no", selectId);

            commandDelete.ExecuteNonQuery();

            LoadOrRefreshPage();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Lütfen formdaki alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand commandadd = new SqlCommand("Insert into Oyuncular(Oyuncu_no,Oyuncu_adi,Oyuncu_soyadi,piyasa_degeri,Oyuncu_yasi,Oyuncu_boyu,ayak_id,Ulke_no,takim_no,mevki_id) values(@no,@name,@surname,@piyasa,@yas,@boy,@ayak,@ulke,@takim,@mevki)", SqlOp.connection);
            SqlOp.Checkconnection(SqlOp.connection);

            commandadd.Parameters.AddWithValue("@no", textBox1.Text);
            commandadd.Parameters.AddWithValue("@name", textBox2.Text);
            commandadd.Parameters.AddWithValue("@surname", textBox3.Text);
            commandadd.Parameters.AddWithValue("@piyasa", textBox4.Text);
            commandadd.Parameters.AddWithValue("@yas", textBox5.Text);
            commandadd.Parameters.AddWithValue("@boy", textBox6.Text);
            commandadd.Parameters.AddWithValue("@ayak", textBox7.Text);
            commandadd.Parameters.AddWithValue("@ulke", textBox8.Text);
            commandadd.Parameters.AddWithValue("@mevki", textBoxbk.Text);

            if (string.IsNullOrEmpty(textBox9.Text))
            {
                commandadd.Parameters.AddWithValue("@takim", DBNull.Value);
            }
            else
            {
                commandadd.Parameters.AddWithValue("@takim", textBox9.Text);
            }



            try
            {
                commandadd.ExecuteNonQuery();
                LoadOrRefreshPage();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e) // düzenleme butonu tasarımdan dolayı değişti
        {
            if (string.IsNullOrEmpty(textboxedit_adi.Text) || string.IsNullOrEmpty(textboxedit_soyadi.Text))
            {
                MessageBox.Show("Oyuncu adı ve soyadı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            SqlCommand commandEdit = new SqlCommand("Update Oyuncular set Oyuncu_adi=@adi , Oyuncu_soyadi=@soyadi , piyasa_degeri=@piyasa , Oyuncu_yasi=@yas ,ayak_id=@ayak, Ulke_no=@ulke ,takim_no=@takim , mevki_id=@mevki, Oyuncu_boyu=@boy where Oyuncu_no=@no", SqlOp.connection);

            SqlOp.Checkconnection(SqlOp.connection);

            commandEdit.Parameters.AddWithValue("@no", textboxedit_no.Text);
            commandEdit.Parameters.AddWithValue("@adi", textboxedit_adi.Text);
            commandEdit.Parameters.AddWithValue("@soyadi", textboxedit_soyadi.Text);
            commandEdit.Parameters.AddWithValue("@yas", textboxedit_yas.Text);
            commandEdit.Parameters.AddWithValue("@ayak", textboxedit_ayak.Text);
            commandEdit.Parameters.AddWithValue("@ulke", textboxedit_ülke.Text);
            commandEdit.Parameters.AddWithValue("@mevki", textboxedit_mevki.Text);
            commandEdit.Parameters.AddWithValue("@boy", textboxedit_boy.Text);

            if (string.IsNullOrEmpty(textboxedit_takim.Text))
            {
                commandEdit.Parameters.AddWithValue("@takim", DBNull.Value);
            }
            else
            {
                commandEdit.Parameters.AddWithValue("@takim", textboxedit_takim.Text);
            }



            // her düzenlemede piyasa değeri artıyordu kendi kendine forumdan aldım
            if (decimal.TryParse(textboxedit_piyasa.Text, out decimal piyasaValue))
            {
                commandEdit.Parameters.AddWithValue("@piyasa", piyasaValue);
            }
            else
            {

                MessageBox.Show("Geçersiz piyasa değeri.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                commandEdit.ExecuteNonQuery();

                LoadOrRefreshPage();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata!");
                
            }
        }

        private void kupaBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Girilen karakterin sayı olup olmadığını kontrol et
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer sayı değilse, olayı işleme
                e.Handled = true;

                // Kullanıcıya hata mesajını göster
                MessageBox.Show("Lütfen sadece sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void takim_AraBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Girilen karakterin sayı olup olmadığını kontrol et
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer sayı değilse, olayı işleme
                e.Handled = true;

                // Kullanıcıya hata mesajını göster
                MessageBox.Show("Lütfen sadece sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Girilen karakterin sayı olup olmadığını kontrol et
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer sayı değilse, olayı işleme
                e.Handled = true;

                // Kullanıcıya hata mesajını göster
                MessageBox.Show("Lütfen sadece sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textboxedit_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                
                e.Handled = true;

                MessageBox.Show("Lütfen sadece sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
    }




