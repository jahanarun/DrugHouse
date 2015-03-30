/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DrugHouse.Model.Types;
using DrugHouse.Shared;

namespace DrugHouse.Model.Reports
{
    public class MedicalCertificateDocumentMaker  : DocumentMaker
    {

        public MedicalCertificateDocumentMaker()
        {
            TemplateName = "Medical Certificate.docx";
        }
        private PatientVisit Visit { get { return (PatientVisit) Entity; } }
        protected override void TagsFiller()
        {            
            Tags.Add("FromDate", Visit.Date.ToString("d"));
            Tags.Add("Dated", DateTime.Now.ToString("d"));
            Tags.Add("Diagnosis", Visit.PrimaryDiagnosis.Name);
            Tags.Add("PatientName", Visit.Patient.Name);
            Tags.Add("DoctorName", ApplicationConstants.DoctorName);
            Tags.Add("Duration", "1 day");
        }
        public override void CreateDoc(string outputPath)
        {
            using (var mem = GetMemoryStreamFromTemplate())
            {
                //WordprocessingDocument.Open(mem, true);
                using (var doc = WordprocessingDocument.Open(mem, true))
                {
                    
                    var body = doc.MainDocumentPart.Document.Body;

                    //mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

                    foreach (var sdt in body.Descendants<SdtElement>().ToList())
                    {
                        var alias = sdt.Descendants<SdtAlias>().FirstOrDefault();

                        if(alias !=null)
                        {
                            if (sdt.ToString() == "DocumentFormat.OpenXml.Wordprocessing.SdtRun")
                            {
                                var xStdRun = (SdtRun) sdt;
                                var xStdContentRun = xStdRun.Descendants<SdtContentRun>().FirstOrDefault();
                                var xRun = xStdContentRun.Descendants<Run>().FirstOrDefault();
                                var xText = xRun.Descendants<Text>().FirstOrDefault();
                                var sdtTitle = alias.Val.Value;
                                string result;
                                Tags.TryGetValue(sdtTitle, out result);
                                if (result == null)
                                    result = string.Empty;
                                result = result.PadLeft(20);
                                result = result.PadRight(20);
                                xText.Text = result;


                            }
                            
                        }
                    }
                    doc.ChangeDocumentType(WordprocessingDocumentType.Document);

                    // Add new text.
                    //var para = body.AppendChild(new Paragraph());
                    //Run run = para.AppendChild(new Run());
                    //run.AppendChild(new Text("Chumman asdfikek"));

                }

                WriteMemoryStreamToFile(mem, outputPath);


            }
        }

    }
   
}
