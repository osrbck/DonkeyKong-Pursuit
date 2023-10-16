using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    public class UserData : MonoBehaviour
{
        //FIELDS
        public string userName { get; }
        public int userScore { get; set; }

        //PROPERTIES
        public string UserName
        {
            get { return userName; }
        }
        public int UserScore
        {
            get { return userScore; }
            set { userScore = value; }
        }

        public UserData(string name, int score)
        {
            try
            {
                userName = name;
                userScore = score;
            }
            catch(Exception)
            {
                Console.WriteLine("Can't catch the User Data");
            }
        }


    }

