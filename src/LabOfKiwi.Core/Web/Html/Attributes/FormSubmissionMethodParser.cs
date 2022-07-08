using System;

namespace LabOfKiwi.Web.Html.Attributes;

public class FormSubmissionMethodParser : Parser<FormSubmissionMethod>
{
    protected sealed override FormSubmissionMethod InternalParse(string rawValue) => rawValue switch
    {
        "GET" => FormSubmissionMethod.GET,
        "POST" => FormSubmissionMethod.POST,
        "dialog" => FormSubmissionMethod.Dialog,
        _ => base.InternalParse(rawValue)
    };

    protected sealed override string InternalToString(FormSubmissionMethod value) => value switch
    {
        FormSubmissionMethod.GET => "GET",
        FormSubmissionMethod.POST => "POST",
        FormSubmissionMethod.Dialog => "dialog",
        _ => throw new NotImplementedException()
    };
}
