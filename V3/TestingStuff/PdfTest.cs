using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iText.Kernel.Pdf;
using Firebase.Storage;
using iText.Forms;
using iText.Forms.Fields;
namespace V3.TestingStuff
{
    public partial class PdfTest : Form
    {
        public PdfTest()
        {
            InitializeComponent();
        }

        private void PdfTest_Load(object sender, EventArgs e)
        {

        }
        public static Task<byte[]> GetPdfBytes(string pdfFilename)
        {
            byte[] pdfBytes = null;

            using (PdfReader reader = new PdfReader(pdfFilename))
            {
                using (MemoryStream msPdfWriter = new MemoryStream())
                {
                    using (PdfWriter writer = new PdfWriter(msPdfWriter))
                    {
                        using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
                        {
                            //don't close underlying streams when PdfDocument is closed
                            pdfDoc.SetCloseReader(false);
                            pdfDoc.SetCloseWriter(false);

                            //close
                            pdfDoc.Close();

                            //set value
                            msPdfWriter.Position = 0;

                            //convert to byte[]
                            pdfBytes = msPdfWriter.ToArray();
                        }
                    }
                }
            }

            return Task.FromResult(pdfBytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] pdfBytes = null;
            string filename = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            stam(ofd.FileName);
            //ofd.Filter = "PDF File (*.pdf)|*.pdf";

            /*if (ofd.ShowDialog() == DialogResult.OK)
            {
                //get PDF as byte[]
                var tResult = GetPdfBytes(ofd.FileName);
                pdfBytes = tResult.Result;

                //For testing, write back to file so we can ensure that the PDF file opens properly
                //create a new filename
                filename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName) + " - Test.pdf";
                filename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(ofd.FileName), filename);
                //save to file
                File.WriteAllBytes(filename, pdfBytes);
                System.Diagnostics.Debug.WriteLine("Saved as '" + filename + "'");

            }*/
            //DatabaseHelpers.DatabaseHelper1.DownloadFile();
        }
        public async  Task stam(string stream)
        {
            var strea1m = File.Open(stream, FileMode.Open);
            var task = new FirebaseStorage("dosarul-meu-f665c.appspot.com")
                .Child("232012.pdf")
                .PutAsync(strea1m);
            
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
            
          //  var download = new FirebaseStorage("dosarul-meu-f665c.appspot.com");
           // var x = download.Child("PDF.pdf").GetDownloadUrlAsync().Result;
            //Console.WriteLine(x);
            // Await the task to wait until upload is completed and get the download url
            //var downloadUrl = await task;
            //Console.WriteLine(downloadUrl);

           

        }
    }
}
