using Firebase.Database;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V3.Models;

namespace V3.DatabaseHelpers
{
    public class DatabaseHelper1
    {
        public static FirebaseClient firebaseClient = new FirebaseClient("https://dosarul-meu-f665c-default-rtdb.europe-west1.firebasedatabase.app/");
        public static List<Models.UserModel> utilizatori = new List<Models.UserModel>();
        public static void LoadUsers()
        {
            var res = firebaseClient.Child("Utilizatori").OnceAsync<UserModel>().Result;
            for(int i=0;i<res.Count;i++)
            {
                utilizatori.Add(new UserModel
                {
                    CNP = res.ToList()[i].Object.CNP,
                    Id = res.ToList()[i].Object.Id,
                    Email = res.ToList()[i].Object.Email,
                    Nume = res.ToList()[i].Object.Nume
                });
            }

        }
        public static void DownloadFile(string bucketName = "dosarul-meu-f665c.appspot.com", string objectName = "PDF.pdf",string localPath = "C:\\Users\\rafxg\\source\\repos\\V3\\V3\\ResourcesV2\\xx.pdf")
        {
            var storage = StorageClient.Create();
            var outputFile = File.OpenWrite(localPath);
            storage.DownloadObject(bucketName, objectName, outputFile);
            Console.WriteLine($"Downloaded {objectName} to {localPath}.");
        }
    }
}
