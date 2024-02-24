using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V3.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace V3
{
    public partial class Form1 : Form
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://dosarul-meu-f665c-default-rtdb.europe-west1.firebasedatabase.app/");
        List<string> listpar = new List<string>();

        List<string> list2 = new List<string>();

        public ObservableCollection<string> TodoItems { get; set; } = new ObservableCollection<string>();
        public Form1()
        {

            var res = firebaseClient.Child("Administratori").OnceAsync<AdminModel>().Result;

            for (int i = 0; i < res.Count; i++) 
            {

                list2.Add(res.ToList()[i].Object.Email.ToString());
                listpar.Add(res.ToList()[i].Object.Pass.ToString());
            }
            InitializeComponent();
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            if (list2.Contains(textBox1.Text))
            {
                if (listpar.Contains(textBox2.Text))
                {
                    MessageBox.Show("Logged");
                    var page =new DashBoard();
                    this.Hide();
                    page.ShowDialog();
                    this.Close();
                }
            }
        }
        private async void GetUser(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
