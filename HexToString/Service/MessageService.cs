using System.Windows;

namespace HexToString.Service
{
    public sealed class MessageService : IMessageService
    {
        public MessageBoxResult Question(string message)
        {
            return MessageBox.Show(
                message,
                "問い合わせ",
                MessageBoxButton.OKCancel,
                 MessageBoxImage.Question);
        }

        public void ShowDialog(string message)
        {
            MessageBox.Show(message);
        }
    }
}
