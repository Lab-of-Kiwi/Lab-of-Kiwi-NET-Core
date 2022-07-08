using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LabOfKiwi.Web.Html.Compliance
{
    public sealed class DocumentComplianceReport : IReadOnlyList<ElementComplianceReport>
    {
        private readonly List<ElementComplianceReport> _elementReports;

        internal DocumentComplianceReport(Document document)
        {
            _elementReports = new List<ElementComplianceReport>();
            Document = document;
        }

        public ElementComplianceReport this[int index] => _elementReports[index];

        public Document Document { get; }

        public int Count => _elementReports.Count;

        public IEnumerator<ElementComplianceReport> GetEnumerator() => _elementReports.GetEnumerator();

        internal void Generate()
        {
            int index = 0;

            foreach (var element in Document.AllElements)
            {
                var eleReport = new ElementComplianceReport(index++, element);
                _elementReports.Add(eleReport);

                try
                {
                    element.Report(eleReport);
                }
                catch { }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_elementReports).GetEnumerator();

        public int TotalMessageCount => _elementReports.Select(r => r.TotalMessageCount).Sum();

        public int WarningAndErrorMessageCount => _elementReports.Select(r => r.WarningAndErrorMessageCount).Sum();

        public int ErrorMessageCount => _elementReports.Select(r => r.ErrorMessageCount).Sum();

        public bool HasWarningsOrErrors => _elementReports.Any(r => r.HasWarningsOrErrors);

        public bool HasErrors => _elementReports.Any(r => r.HasErrors);

        public void PrintToConsole()
        {
            var origColor = Console.ForegroundColor;

            foreach (var eleReport in _elementReports)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Element: {eleReport.Index}, Type: {eleReport.Element.TagName}");

                foreach (var msg in eleReport.AllMessages)
                {
                    Console.ForegroundColor = msg.Type switch
                    {
                        ElementComplianceReport.MessageType.Warning => ConsoleColor.Yellow,
                        ElementComplianceReport.MessageType.Error => ConsoleColor.Red,
                        _ => ConsoleColor.DarkGray,
                    };

                    Console.WriteLine("    " + msg.Message);
                }
            }

            Console.ForegroundColor = origColor;
        }
    }

    public sealed class ElementComplianceReport
    {
        private readonly List<ComplianceReportMessage> _messages;

        internal ElementComplianceReport(int index, Element element)
        {
            _messages = new List<ComplianceReportMessage>();
            Index = index;
            Element = element;
        }

        public int Index { get; }

        public Element Element { get; }

        public int TotalMessageCount => _messages.Count;

        public int WarningAndErrorMessageCount => _messages.Where(m => m.Type != MessageType.Info).Count();

        public int ErrorMessageCount => _messages.Where(m => m.Type == MessageType.Error).Count();

        public IReadOnlyList<ComplianceReportMessage> AllMessages => _messages.AsReadOnly();

        public IReadOnlyList<ComplianceReportMessage> InfoMessages => _messages
            .Where(m => m.Type == MessageType.Info)
            .ToList().AsReadOnly();

        public IReadOnlyList<ComplianceReportMessage> WarningMessages => _messages
            .Where(m => m.Type == MessageType.Warning)
            .ToList().AsReadOnly();

        public IReadOnlyList<ComplianceReportMessage> ErrorMessages => _messages
            .Where(m => m.Type == MessageType.Error)
            .ToList().AsReadOnly();

        public IReadOnlyList<ComplianceReportMessage> WarningAndErrorMessages => _messages
            .Where(m => m.Type != MessageType.Info)
            .ToList().AsReadOnly();

        public bool HasWarningsOrErrors => _messages.Where(m => m.Type != MessageType.Info).Any();

        public bool HasErrors => _messages.Where(m => m.Type == MessageType.Warning).Any();

        internal ElementComplianceReport AddInfo(string message)
            => AddMessage(MessageType.Info, message);

        internal ElementComplianceReport AddWarning(string message)
            => AddMessage(MessageType.Warning, message);

        internal ElementComplianceReport AddError(string message)
            => AddMessage(MessageType.Error, message);

        private ElementComplianceReport AddMessage(MessageType type, string message)
        {
            _messages.Add(new(type, message));
            return this;
        }

        public record ComplianceReportMessage(MessageType Type, string Message)
        {
        }

        public enum MessageType : byte
        {
            Info,
            Warning,
            Error
        }
    }
}
