using System;

namespace DrugHouse.Model.Exceptions
{
    class ReportTemplateNotFoundException : Exception
    {
        private readonly string MessageValue;
        public override string Message
        {
            get { return MessageValue; }
        }

        public ReportTemplateNotFoundException(string path)
        {
            MessageValue = "Template file missing - " + path;
        }
    }
}
