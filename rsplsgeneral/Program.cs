using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace rsplsgeneral
{
    public abstract class ComputerChoice
    {
        protected const int n = 7;
        protected static string[] move = new string[n] { "Rock", "Scissors", "Paper", "Lizard", "Spock","Pencil" ,"Fire"};
        protected int comp;
        private int key;
        private byte[] computer = new byte[1];
        public void Computermove()
        {           
            RNGCryptoServiceProvider Generate = new RNGCryptoServiceProvider();
            Generate.GetBytes(computer);
            comp = Convert.ToInt32(computer[0]) % n;
            return;
        }
        public void GettheKey()
        {
            byte[] keybyte = new byte[1];
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();
            Gen.GetBytes(keybyte);
            key = Convert.ToInt32(keybyte[0]);
        }
        public int GettheHash()
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();
            byte[] hash = sha.ComputeHash(computer);
            int hashint = Convert.ToInt32(hash[0]) + key;
            Console.WriteLine("The computer move's hash is {0}", hashint);
            return hashint;
        }
        public void Showthemove()
        {
            Console.WriteLine("the computer has chosen {0}", move[comp]);
        }
        public void Showthekey()
        {
            Console.WriteLine("the hash key is {0}", key);
        }
    };
    public  class User : ComputerChoice
    {

        protected int user;
        protected bool t;
        public User() { user = 0;t = false; }        
        public void UserChoice()
        {
            Console.WriteLine("Choose your move ");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(i + " - " + move[i]);
            }
            user = Convert.ToInt32(Console.ReadLine());
            if(user>=n)
            {
                Exceptions Obj = new Exceptions("The number you've entered is out of range, try again");
                throw Obj;
            }
        }
        public void Compare()
        {
            if (comp == user)
            {
                Exceptions Obj = new Exceptions(move[comp] + " vs " + move[user] + "= try again");
                throw Obj;
            }
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (comp == j && user == i) { Console.WriteLine(move[comp] + " crushes " + move[user]); t = false; }
                        if (comp == i && user == j) { Console.WriteLine(move[user] + " crushes " + move[comp]); t = true; }
                    }
                    else
                    {
                        if (comp == j && user == i) { Console.WriteLine(move[user] + " crushes " + move[comp]); t = true; }
                        if (comp == i && user == j) { Console.WriteLine(move[comp] + " crushes " + move[user]); t = false; }
                    }
                }
            }
            if (t == true) Console.WriteLine(" You've won!!!");
            else if (t == false) Console.WriteLine("You've lost!!!");

        }

    };
    public class Exceptions: Exception
    {
        private string Message;
        public Exceptions(string a) { Message = a; }
        public void Showthemessage() { Console.WriteLine(Message); }
        
    };
    class Program 
    {
        static void Main(string[] args)
        {
            try
            { 
            User A = new User();
            A.Computermove();
            A.GettheKey();
            A.GettheHash();
            A.UserChoice();
            A.Showthekey();
            A.Showthemove();
            A.Compare();
            Console.ReadLine();
            }
        catch (Exceptions obj )
            {
                obj.Showthemessage();
                Console.ReadLine();
            }
            
        }

    }
}
