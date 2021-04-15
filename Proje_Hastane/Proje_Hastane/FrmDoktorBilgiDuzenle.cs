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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCNO;

            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar Where DoktorTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[4].ToString();
;            }
            bgl.baglanti().Close();


        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_DOktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 Where DoktorTC=@p5",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4",txtSifre.Text);
            komut.Parameters.AddWithValue("@p5",mskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi");
        }
    }
}
