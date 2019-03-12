using NUnit.Framework;

namespace Methods.Tests
{
    [TestFixture]
    public class Tests
    {
        private FileSystemVisitor _visitor;

        [SetUp]
        public void Setup()
        {
            _visitor = new FileSystemVisitor("D:\\test_folder",f => f.Name.Equals("f2"));
        }

        [Test]
        public void GetAllItems()
        {
            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(12));
        }

        [Test]
        public void StopAfterFindFile()
        {
            _visitor.FileFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 10)
                {
                    args.ActionType = ActionType.StopSearch;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void FindAllItemsWhenRequirementIsNotCorrect()
        {
            _visitor.FileFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 100)
                {
                    args.ActionType = ActionType.StopSearch;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(12));
        }

        [Test]
        public void SkipFileIfRequirementIsCorrect()
        {
            _visitor.FileFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 10)
                {
                    args.ActionType = ActionType.SkipElement;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(11));
        }

        [Test]
        public void StopAfterFindFolder()
        {
            _visitor.DirectoryFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 4)
                {
                    args.ActionType = ActionType.StopSearch;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void SkipFolderIfRequirementIsCorrect()
        {
            _visitor.DirectoryFinded += (sender, args) =>
            {

                if (args.FindedItem.Name.Length == 2)
                {              
                    args.ActionType = ActionType.SkipElement;
                }

                int count = 0;
                foreach (var item in _visitor.GetAllItems())
                {
                    count++;
                }

                Assert.That(count, Is.EqualTo(8));
            };
        }

        [Test]
        public void SkipFilteredFileIfRequirementIsCorrect()
        {
            _visitor.FilteredFileFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 2)
                {
                    args.ActionType = ActionType.SkipElement;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(11));
        }

        [Test]
        public void StopSearchIfFoundFilteredFileAndRequirementIsCorrect()
        {
            _visitor.FilteredFileFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 2)
                {
                    args.ActionType = ActionType.StopSearch;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(9));
        }

        [Test]
        public void SkipFilteredDirectoryIfRequirementIsCorrect()
        {
            _visitor.FilteredDirectoryFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 2)
                {
                    args.ActionType = ActionType.SkipElement;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(9));
        }

        [Test]
        public void StopSearchIfFiltered()
        {
            _visitor.FilteredDirectoryFinded += (sender, args) =>
            {
                if (args.FindedItem.Name.Length == 2)
                {
                    args.ActionType = ActionType.StopSearch;
                }
            };

            int count = 0;
            foreach (var item in _visitor.GetAllItems())
            {
                count++;
            }

            Assert.That(count, Is.EqualTo(4));
        }
    }
}
