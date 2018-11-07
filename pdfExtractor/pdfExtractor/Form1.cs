using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdfExtractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ShowPDFbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string filePath;
            dlg.Filter = "PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName.ToString();


                string pdftext = string.Empty;
                try
                {
                    PdfReader reader = new PdfReader(filePath);
                    for(int page=1;page<= reader.NumberOfPages;page++)
                    {
                        ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                        String s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                        pdftext = pdftext + s;
                        PDFtextBox.Text = pdftext;
                    }
                    reader.Close();
                    IDictionary<String, String> metadic = reader.Info;
                    var dic = from m in metadic
                              select m;
                    foreach (var d in dic)
                    {
                        MetadatalistBox.Items.Add(d.Key + ": " + d.Value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}



 

