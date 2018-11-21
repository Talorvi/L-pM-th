using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            string Author = "", Title = "", Creator = "", Subject = "", Keywords = "", CreatedDate = "";
            WorkPiece workpiece = new WorkPiece(Author, Title, Creator, Subject, Keywords, CreatedDate);

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
                    FileInfo myfilepath = new FileInfo(filePath);
                    workpiece.GetInfoFromPDF(reader, myfilepath);
                    reader.Close();

                    AuthortextBox.Text = workpiece.Author;
                    TitletextBox.Text = workpiece.Title;
                    CreatortextBox.Text = workpiece.Creator;
                    SubjecttextBox.Text = workpiece.Subject;
                    KeywordstextBox.Text = workpiece.Keywords;
                    CreationDatetextBox.Text = workpiece.CreatedDate;

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



 

