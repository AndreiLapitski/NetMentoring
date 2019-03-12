using System;
using System.IO;

namespace Methods
{
    class Program
    {
        static void Main()
        {
            //Without a filter
            //var fileSystemVisitor = new FileSystemVisitor("..\\..\\..\\test_folder");
            //fileSystemVisitor.Start += (sender, args) =>
            //{
            //    Console.WriteLine("Start\n\r");
            //};
            //fileSystemVisitor.Finish += (sender, args) =>
            //{
            //    Console.WriteLine("\n\rFinish");
            //};

            //fileSystemVisitor.FileFinded += (sender, args) =>
            //{
            //    Console.WriteLine("\tFile finded: " + args.FindedItem);
            //};

            //fileSystemVisitor.DirectoryFinded += (sender, args) =>
            //{
            //    Console.WriteLine("Directory finded: " + args.FindedItem);
            //};


            //With a filter
            var fileSystemVisitor = new FileSystemVisitor("..\\..\\..\\test_folder", f => f.Name.Equals("f2"));

            fileSystemVisitor.Start += (sender, args) =>
            {
                Console.WriteLine("Start\n\r");
            };
            fileSystemVisitor.Finish += (sender, args) =>
            {
                Console.WriteLine("\n\rFinish");
            };

            fileSystemVisitor.FileFinded += (sender, args) =>
            {
                //Just show
                Console.WriteLine("\tFile finded: " + args.FindedItem);

                //Skip item with name length = 10 (file1.docx)
                //if (args.FindedItem.Name.Length != 10)
                //{
                //    Console.WriteLine("\tFile finded: " + args.FindedItem);
                //}
                //else
                //{
                //    args.ActionType = ActionType.SkipElement;
                //}

                //Stop search if file1.docx was found
                //Console.WriteLine("\tFile finded: " + args.FindedItem);
                //if (args.FindedItem.Name.Length == 10)
                //{
                //    args.ActionType = ActionType.StopSearch;
                //}
            };

            fileSystemVisitor.DirectoryFinded += (sender, args) =>
            {
                //Just show
                Console.WriteLine("Directory finded: " + args.FindedItem);

                //Skip folder (with nested files) if name langth = 2
                //if (args.FindedItem.Name.Length != 2)
                //{
                //    Console.WriteLine("Directory finded: " + args.FindedItem);
                //}
                //else
                //{
                //    args.ActionType = ActionType.SkipElement;
                //}

                //Stop search if name length = 2
                //Console.WriteLine("Directory finded: " + args.FindedItem);
                //if (args.FindedItem.Name.Length == 4)
                //{
                //    args.ActionType = ActionType.StopSearch;
                //}
            };

            fileSystemVisitor.FilteredFileFinded += (sender, args) =>
            {
                //Just show
                Console.WriteLine("\tFiltered file finded: " + args.FindedItem);

                //Stop search if name length = 2
                //Console.WriteLine("\tFiltered file finded: " + args.FindedItem);
                //if (args.FindedItem.Name.Length == 2)
                //{
                //    args.ActionType = ActionType.StopSearch;
                //}

                //Skip item if name length = 2
                //if (args.FindedItem.Name.Length == 2)
                //{
                //    args.ActionType = ActionType.SkipElement;
                //}
            };

            fileSystemVisitor.FilteredDirectoryFinded += (sender, args) =>
            {
                //Just show
                Console.WriteLine("\rFiltered directory finded: " + args.FindedItem);

                //Stop search if name length = 2
                //Console.WriteLine("\rFiltered directory finded: " + args.FindedItem);
                //if (args.FindedItem.Name.Length == 2)
                //{
                //    args.ActionType = ActionType.StopSearch;
                //}

                //Skip folder (with nested files) if name length = 2
                //if (args.FindedItem.Name.Length == 2)
                //{
                //    args.ActionType = ActionType.SkipElement;
                //}
            };

            int count = 0;
            foreach (FileSystemInfo item in fileSystemVisitor.GetAllItems())
            {
                count++;
            }

            Console.WriteLine("Total count: " + count);
            Console.ReadKey();

        }
    }
}
