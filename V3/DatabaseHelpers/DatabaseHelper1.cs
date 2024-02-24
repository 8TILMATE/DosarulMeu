using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
