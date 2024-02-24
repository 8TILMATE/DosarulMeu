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
using V3.Models;

namespace V3
{
    public partial class DashBoard : Form
    {
        AutoCompleteStringCollection source = new AutoCompleteStringCollection();
        public static UserModel md;


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
                    MessageBox.Show("TickTock");
                    md = model; break;
                }
            }
            if (md != null)
            {
                TestingStuff.PdfTest x = new TestingStuff.PdfTest();
                this.Hide();
                x.ShowDialog();
                this.Close();
            }
        }
    }

}
