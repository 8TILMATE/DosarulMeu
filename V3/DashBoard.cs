using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Pdf.Parsing;
using V3.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Mail;
using System.Net;

namespace V3
{
    public partial class DashBoard : Form
    {
        AutoCompleteStringCollection source = new AutoCompleteStringCollection();
        public static UserModel md = null;
        public static bool IsPassedOn=false;


        public DashBoard()
        {
            InitializeComponent();
        }

        private async void DashBoard_Load(object sender, EventArgs e)
        {
            textBox1.AutoCompleteSource = AutoCompleteSource.None;
            DatabaseHelpers.DatabaseHelper1.LoadUsers();
            await Task.Delay(1000);
            //AutoCompletion();
        }
        /*
        private void AutoCompletion()
        {
            string[] CNPS= new string[DatabaseHelpers.DatabaseHelper1.utilizatori.Count];
            foreach (var item in DatabaseHelpers.DatabaseHelper1.utilizatori)
            {
               CNPS.Append(item.CNP.ToString());
            }

            textBox1.AutoCompleteCustomSource.Clear();
                source.AddRange(CNPS);
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(CNPS);
                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox1.AutoCompleteCustomSource = collection;
            
        }
        */
        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            //AutoCompletion();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (UserModel model in DatabaseHelpers.DatabaseHelper1.utilizatori)
            {
                if (model.CNP == textBox1.Text)
                {
                   // MessageBox.Show("TickTock");
                    md = model; break;
                }

            }
            if (md != null)
            {

                DatabaseHelpers.DatabaseHelper1.CNPSearchDoc(md.CNP);
                label3.Text = md.Nume;
                label4.Text = md.CNP;
                label5.Text = md.Email;
                if (DatabaseHelpers.DatabaseHelper1.Documete.Count > 0)
                {
                    foreach(var x in DatabaseHelpers.DatabaseHelper1.Documete)
                    {
                        comboBox1.Items.Add(x.NumeFisier);
                    }
                    //label2.Text = "Status: " + DatabaseHelpers.DatabaseHelper1.Documete[comboBox1.SelectedIndex].Status;
                    
                }
                //System.Diagnostics.Process.Start("https://firebasestorage.googleapis.com/v0/b/dosarul-meu-f665c.appspot.com/o/PDF.pdf");

                /*
                TestingStuff.PdfTest x = new TestingStuff.PdfTest();
                this.Hide();
                x.ShowDialog();
                this.Close();
                */

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Items.Count > 0)
            {
                label2.Text = "Status: " + DatabaseHelpers.DatabaseHelper1.Documete[comboBox1.SelectedIndex].Status;

                DatabaseHelpers.DatabaseHelper1.DownloadFile(comboBox1.SelectedItem.ToString() + ".pdf", "C:\\Users\\rafxg\\source\\repos\\V3\\V3\\ResourcesV2\\" + comboBox1.SelectedItem.ToString() + ".pdf");

                //PdfLoadedDocument document = new PdfLoadedDocument
                webBrowser1.Url=new Uri
                ("C:\\Users\\rafxg\\source\\repos\\V3\\V3\\ResourcesV2\\" + comboBox1.SelectedItem.ToString() + ".pdf");
                //webView21.NavigateToString("C:\\Users\\rafxg\\source\\repos\\V3\\V3\\ResourcesV2\\" + comboBox1.SelectedItem.ToString() + ".pdf");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsPassedOn = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsPassedOn = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();

            // Set the sender, recipient, subject and body of the email
            mail.From = new MailAddress("rafxgam@gmail.com");
            mail.To.Add(new MailAddress(md.Email));
            mail.Subject = "STATUS SCHIMBAT LA DOSARUL #" + comboBox1.Items[comboBox1.SelectedIndex].ToString();
            if (IsPassedOn)
            {
                mail.Body = "Proces Finalizat Cu Succes \n";
            }
            else
            {

                mail.Body = "Document Respins \n" + textBox2.Text;
            }

            DatabaseHelpers.DatabaseHelper1.UpdateStatus(comboBox1.Items[comboBox1.SelectedIndex].ToString(), IsPassedOn,textBox2.Text);
            // Create a new SMTP client object
            SmtpClient smtp = new SmtpClient();

            // Set the host, port, credentials and SSL settings of the SMTP server
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("dosarulmeums3@gmail.com", "kvar bggk mlcn vwfb");
            smtp.EnableSsl = true;

            try
            {
                // Send the email using the SMTP client
                smtp.Send(mail);

                // Display a success message
                MessageBox.Show("Email sent successfully!");

            }
            catch (Exception ex)
            {
                // Display an error message
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }
    }

}
