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
        public WorkPiece(PdfReader MyReader,string FilePath, string Author, string Title, string Creator, string Language, string Type, string Subject, string Keywords, string CreatedDate)
        {
            this.MyReader = MyReader;
            this.FilePath = FilePath;
            this.Author = Author;
            this.Title = Title;
            this.Creator = Creator;
            this.Language = Language;
            this.Type = Type;
            this.Subject = Subject;
            this.Keywords = Keywords;
            this.CreatedDate = CreatedDate;
        }

        public PdfReader MyReader { get; set; }
        public string FilePath { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
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
            {
                CreatedDate = reader.Info["CreationDate"];
                CreatedDate = CreatedDate.Substring(2, 4);
            }
            
            reader.Close();
        }

        /*public void SetInfo(string myfilepath, string keyword, string message)
        {
            PdfReader myreader = new PdfReader(myfilepath);
            String folder = Path.GetDirectoryName(myfilepath);
            String file = Path.GetFileName(myfilepath).Remove(Path.GetFileName(myfilepath).Length - 4);
            String dest = folder + file + "_meta.pdf";
            Console.WriteLine(dest);

            //FileStream fs = new FileStream(myfilepath, FileMode.Append, FileAccess.Write, FileShare.None);
            PdfStamper stamper = new PdfStamper(myreader, 
            new FileStream(dest, FileMode.Create, FileAccess.Write, FileShare.None));
                
            var info = myreader.Info;
            if (myreader.Info.ContainsKey(keyword))
                info[keyword] = message;
            else
                info.Add(keyword, message);
            stamper.MoreInfo = info;
            stamper.FormFlattening = true;
            stamper.Close();

                    /* using (var ms = new MemoryStream())
                     {
                         var xmp = new XmpWriter(ms, info);
                         stamper.XmpMetadata = ms.ToArray();
                         xmp.Close();
                     }*/
                
                
            
           // myreader.Close();

        //}
    }
}
