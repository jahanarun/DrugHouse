/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Threading.Tasks;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;

namespace DrugHouse.Model.Reports
{
    public class Report  
        : IDisposable
    {
        public async Task CreateDocumentAsync(DocumentType documentType, ModelBase item)
        {
            await Task.Run(() => CreateDocument(documentType, item));
        }

        public void CreateDocument(DocumentType documentType, ModelBase item)
        {
            var wordDocument = GetDocument(documentType);
            DocumentCreator(wordDocument, item);
        }

        private static DocumentMaker GetDocument(DocumentType documentType)
        {
            DocumentMaker documentMaker = null;

            switch (documentType)
            {
                case DocumentType.MedicalCertificate:
                    documentMaker = new MedicalCertificateDocumentMaker();
                    break;   
            }
            return documentMaker;
        }

        private static void DocumentCreator(DocumentMaker docCreator, ModelBase item)
        {

            docCreator.SetEntity(item);
            var path = GetOutputPath();

            docCreator.CreateDoc(path);

            // Open the created file in default application
            System.Diagnostics.Process.Start(path);
        }

        private static string GetOutputPath()
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Report.docx");
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            return path;

        }

        public void Dispose()
        {
                
        }
    }

}
