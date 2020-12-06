﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace HexToString.Service
{
    public interface IMessageService
    {
        void ShowDialog(string message);
        MessageBoxResult Question(string message);
    }
}
