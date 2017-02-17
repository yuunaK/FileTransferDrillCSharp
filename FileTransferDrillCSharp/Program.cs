// Title: File Transfer Program
// Author: Yuuna Kaparti
// Assignment # 27, C# Course
// The Tech Academy, Portland, Oregon

// Program Overview: This program checks files in FolderA 
// for newly created text documents and/or existing documents 
// that have been modified in the last 24 hours.

// Classes used in this program include:
// a. Directory.GetFiles()
// b. Array Methods: Length() and Substring()
// c. DateTime class, DateTime.Now(), DateTime.Compare()
// d. File.GetLastWriteTime()
// e. File, File.Create(), File.Exists(), File.Delete()
// f. File.Move

// Tasks accomplished by program:
// 1. Create an string array and store text files to it.
// 2. Do a comparison: Go through each string element in the array to see 
// if it was created or modified in the last 24 hours.
// 3. If file was created or modified in last 24 hours, move it to FolderB. 



// The following are namespaces used in this script.
using System;
using System.IO;


namespace FileTransferDrillCSharp
{
    class FileMoverProgram
    {
        public static void FileMover()
        {

            // Step 1: Create a string array and assign values to it immediately.
            // Use Directory.GetFiles() method.
            // Directory.GetFiles(@"directorypath", "search term using *")
            // The use of the ampersand "@" is required in order to write the file
            // path with only one backward slash:
            // without @: "c:\\FolderA"
            // with @: @"c:\FolderA"
            // The search operator * could have been substituted with ?.
            // Difference between ? and * lies in range of files that are returned.
            // ? returns files that match specified pattern.
            // * returns files that contain specified pattern.
            string[] TextFilesinFolderA = Directory.GetFiles(@"c:\FolderA", "*.txt");

            // Step 2: Cycle through String array to check when it was created or modified.
            // For loop syntax: counter variable is declared and initialized.
            // Duration is specified using the length of the array.  Duration has to be
            // less than lenght of the array since array index starts at 0 and length of
            // array will always be one more than the index value of last element in array.
            // Lastly we set the increment.
            for (int i=0; i < TextFilesinFolderA.Length; i++)
                {
                    // We situate the steps of the for-loop inside a try-catch block.
                    // We want to make sure that the process works for each file that
                    // the program looks at and attempts to move.
                    try
                    {
                        // We create a string array to store the names of each file.  
                        // TextFilesinFolderA stores each file as a string with its entire filepath and 
                        // file extenstion information: "c:\FolderA\Text1.txt"
                        // We want to slice this string to just "Text1.txt"
                        // To slice the string, we use the Array method 
                        // Substring(index to begin slice).
                        // To create a string array, we have to 
                        // use the keyword new and specify length of array.
                        // We use the length of our first array to set size for new array.
                        string[] TextFileNames = new string[TextFilesinFolderA.Length];

                        // For each filepath string in our first array, slice it and store 
                        // to our second array.
                        TextFileNames[i] = TextFilesinFolderA[i].Substring(11);


                        // For each file in our first array, use the File.GetLastWriteTime method to 
                        // find out when the file was created or modified.  You have to pass a string
                        // value which includes the file's full file path and file extension information.

                        DateTime TimeFileLastModified = File.GetLastWriteTime(TextFilesinFolderA[i]);

                        // Use the DateTime.Now() method to find out the current date and time.
                        DateTime dateNow = DateTime.Now;

                        // Use the DateTime.Compare() method to find the difference between the 
                        // last write teime and current time.  The value is a whole number.
                        int dateComparisonResult = DateTime.Compare(TimeFileLastModified, dateNow);


                        // This will create a file if it doesn't exist and put file in it.
                        if (!File.Exists(TextFilesinFolderA[i]))
                        {
                            using (FileStream fs = File.Create(TextFilesinFolderA[i])) { }
                        }

                        // This will check if an old version of file exists.  If yes, it will be
                        // deleted.
                        if (File.Exists(@"c:\FolderB\*.txt"))
                        {
                            File.Delete(@"c:\FolderB\*.txt");
                        }


                        // This code compares the comparison between the last file write time and 
                        // current time.  Current moment will be 0; 24 hours prior will be -24.
                        if (dateComparisonResult >= -24 || dateComparisonResult == 0)
                        {
                            // Concatenate strings to create a full file path with file name and 
                            // extension for each file.
                            string filePathtoFolderB = @"c:\FolderB\" + TextFileNames[i];
                            // The arguments for the File.Move() method requires strings that 
                            // provide full filepath, filename, and file extension information.
                            File.Move(TextFilesinFolderA[i], filePathtoFolderB);
                            Console.WriteLine("{0} was moved to {1}.", TextFilesinFolderA[i], filePathtoFolderB);
                        }
                    }
                    // This is the catch block.  Providing this will capture system information
                    // about the exception and print it to the console.
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0} ", e.ToString());
                        // Provide a way for user to exit application.
                        Console.ReadKey(true);
                    }




                }
            
            


         
        

        }


        static void Main(string[] args)
        {
            Console.Write("Check for modified files in FolderA?  (y/n) ");
            string response = Console.ReadLine();
            if (response == "y")
            {
                // Call the FileMoverProgram's FileMover method.
                FileMoverProgram.FileMover();
                // Provides a way for user to exit the application.
                Console.ReadKey(true);
            }
            else
            {
                // Provides a way for user to exit application.
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(true);
            }
        }
    }
}
