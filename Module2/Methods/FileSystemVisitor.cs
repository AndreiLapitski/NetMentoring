using System;
using System.Collections.Generic;
using System.IO;
using Methods.EventArguments;

namespace Methods
{
    public class FileSystemVisitor
    {
        private readonly DirectoryInfo _startDirectory;
        private readonly Func<FileSystemInfo, bool> _filter;

        public event EventHandler<StartEventArgs> Start;
        public event EventHandler<FinishEventArgs> Finish;

        public event EventHandler<ItemFindedEventArgs<FileInfo>> FileFinded;
        public event EventHandler<ItemFindedEventArgs<FileInfo>> FilteredFileFinded;
        public event EventHandler<ItemFindedEventArgs<DirectoryInfo>> DirectoryFinded;        
        public event EventHandler<ItemFindedEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        public FileSystemVisitor(string path)
        {
            _startDirectory = new DirectoryInfo(path);
        }

        public FileSystemVisitor(string path, Func<FileSystemInfo, bool> filter) : this(path)
        {           
            _filter = filter;
        }        

        public IEnumerable<FileSystemInfo> GetAllItems()
        {
            OnEvent(Start, new StartEventArgs());

            foreach (FileSystemInfo fileSystemInfo in Bypass(_startDirectory, CurrentAction.ContinueSearch))
            {
                yield return fileSystemInfo;
            }

            OnEvent(Finish, new FinishEventArgs());
        }

        private IEnumerable<FileSystemInfo> Bypass(DirectoryInfo directory, CurrentAction currentAction)
        {
            foreach (FileSystemInfo fileSystemInfo in directory.GetFileSystemInfos())
            {
                if (fileSystemInfo is FileInfo file)
                {
                    currentAction.Action = 
                        CheckFileAction(file, 
                        FileFinded, 
                        FilteredFileFinded);
                    
                    if (currentAction.Action == ActionType.SkipElement)
                    {
                        currentAction.Action = ActionType.ContinueSearch;
                        continue;
                    }

                    yield return file;
                }

                if (fileSystemInfo is DirectoryInfo dir)
                {
                    currentAction.Action = 
                        CheckDirectoryAction(dir, 
                        DirectoryFinded, 
                        FilteredDirectoryFinded);

                    if (currentAction.Action == ActionType.SkipElement)
                    {
                        currentAction.Action = ActionType.ContinueSearch;
                        continue;
                    }

                    if (currentAction.Action == ActionType.ContinueSearch)
                    {
                        yield return dir;

                        foreach (FileSystemInfo innerFile in Bypass(dir, currentAction))
                        {
                            yield return innerFile;
                        }
                    }                     
                }

                if (currentAction.Action == ActionType.StopSearch)
                {
                    yield break;
                }
            }            
        }

        private ActionType CheckFileAction(FileInfo file, 
            EventHandler<ItemFindedEventArgs<FileInfo>> fileFinded, 
            EventHandler<ItemFindedEventArgs<FileInfo>> filteredFileFinded)
        {
            ItemFindedEventArgs<FileInfo> args = new ItemFindedEventArgs<FileInfo>()
            {
                FindedItem = file
            };

            if (_filter != null && _filter(file))
            {
                OnEvent(filteredFileFinded, args);
            }
            else
            {
                OnEvent(fileFinded, args);
            }
            
            return args.ActionType;
        }

        private ActionType CheckDirectoryAction(DirectoryInfo directory, 
            EventHandler<ItemFindedEventArgs<DirectoryInfo>> directoryFinded, 
            EventHandler<ItemFindedEventArgs<DirectoryInfo>> filteredDirectoryFinded)
        {
            ItemFindedEventArgs<DirectoryInfo> args = new ItemFindedEventArgs<DirectoryInfo>()
            {
                FindedItem = directory
            };

            if (_filter != null && _filter(directory))
            {
                OnEvent(filteredDirectoryFinded, args);
            }
            else
            {
                OnEvent(directoryFinded, args);
            }

            return args.ActionType;
        }

        private void OnEvent<T>(EventHandler<T> someEvent, T args)
        {
            someEvent?.Invoke(this, args);
        }

        private class CurrentAction
        {
            public ActionType Action { get; set; }
            public static CurrentAction ContinueSearch
                => new CurrentAction { Action = ActionType.ContinueSearch };
        }
    }
}

