﻿using Prism.Commands;
using Prism.Mvvm;
using HexToString.Service;
using System.IO;
using System.Text;
using System.Windows;

namespace HexToString.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IMessageService _messageService;

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

        private string _outHexStr;
        public string OutHexStr
        {
            get { return _outHexStr; }
            set { SetProperty(ref _outHexStr, value); }
        }

        public DelegateCommand ExecuteCmd { get; }
        public DelegateCommand CopyToClipboardCmd { get; }

        public MainWindowViewModel() : this( new MessageService())
        {
        }

        public MainWindowViewModel(IMessageService messageService)
        {
            _messageService = messageService;

            ExecuteCmd = new DelegateCommand(ExecuteHexToStr);
            CopyToClipboardCmd = new DelegateCommand(CopyToClipboardExecute);
        }

        public void ExecuteHexToStr() 
        {
            string path = FilePath;
            Encoding enc = Encoding.GetEncoding("utf-8");

            if (path == null)
            {
                _messageService.ShowDialog("ファイルパスを入力してください");
                return;
            }


            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                using (var writer = new StreamWriter(@"test.txt", false /*append*/, enc))
                using (var baseStreasm = br.BaseStream)
                {
                    var charCount = 0;
                    var newLine = false;
                    var str = string.Empty;
                    while (baseStreasm.Position != baseStreasm.Length)
                    {
                        if (newLine)
                        {
                            str += ", ";
                            str += "\n";
                            newLine = false;
                        }

                        var byteData = br.ReadByte();
                        str += string.Format(@"0x{0:x2}", byteData);
                        charCount++;
                        if (charCount == 16)
                        {
                            charCount = 0;
                            newLine = true;
                        }
                        else
                        {
                            str += ", ";
                        }
                    }
                    OutHexStr = str;
                    writer.Write(str);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                _messageService.ShowDialog("ファイルが見つかりませんでした。");
            }
            catch (System.IO.FileLoadException)
            {
                _messageService.ShowDialog("ファイルが開けませんでした。");
            }
        }

        public void CopyToClipboardExecute()
        {
            Clipboard.SetText(OutHexStr);
            _messageService.ShowDialog("コピーしました。");
        }
    }
}
