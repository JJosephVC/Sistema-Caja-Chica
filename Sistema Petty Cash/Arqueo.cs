using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NReco.PdfGenerator;
using NReco;

namespace Sistema_Petty_Cash
{
    public partial class Arqueo : Form
    {
        private List<TextBox> sumartextbox;

        private List<TextBox> textbox_alterados;

        List<TextBox> txtMultiplicacionBilletesList;
        List<TextBox> txtMultiplicacionMonedasList;


        List<TextBox> txtTotalEfectivoList;

        List<TextBox> txtTotalArquoList;

        public Arqueo()
        {
            InitializeComponent();
            sumartextbox = new List<TextBox> { textBox0_2,textBox1_2,textBox2_2,textBox3_2,textBox4_2,textBox5_2,textBox6_2,textBox7_2,
            textBox8_2,textBox9_2,textBox10_2,textBox11_2,textBox12_2,textBox13_2,textBox14_2};

            textbox_alterados = new List<TextBox> { textBox0_0, textBox1_0, textBox2_0, textBox3_0, textBox4_0, textBox5_0, textBox6_0, textBox7_0, textBox8_0, textBox9_0, textBox10_0, textBox11_0, textBox12_0, textBox13_0, textBox14_0,
            textBox0_1, textBox1_1, textBox2_1, textBox3_1, textBox4_1, textBox5_1, textBox6_1, textBox7_1, textBox8_1, textBox9_1, textBox10_1, textBox11_1, textBox12_1, textBox13_1, textBox14_1,
            textBox0_2, textBox1_2, textBox2_2, textBox3_2, textBox4_2, textBox5_2, textBox6_2, textBox7_2, textBox8_2, textBox9_2, textBox10_2, textBox11_2, textBox12_2, textBox13_2, textBox14_2};

            txtMultiplicacionBilletesList = new List<TextBox> {txt_multiplicacion_billetes1, txt_multiplicacion_billetes2, txt_multiplicacion_billetes3, txt_multiplicacion_billetes4,
                txt_multiplicacion_billetes5, txt_multiplicacion_billetes6, txt_multiplicacion_billetes7};

            txtMultiplicacionMonedasList = new List<TextBox> {txt_multiplicacion_Monedas1, txt_multiplicacion_Monedas2,
                txt_multiplicacion_Monedas3, txt_multiplicacion_Monedas4, txt_multiplicacion_Monedas5,
                txt_multiplicacion_Monedas6,txt_multiplicacion_Monedas7};

            txtTotalEfectivoList = new List<TextBox> {txt_total_billetes,txtResultadoM};

            txtTotalArquoList = new List<TextBox> {txt_totalefectivo,txt_sumafinal};

            //Recalculando suma de los montos de recibos cada vez que se cambié el Texto de los TextBox a Sumar
            Recalcularsuma_montos();
            foreach (TextBox textBox in sumartextbox)
            {
                textBox.TextChanged += TextBox_TextChanged_monto;
            }

            // Recalculando suma de los billetes cada vez que se cambié el Texto de los TextBox a Sumar
            Recalcular_Billetes();
            foreach (TextBox textBox in txtMultiplicacionBilletesList)
            {
                textBox.TextChanged += TextBox_TextChanged_Billete;
            }

            //Recalcular la suma de las monedas cada vez que se cambié el Texto de los TextBox a sumar
            Recalcular_Monedas();
            foreach (TextBox textBox in txtMultiplicacionMonedasList)
            {
                textBox.TextChanged += TextBox_TextChanged_Moneda;
            }

            Recalcular_Efectivo();
            foreach (TextBox textBox in txtTotalEfectivoList)
            {
                textBox.TextChanged += TextBox_TextChanged_Efectivo;
            }
           
        }
        //Métodos
        private void TextBox_TextChanged_monto(object sender, EventArgs e)
        {
            Recalcularsuma_montos();
        }
        private void Recalcularsuma_montos()
        {
            float suma = 0;
            foreach (TextBox textBox in sumartextbox)
            {
                float valor;
                if (float.TryParse(textBox.Text, out valor))
                {
                    suma += valor;
                }
            }
            txt_sumafinal.Text = suma.ToString("N2");
        }
        private void TextBox_TextChanged_Billete(object sender, EventArgs e)
        {
            Recalcular_Billetes();
        }
        private void Recalcular_Billetes()
        {
            float suma = 0;
            foreach (TextBox textBox in txtMultiplicacionBilletesList)
            {
                float valor;
                if (float.TryParse(textBox.Text, out valor))
                {
                    suma += valor;
                }
            }
            txt_total_billetes.Text = suma.ToString();
        }

        private void TextBox_TextChanged_Moneda(object sender, EventArgs e)
        {
            Recalcular_Monedas();
        }
        private void Recalcular_Monedas()
        {
            float suma = 0;
            foreach (TextBox textBox in txtMultiplicacionMonedasList)
            {
                float valor;
                if (float.TryParse(textBox.Text, out valor))
                {
                    suma += valor;
                }
            }
            txtResultadoM.Text = suma.ToString("N2");
        }

        private void TextBox_TextChanged_Efectivo(object sender, EventArgs e)
        {
            Recalcular_Efectivo();
        }
        private void Recalcular_Efectivo()
        {
            float suma = 0;
            foreach (TextBox textBox in txtTotalEfectivoList)
            {

                float valor;
                if (float.TryParse(textBox.Text, out valor))
                {
                    suma += valor;
                }
            }
            txt_totalefectivo.Text = suma.ToString("N2");
        }
        private void Imprimir_Click(object sender, EventArgs e)
        {
        }

        private void generatorPdf()
        {
            var converter = new HtmlToPdfConverter
            {
                Size = NReco.PdfGenerator.PageSize.Letter,
                Orientation = PageOrientation.Portrait,
                Margins = new PageMargins { Left = 1, Right = 1, Top = 1, Bottom = 1 }
            };
            string directory = Directory.GetCurrentDirectory();
            string archivohtml = "plantillap.html";
            string htmlphat = Path.Combine(directory, archivohtml);

            string paginaHtml_texto = File.ReadAllText(htmlphat);
            paginaHtml_texto = paginaHtml_texto.Replace("@fechaa", txt_Farqueo.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@fondo", txt_fondoarqueado.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@hfinal", txtHfinal.Text);

            paginaHtml_texto = paginaHtml_texto.Replace(" @hincio", txtHincio.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@c1", txt_cantidad_billetes1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@c2", txt_cantidad_billetes2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@c3", txt_cantidad_billetes3.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@c4", txt_cantidad_billetes4.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@c5", txt_cantidad_billetes5.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@c6", txt_cantidad_billetes6.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@c7", txt_cantidad_billetes7.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@t1", txt_multiplicacion_billetes1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@t2", txt_multiplicacion_billetes2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@t3", txt_multiplicacion_billetes3.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@t4", txt_multiplicacion_billetes4.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@t5", txt_multiplicacion_billetes5.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@t6", txt_multiplicacion_billetes6.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@t7", txt_multiplicacion_billetes7.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@subtotalcordobas", txt_total_billetes.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mc1", txtM1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mc2", txtM2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mc3", txtM3.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mc4", txtM4.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mc5", txtM5.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mc6", txtM6.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mc7", txtM7.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mt1", txt_multiplicacion_Monedas1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mt2", txt_multiplicacion_Monedas2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mt3", txt_multiplicacion_Monedas3.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mt4", txt_multiplicacion_Monedas4.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mt5", txt_multiplicacion_Monedas5.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mt6", txt_multiplicacion_Monedas6.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@mt7", txt_multiplicacion_Monedas7.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("msubtotal", txtResultadoM.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@totalefectivo", txt_totalefectivo.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f1", textBox0_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f2", textBox1_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f3", textBox2_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f4", textBox3_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f5", textBox4_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f6", textBox5_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f7", textBox6_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f8", textBox7_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f9", textBox8_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f10", textBox9_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f11", textBox10_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f12", textBox11_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f13", textBox12_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f14", textBox13_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f15", textBox14_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f16", textBox15_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@fe17", textBox16_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f18", textBox17_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f19", textBox18_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f20", textBox19_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f21", textBox20_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f22", textBox21_0.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@f23", textBox22_0.Text);

            paginaHtml_texto = paginaHtml_texto.Replace("@r1", textBox0_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r2", textBox1_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r3", textBox2_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r4", textBox3_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r5", textBox4_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r6", textBox5_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r7", textBox6_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r8", textBox7_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r9", textBox8_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r10", textBox9_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r11", textBox10_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r12", textBox11_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r13", textBox12_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r14", textBox13_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r15", textBox14_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r16", textBox15_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r17", textBox16_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r18", textBox17_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r19", textBox18_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r20", textBox19_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r21", textBox20_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r22", textBox21_1.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@r23", textBox22_1.Text);

            paginaHtml_texto = paginaHtml_texto.Replace("@m1", textBox0_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m2", textBox1_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m3", textBox2_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m4", textBox3_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m5", textBox4_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m6", textBox5_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m7", textBox6_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m8", textBox7_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m9", textBox8_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m10", textBox9_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m11", textBox10_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m12", textBox11_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m13", textBox12_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m14", textBox13_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m15", textBox14_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m16", textBox15_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m17", textBox16_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m18", textBox17_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m19", textBox18_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m20", textBox19_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m21", textBox20_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m22", textBox21_2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@m23", textBox22_2.Text);

            paginaHtml_texto = paginaHtml_texto.Replace("@totaldocumentos", txt_sumafinal.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@observaciones", Observaciones.Text);
            var pdfbytes = converter.GeneratePdf(paginaHtml_texto);


            string rut = Path.Combine(directory, "ArqueoPDFS");
            //string rut = "C:\\Users\\Asus rog\\Desktop\\PDFS";

            try
            {
                rut = Path.Combine(rut, $"Arqueo{DateTime.Now.ToString("dd-MM-yyyy")}.pdf"); //Se guardara en la carpeta de ejecucion del proyecto bin//debug//ArqueoPDFS
                File.WriteAllBytes(rut, pdfbytes);
            }
            catch (System.Exception)
            {
                throw;
            }

            Process.Start(rut);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;

            }
            this.Hide();
            Menu menu = new Menu();
            menu.Show();

            generatorPdf();
        }

        private void txt_cantidad_billetes1_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txt_denominacion_billetes1.Text, out float denominacion) && int.TryParse(txt_cantidad_billetes1.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_billetes1.Text = multiplicacion.ToString("N2");
            }
        }

        private void txt_cantidad_billetes2_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txt_denominacion_billetes2.Text, out float denominacion) && int.TryParse(txt_cantidad_billetes2.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_billetes2.Text = multiplicacion.ToString("N2");
            }
        }

        private void txt_cantidad_billetes3_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txt_denominacion_billetes3.Text, out float denominacion) && int.TryParse(txt_cantidad_billetes3.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_billetes3.Text = multiplicacion.ToString("N2");
            }
        }

        private void txt_cantidad_billetes4_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txt_denominacion_billetes4.Text, out float denominacion) && int.TryParse(txt_cantidad_billetes4.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_billetes4.Text = multiplicacion.ToString("N2");
            }
        }

        private void txt_cantidad_billetes5_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txt_denominacion_billetes5.Text, out float denominacion) && int.TryParse(txt_cantidad_billetes5.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_billetes5.Text = multiplicacion.ToString("N2");
            }
        }

        private void txt_cantidad_billetes6_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txt_denominacion_billetes6.Text, out float denominacion) && int.TryParse(txt_cantidad_billetes6.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_billetes6.Text = multiplicacion.ToString("N2");
            }
        }

        private void txt_cantidad_billetes7_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txt_denominacion_billetes7.Text, out float denominacion) && int.TryParse(txt_cantidad_billetes7.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_billetes7.Text = multiplicacion.ToString("N2");
            }
        }

        private void txtM1_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox47.Text, out float denominacion) && int.TryParse(txtM1.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_Monedas1.Text = multiplicacion.ToString();
            }
        }

        private void txtM2_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox44.Text, out float denominacion) && int.TryParse(txtM2.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_Monedas2.Text = multiplicacion.ToString();
            }
        }

        private void txtM3_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox41.Text, out float denominacion) && int.TryParse(txtM3.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_Monedas3.Text = multiplicacion.ToString();
            }
        }

        private void txtM4_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox39.Text, out float denominacion) && int.TryParse(txtM4.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_Monedas4.Text = multiplicacion.ToString();
            }
        }

        private void txtM5_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox2.Text, out float denominacion) && int.TryParse(txtM5.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_Monedas5.Text = multiplicacion.ToString();
            }
        }

        private void txtM6_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox33.Text, out float denominacion) && int.TryParse(txtM6.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_Monedas6.Text = multiplicacion.ToString();
            }
        }

        private void txtM7_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox29.Text, out float denominacion) && int.TryParse(txtM7.Text, out int cantidad))
            {
                float multiplicacion = denominacion * cantidad;
                txt_multiplicacion_Monedas7.Text = multiplicacion.ToString();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
