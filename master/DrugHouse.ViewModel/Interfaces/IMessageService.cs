/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

namespace DrugHouse.ViewModel.Interfaces
{
    public interface IMessageService
    {
        void ShowMessage(string msg);

        MessageResult YesNo(string msg, string caption);

        MessageResult YesNoCancel(string msg, string caption="");

        MessageResult OkCancel(string msg, string caption);
        void Ok(string msg, string caption);
        void Error(string msg, string caption);

        MessageResult UserSelection { get; set; }
    }
}