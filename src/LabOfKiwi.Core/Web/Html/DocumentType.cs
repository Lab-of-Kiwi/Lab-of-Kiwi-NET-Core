using System;

namespace LabOfKiwi.Web.Html;

public sealed class DocumentType : LeafNode
{
    private readonly string _text;

    public DocumentType(DocumentTypeOption docType)
    {
        _text = docType switch
        {
            DocumentTypeOption.Html5 => "html",
            _ => throw new NotImplementedException()
        };
    }

    public override string ToString() => $"<!DOCTYPE {_text}>";
}
