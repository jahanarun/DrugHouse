/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Windows;
using DrugHouse.ViewModel.Interfaces;
namespace DrugHouse.View.Services
{
    public class MessageServiceBoxService : IMessageService
    {

        private static readonly MessageServiceBoxService InstanceValue = new MessageServiceBoxService();
        public static MessageServiceBoxService Instance
        {
            get
            { return InstanceValue; }
        }
            
        #region IMessageService
        public MessageResult UserSelection { get; set; }
        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        public MessageResult YesNo(string msg, string caption = "Select Option")
        {
            var result = MessageBox.Show(msg, caption, MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes ? MessageResult.Yes : MessageResult.No;
        }

        public MessageResult YesNoCancel(string msg, string caption = "Select Option")
        {
            var result = MessageBox.Show(msg, caption, MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return MessageResult.Yes;
                case MessageBoxResult.No:
                    return MessageResult.No;
                default:
                    return MessageResult.Cancel;
            }
        }

        public MessageResult OkCancel(string msg, string caption = "Select Option")
        {
            var result = MessageBox.Show(msg, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            return result == MessageBoxResult.OK ? MessageResult.Ok : MessageResult.Cancel;
        }

        public void Ok(string msg, string caption)
        {
            MessageBox.Show(msg, caption, MessageBoxButton.OK,MessageBoxImage.Information);
        }

        public void Error(string msg, string caption)
        {
            MessageBox.Show(msg, caption, MessageBoxButton.OK,MessageBoxImage.Error);
        }

        #endregion

    }
}
