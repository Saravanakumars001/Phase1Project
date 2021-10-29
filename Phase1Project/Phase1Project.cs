using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Schema;

namespace Phase1Project
{


    interface IManageTeacherData
    {
        void Adddetails();
        void Fetchdetails();
        void Updatedetails();
    }
    public class ManageTeacherData : IManageTeacherData
    {
        protected int iopt;
        private FileStream fsFileStream;
        private StreamWriter stmWriter;
        private StreamReader stmReader;
        private long id;
        private string name;
        private string classname;
        private string section;
        string data;
        string[] temp = null;
        bool res = false;
        private const string filepath = "C:\\Saravana\\TeacherDetails.txt";


        public ManageTeacherData()
        {
            if (!File.Exists(filepath))
            {
                FileStream fsFileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                stmWriter = new StreamWriter(fsFileStream);
                stmWriter.WriteLine("===========================================");
                stmWriter.WriteLine("ID,Name,ClassName,Section");
                stmWriter.WriteLine("===========================================");
                stmWriter.Close();
                fsFileStream.Close();
            }

        }
        public void ManageTeacherDetails()
        {
            do
            {
                Console.WriteLine("***************************Welcome to your Rainbow School******************************");
                Console.WriteLine("1. Add details");
                Console.WriteLine("2. Fetch details");
                Console.WriteLine("3. Update details");
                Console.WriteLine("4. Exit");
                Console.Write("Please select options from Menu : ");
                iopt = int.Parse(Console.ReadLine());
                switch (iopt)
                {
                    case 1:
                        Adddetails();
                        break;
                    case 2:
                        Fetchdetails();
                        break;
                    case 3:
                        Updatedetails();
                        break;
                    case 4:
                        Console.WriteLine("Thank you for using Raibow School System.");
                        break;
                }
            } while (iopt != 4);
        }
        public void Adddetails()
        {
            fsFileStream = new FileStream(filepath, FileMode.Append,FileAccess.Write);
            stmWriter = new StreamWriter(fsFileStream);

            Console.Write("Enter id : ");
            id = int.Parse(Console.ReadLine());
            Console.Write("Enter Name : ");
            name = Console.ReadLine();
            Console.Write("Enter Classname : ");
            classname = Console.ReadLine();
            Console.Write("Enter Section : ");
            section = Console.ReadLine();

            stmWriter.WriteLine($"{id},{name},{classname},{section}");

            stmWriter.Close();
            fsFileStream.Close();
        }

        public void Fetchdetails()
        {
            Console.Write("Enter id to Fetch : ");
            id = int.Parse(Console.ReadLine());

            fsFileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            stmReader = new StreamReader(fsFileStream);

            List<string> lines = new List<string>();
            while (!stmReader.EndOfStream)
            {
                lines.Add(stmReader.ReadLine());
            }

            for (int iline = 3; iline < lines.Count; iline++)
            {
                temp = lines[iline].Split(",");
                if (id == int.Parse(temp[0]))
                {
                    res = true;
                    break;
                }

            }

            if (res)
            {
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@Fetched details@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                Console.WriteLine("Teacher Id : " + temp[0]);
                Console.WriteLine("Teacher Name : " + temp[1]);
                Console.WriteLine("Teacher ClassName : " + temp[2]);
                Console.WriteLine("Teacher Section : " + temp[3]);
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            }
            else
            {
                Console.WriteLine("No matching records found");
            }
            stmReader.Close();
            fsFileStream.Close();

       }

        public void Updatedetails()
        {

            Console.Write("Enter id to update : ");
            id = int.Parse(Console.ReadLine());
            

            fsFileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            stmReader = new StreamReader(fsFileStream);
            
            List<string> lines = new List<string>();
            while (!stmReader.EndOfStream)
            {
                lines.Add(stmReader.ReadLine());
            }

            for (int iline = 3; iline < lines.Count; iline++)
            {
                temp = lines[iline].Split(",");
                if (id == int.Parse(temp[0]))
                {
                    //Console.Write("Enter id : ");
                    //id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name : ");
                    name = Console.ReadLine();
                    Console.Write("Enter Classname : ");
                    classname = Console.ReadLine();
                    Console.Write("Enter Section : ");
                    section = Console.ReadLine();
                    string newString = id + "," + name + "," + classname + "," + section;
                    lines[iline] = newString;
                    res = true;
                    break;
                }
               
            }
            stmReader.Close();

            if (res)
            {
                fsFileStream = new FileStream(filepath, FileMode.Open, FileAccess.Write);
                stmWriter = new StreamWriter(fsFileStream);
                for (int mline = 0; mline < lines.Count; mline++)
                {
                    stmWriter.WriteLine(lines[mline]);
                }
            }
            else
            {
                Console.WriteLine("No matching records found");
            }
            stmWriter.Close();
            fsFileStream.Close();
        }

    }

    class Phase1Project
    {
        static void Main(string[] args)
        {
            ManageTeacherData teacherobj = new ManageTeacherData();
            teacherobj.ManageTeacherDetails();
        }
    }
}
