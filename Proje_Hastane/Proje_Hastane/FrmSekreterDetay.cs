using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tcnumara;
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tcnumara;

            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter Where SekreterTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları Listeye Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar",bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorları Listeye Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select DoktorAd,DoktorSoyad,DoktorBrans From Tbl_Doktorlar",bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşı combobxa aktarma
            SqlCommand komut2 = new SqlCommand("select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) Values (@p1,@p2,@p3,@p4)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1",mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2",mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3",cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4",cmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",cmbBrans.Text);
            SqlDataReader dr = komut3.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0]+" "+dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) Values (@p1) ",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu!!!");
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fr = new FrmDoktorPaneli();
            fr.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frm = new FrmBrans();
            frm.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm1 = new FrmDuyurular();
            frm1.Show();
        }
    }
}
