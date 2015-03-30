/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using DrugHouse.Model.Exceptions;
using DrugHouse.Model.Types;

namespace DrugHouse.Model.Reports
{
    public abstract class DocumentMaker
    {
        private readonly string TemplatePath = string.Empty;
        protected string TemplateName = string.Empty;
        protected object Entity;
        protected readonly Dictionary<string, string> Tags = new Dictionary<string, string>(); 

        protected DocumentMaker()
        {
            TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports\\Templates");
        }

        protected string GetTemplatePath
        {
            get { return Path.Combine(TemplatePath, TemplateName); }
        }
        public abstract void CreateDoc(string path);
        protected abstract void TagsFiller();

        public void SetEntity(ModelBase entity)
        {
            Entity = entity;
            TagsFiller();
        } 
        protected void WriteMemoryStreamToFile(MemoryStream mem, string outputPath)
        {
            using (var fileStream = new FileStream(outputPath, FileMode.Create))
            {
                mem.WriteTo(fileStream);
            }
        }
        protected void PopulateBookmark(BookmarkStart bookmarkStart, string text)
        {
            OpenXmlElement elem = bookmarkStart.NextSibling();

            while (elem != null && !(elem is BookmarkEnd))
            {
                OpenXmlElement nextElem = elem.NextSibling();
                elem.Remove();
                elem = nextElem;
            }

            bookmarkStart.Parent.InsertAfter<Run>(new Run(new Text(text)), bookmarkStart);
        }
        protected MemoryStream GetMemoryStreamFromTemplate()
        {
            var path = GetTemplatePath;
            var memoryStream = new MemoryStream();
            if(!File.Exists(path))
                throw new ReportTemplateNotFoundException(path);
            var templateBytes = File.ReadAllBytes(path);
            memoryStream.Write(templateBytes, 0, templateBytes.Length);
            return memoryStream;
        }
        protected void ApplyFormatToRunElement(Run sdt)
        {
            var runPro = new RunProperties();
            var runFont = new RunFonts() { Ascii = "Cambria(Headings)", HighAnsi = "Cambria(Headings)" };
            var bold = new Bold();
            var text = new Text("TESTING");
            var color = new Color() { Val = "365F91", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "BF" };
            runPro.Append(runFont);
            runPro.Append(bold);
            runPro.Append(color);
            sdt.Append(runPro); 
        }                       
    }
}
