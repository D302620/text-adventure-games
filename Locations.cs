using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace text_based_adventure
{
    public class Locations
    {
        public void Town()
        {
            bool choiceMade = false;
            Console.WriteLine("test location");
            Console.WriteLine("test dialogue");
            Console.WriteLine("what do you want to do\n1. test1\n2. test2\n3. test3\n4. test4");
            while (choiceMade == false)
            {
                string choice = Console.ReadLine();
                switch (choice) 
                {
                    default:
                        Console.WriteLine(choiceMade);
                        break;
                }
            }
        }
    }
}
