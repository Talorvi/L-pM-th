using iTextSharp.text.pdf;
using iTextSharp.text.xml.xmp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfExtractor
{
    public class WorkPiece
    {
        public WorkPiece(PdfReader MyReader,string FilePath, string Author, string Title, string Creator, string Subject, string Keywords, string CreatedDate)
        {
            this.MyReader = MyReader;
            this.FilePath = FilePath;
            this.Author = Author;
            this.Title = Title;
            this.Creator = Creator;
            this.Subject = Subject;
            this.Keywords = Keywords;
            this.CreatedDate = CreatedDate;
        }

        public PdfReader MyReader { get; set; }
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Subject { get; set; }
        public string Keywords { get; set; }
        public string CreatedDate { get; set; }


        public void GetInfoFromPDF(PdfReader reader, FileInfo myfilepath)
        {
            if (reader.Info.ContainsKey("Author"))
                Author = reader.Info["Author"];
            if (reader.Info["Author"] == "" || reader.Info.ContainsKey("Author") == false)
                Author = myfilepath.Name.Substring(0, myfilepath.Name.LastIndexOf(".pdf"));

            if (reader.Info.ContainsKey("Title"))
                Title = reader.Info["Title"];
            if (reader.Info["Title"] == "" || reader.Info.ContainsKey("Title") == false)
                Title = myfilepath.Name.Substring(0, myfilepath.Name.LastIndexOf(".pdf"));

            if (reader.Info.ContainsKey("Creator"))
                Creator = reader.Info["Creator"];
            if (reader.Info.ContainsKey("Subject"))
                Subject = reader.Info["Subject"];
            if (reader.Info.ContainsKey("Keywords"))
                Keywords = reader.Info["Keywords"];
            if (reader.Info.ContainsKey("CreationDate"))
                CreatedDate = reader.Info["CreationDate"];
            CreatedDate = CreatedDate.Substring(2, 4);
            reader.Close();
        }

        public void SetInfo(string myfilepath, string keyword, string message)
        {
            using (PdfReader myreader = new PdfReader(myfilepath))
            {
                using (var stamper = new PdfStamper(myreader, new FileStream(myfilepath, FileMode.Create)))
                {
                    var info = myreader.Info;
                    info[keyword] = message;
                    stamper.MoreInfo = info;
                    

                    using (var ms = new MemoryStream())
                    {
                        var xmp = new XmpWriter(ms, info);
                        stamper.XmpMetadata = ms.ToArray();
                        xmp.Close();
                    }
                }
                myreader.Close();
            }
            
        }
    }
}
