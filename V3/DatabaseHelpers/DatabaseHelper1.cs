using Firebase.Database;
using Firebase.Database.Query;
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
        public static List<Models.DocumentModel> Documete = new List<DocumentModel>();
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
        public static void CNPSearchDoc(string CNP)
        {
            Documete.Clear();
            var res = firebaseClient.Child("Documente").OnceAsync<DocumentModel>().Result;
            for(int i = 0; i < res.Count; i++)
            {
                if (res.ToList()[i].Object.CNP == Int32.Parse(CNP))
                {
                    Documete.Add(new DocumentModel
                    {
                        CNP = res.ToList()[i].Object.CNP,
                        NrReg = res.ToList()[i].Object.NrReg,
                        NumeFisier = res.ToList()[i].Object.NumeFisier,
                        InfoAdd = res.ToList()[i].Object.InfoAdd,
                        Status = res.ToList()[i].Object.Status,
                        TipDocument = res.ToList()[i].Object.TipDocument
                    });
                }
            }
        }
        public static async void UpdateStatus(string NumeFisier,bool IsPassed,string Obs)
        {
            var res = firebaseClient.Child("Documente").OnceAsync<DocumentModel>().Result;
            for(int i =0; i < res.Count;i++)
            {
                if (res.ToList()[i].Object.NumeFisier == NumeFisier)
                {
                    DocumentModel model = res.ToList()[i].Object;
                    if (IsPassed)
                    {
                        model.Status = "proces finalizat";
                    }
                    else
                    {
                        model.Status = "document respins";
                        model.InfoAdd = Obs;
                    }
                    await firebaseClient.Child("Documente").Child(res.ToList()[i].Key).DeleteAsync();
                    await firebaseClient.Child("Documente").PostAsync(model);
                }

            }



        }
        public static void DownloadFile( string objectName,string localPath, string bucketName = "dosarul-meu-f665c.appspot.com")
        {
            var storage = StorageClient.Create();
            var outputFile = File.OpenWrite(localPath);
            storage.DownloadObject(bucketName, objectName, outputFile);
            Console.WriteLine($"Downloaded {objectName} to {localPath}.");
            outputFile.Close();
        }
    }
}
