// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
/// <summary>
/// </summary>

class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="length - number of command line args"></param>
    /// <param name="tofind -- set by command line args to string to find"></param>
    /// <param name="path -- set by command line args to be path to search"></param>
    /// <param name="verbose -- set by command line args to pring entire paragraphs"></param>
    /// <param name="nocase -- set by command line args fold all checking to lower case"></param>
    void ReadCommandLineArguments(int length, ref string tofind, ref string path,ref bool verbose, ref bool nocase)
    {
        int i = 1;
        while (i <= length)
        {
            string argument = Environment.GetCommandLineArgs()[i];
            Console.WriteLine(" Argment is: {0}", argument);
            if (argument == "-p" || argument == "-P")
            {
                i = i + 1;
                path = Environment.GetCommandLineArgs()[i];
            }
            else if (argument == "-f" || argument == "-F")
            {
                i = i + 1;
                tofind = Environment.GetCommandLineArgs()[i];
            }
            else if (argument == "-nv" || argument == "-NV")
                verbose = false;
            else if (argument == "-nc" || argument == "-NC")
                nocase = true;
            else
            {
                tofind = Environment.GetCommandLineArgs()[i];
            }
            
            
            i++;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pathname -- set by command line args to be path to search"></param>
    /// <param name="verbose  -- set by command line args to print entire paragraphs"></param>
    /// <param name="nocase -- fold all checking to lower case"></param>
    /// <returns></returns>
    int ReadDocxFile(object pathname, string tofind, bool verbose, bool nocase, in Microsoft.Office.Interop.Word.Application word)
    {
        int returncode = 0;
        object miss = System.Reflection.Missing.Value;
        object confirconversions = false;
        
        object readOnly = true;
        Microsoft.Office.Interop.Word.Document docs;
        try
        {
            docs = word.Documents.Open(ref pathname, ref confirconversions, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
        }
        catch
        {
            //docs.Close();
            return -1;
        }
        Console.WriteLine(" total paragraphs: {0}", docs.Paragraphs.Count);
        string text;
        for (int i = 0; i < docs.Paragraphs.Count; i++)
        {
            text = "";
            text = docs.Paragraphs[i + 1].Range.Text.ToString();
            if (nocase)
            {
                string temptext = text;
                text = temptext.ToLower();
            }
                
            if (text.Contains(tofind))
            {
                Console.WriteLine(" String: [{0} found in paragraph {1}!", tofind, i);
                if (verbose)
                {
                    Console.WriteLine(text);
                    Console.WriteLine("");
                }

            }
        }
            
            
        docs.Close();
        return returncode;
    }
    /// <summary>
    /// Usage: ScanWordFiles {stringtofind} -p startpath -nv {do not print verbose} -f string to find {altername way to specify}
    /// if stringtofind includes quotes they are removed but spaces are retained for example " string " will not find string,
    /// path should be fully qualified e.g. c:\users\documents  Without the endig slash that is added to create file name to open
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        bool verbose = true;
        bool nocase = false;
        string tofind = "diet";
        //string basepath = @"C:\users\glenn.000\Documents";
        Program p = new Program();

        string basepath = Directory.GetCurrentDirectory();
        Console.WriteLine("Hello, World! from {0}", basepath);

        //Console.WriteLine(" number of args {0}", args.Length);

        Console.WriteLine("");

        string name = Environment.GetCommandLineArgs()[0];
        Console.WriteLine($"Hello main, {name}!");
        Console.WriteLine("");

        p.ReadCommandLineArguments(args.Length, ref tofind, ref basepath, ref verbose, ref nocase);
        tofind.Replace("\"", "");
        
        if (nocase)
        {
            string temptext = tofind;
            tofind = temptext.ToLower();
        }
            

        // Get the directory
        DirectoryInfo place = new DirectoryInfo(basepath);

        Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();


        // Using GetFiles() method to get list of all
        // the files present in the Train directory
        FileInfo[] Files = place.GetFiles();

        Console.WriteLine("Scanning directory: {0}", basepath);
        Console.WriteLine();
        Console.WriteLine("Files are:");
        // Display the file names
        
        foreach (FileInfo i in Files)
        {
            if (i.Name.Contains(".docx") ) {
                Console.WriteLine(" ***> Document Found: Name - {0} <***", i.Name);
                string fullpath = basepath + @"\" + i.Name;
                p.ReadDocxFile(fullpath, tofind, verbose, nocase, in word);
                Console.WriteLine("");
            }
            //else
            //{
            //    Console.WriteLine("File Name - {0}", i.Name);
            //}
           
        }
        word.Quit();
    }

    
}
