using LabOfKiwi.Web.Html.Compliance;
using LabOfKiwi.Web.Html.Tags;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LabOfKiwi.Web.Html;

public class Document : Node
{
    private readonly Node[] _childNodes;

    public Document(DocumentTypeOption? docType = DocumentTypeOption.Html5)
    {
        HTML = new() { ParentNode = this };

        if (docType.HasValue)
        {
            DocumentType docTypeNode = new(docType.Value) { ParentNode = this };
            _childNodes = new Node[] { docTypeNode, HTML };
        }
        else
        {
            _childNodes = new Node[] { HTML };
        }
    }

    public HTML HTML { get; }

    public sealed override Node this[int index] => _childNodes[index];

    public sealed override int ChildNodeCount => _childNodes.Length;

    public sealed override IEnumerable<Node> ChildNodes => new ReadOnlyCollection<Node>(_childNodes);

    public sealed override string ToString()
    {
        if (_childNodes.Length == 1)
        {
            return _childNodes[0].ToString();
        }

        Debug.Assert(_childNodes.Length == 2);
        return _childNodes[0].ToString() + _childNodes[1].ToString();
    }

    public DocumentComplianceReport GenerateComplianceReport()
    {
        var report = new DocumentComplianceReport(this);
        report.Generate();
        return report;
    }
}
