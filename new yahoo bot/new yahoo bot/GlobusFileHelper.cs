using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Globussoft.File
{
    public static class GlobusFileHelper
    {
        public static String ReadStringFromTextfile(string filepath)
        {
            StreamReader reader = new StreamReader(filepath);
            string fileText=reader.ReadToEnd();
            reader.Close();
            return fileText;
        }

        public static List<string> ReadFiletoStringList(string filepath)
        {
            List<string> list = new List<string>();
            StreamReader reader = new StreamReader(filepath);
            string text="";
            while ((text = reader.ReadLine()) != null)
            {
                list.Add(text);
            }
            reader.Close();
            return list;


        }

        public static void WriteStringToTextfile(string content,string filepath)
        {
           StreamWriter writer = new StreamWriter(filepath);
           writer.Write(content);
           writer.Close();
           
        }

        public static void WriteStringToTextfileNewLine(String content, string filepath)
        {

            StreamWriter writer = new StreamWriter(filepath);

            StringReader reader = new StringReader(content);

            while(reader.ReadLine()!=null)
            { 
                writer.WriteLine(content);
            }           

            writer.Close();
        }
        public static void AppendStringToTextfileNewLine(String content, string filepath)
        {

            StreamWriter writer = new StreamWriter(filepath,true);

            StringReader reader = new StringReader(content);

            string temptext ="";

            while ((temptext=reader.ReadLine()) != null)
            {
                writer.WriteLine(temptext);
            }

            writer.Close();
        }

        public static void WriteListtoTextfile(List<string> list,string filepath)
        {
            StreamWriter writer = new StreamWriter(filepath);          

            foreach(string listitem in list)
            { 
                writer.WriteLine(listitem);
            }           

            writer.Close();
        }

    }
}
