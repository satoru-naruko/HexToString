using Prism.Commands;
using Prism.Mvvm;
using System;
using System.IO;

namespace HexToString.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        public DelegateCommand ExecuteCmd { get; }

        public MainWindowViewModel()
        {
            ExecuteCmd = new DelegateCommand(ExecuteHexToStr);
        }

        public void ExecuteHexToStr() 
        {
            string path = FilePath;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            using (var baseStreasm = br.BaseStream)
            {
                var count = 0;
                while (baseStreasm.Position != baseStreasm.Length)
                {
                    var byteData = br.ReadByte();
                    Console.Write("0x{0:x2}", byteData);
                    Console.Write(",");
                    count++;
                    if (count == 16)
                    {
                        count = 0;
                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}
