namespace LabOfKiwi.Web.Html.Attributes;

public interface IBodyEvents : IEvents
{
    string? OnAfterPrint { get; }

    string? OnBeforePrint { get; }

    string? OnBeforeUnload { get; }

    string? OnHashChange { get; }

    string? OnLanguageChange { get; }

    string? OnMessage { get; }

    string? OnMessageError { get; }

    string? OnOffline { get; }

    string? OnOnline { get; }

    string? OnPageHide { get; }

    string? OnPageShow { get; }

    string? OnPopState { get; }

    string? OnRejectionHandled { get; }

    string? OnStorage { get; }

    string? OnUnhandledRejection { get; }

    string? OnUnload { get; }
}
