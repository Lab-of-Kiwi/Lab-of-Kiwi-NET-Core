namespace LabOfKiwi.Web.Html.Attributes;

public sealed class BodyEvents : Events, IBodyEvents
{
    internal BodyEvents(AttributeCollection rawAttributes) : base(rawAttributes)
    {
    }

    public string? OnAfterPrint
    {
        get => _rawAttributes["onafterprint"];
        set => _rawAttributes["onafterprint"] = value;
    }

    public string? OnBeforePrint
    {
        get => _rawAttributes["onbeforeprint"];
        set => _rawAttributes["onbeforeprint"] = value;
    }

    public string? OnBeforeUnload
    {
        get => _rawAttributes["onbeforeunload"];
        set => _rawAttributes["onbeforeunload"] = value;
    }

    public string? OnHashChange
    {
        get => _rawAttributes["onhashchange"];
        set => _rawAttributes["onhashchange"] = value;
    }

    public string? OnLanguageChange
    {
        get => _rawAttributes["onlanguagechange"];
        set => _rawAttributes["onlanguagechange"] = value;
    }

    public string? OnMessage
    {
        get => _rawAttributes["onmessage"];
        set => _rawAttributes["onmessage"] = value;
    }

    public string? OnMessageError
    {
        get => _rawAttributes["onmessageerror"];
        set => _rawAttributes["onmessageerror"] = value;
    }

    public string? OnOffline
    {
        get => _rawAttributes["onoffline"];
        set => _rawAttributes["onoffline"] = value;
    }

    public string? OnOnline
    {
        get => _rawAttributes["ononline"];
        set => _rawAttributes["ononline"] = value;
    }

    public string? OnPageHide
    {
        get => _rawAttributes["onpagehide"];
        set => _rawAttributes["onpagehide"] = value;
    }

    public string? OnPageShow
    {
        get => _rawAttributes["onpageshow"];
        set => _rawAttributes["onpageshow"] = value;
    }

    public string? OnPopState
    {
        get => _rawAttributes["onpopstate"];
        set => _rawAttributes["onpopstate"] = value;
    }

    public string? OnRejectionHandled
    {
        get => _rawAttributes["onrejectionhandled"];
        set => _rawAttributes["onrejectionhandled"] = value;
    }

    public string? OnStorage
    {
        get => _rawAttributes["onstorage"];
        set => _rawAttributes["onstorage"] = value;
    }

    public string? OnUnhandledRejection
    {
        get => _rawAttributes["onunhandledrejection"];
        set => _rawAttributes["onunhandledrejection"] = value;
    }

    public string? OnUnload
    {
        get => _rawAttributes["onunload"];
        set => _rawAttributes["onunload"] = value;
    }
}
