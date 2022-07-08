namespace LabOfKiwi.Web.Html.Attributes;

public class Events : IEvents
{
    protected readonly AttributeCollection _rawAttributes;

    internal Events(AttributeCollection rawAttributes)
    {
        _rawAttributes = rawAttributes;
    }

    public string? OnAuxClick
    {
        get => _rawAttributes["onauxclick"];
        set => _rawAttributes["onauxclick"] = value;
    }

    public string? OnBeforeMatch
    {
        get => _rawAttributes["onbeforematch"];
        set => _rawAttributes["onbeforematch"] = value;
    }

    public string? OnBlur
    {
        get => _rawAttributes["onblur"];
        set => _rawAttributes["onblur"] = value;
    }

    public string? OnCancel
    {
        get => _rawAttributes["oncancel"];
        set => _rawAttributes["oncancel"] = value;
    }

    public string? OnCanPlay
    {
        get => _rawAttributes["oncanplay"];
        set => _rawAttributes["oncanplay"] = value;
    }

    public string? OnCanPlaythrough
    {
        get => _rawAttributes["oncanplaythrough"];
        set => _rawAttributes["oncanplaythrough"] = value;
    }

    public string? OnChange
    {
        get => _rawAttributes["onchange"];
        set => _rawAttributes["onchange"] = value;
    }

    public string? OnClick
    {
        get => _rawAttributes["onclick"];
        set => _rawAttributes["onclick"] = value;
    }

    public string? OnClose
    {
        get => _rawAttributes["onclose"];
        set => _rawAttributes["onclose"] = value;
    }

    public string? OnContextLost
    {
        get => _rawAttributes["oncontextlost"];
        set => _rawAttributes["oncontextlost"] = value;
    }

    public string? OnContextMenu
    {
        get => _rawAttributes["oncontextmenu"];
        set => _rawAttributes["oncontextmenu"] = value;
    }

    public string? OnContextRestored
    {
        get => _rawAttributes["oncontextrestored"];
        set => _rawAttributes["oncontextrestored"] = value;
    }

    public string? OnCopy
    {
        get => _rawAttributes["oncopy"];
        set => _rawAttributes["oncopy"] = value;
    }

    public string? OnCueChange
    {
        get => _rawAttributes["oncuechange"];
        set => _rawAttributes["oncuechange"] = value;
    }

    public string? OnCut
    {
        get => _rawAttributes["oncut"];
        set => _rawAttributes["oncut"] = value;
    }

    public string? OnDblClick
    {
        get => _rawAttributes["ondblclick"];
        set => _rawAttributes["ondblclick"] = value;
    }

    public string? OnDrag
    {
        get => _rawAttributes["ondrag"];
        set => _rawAttributes["ondrag"] = value;
    }

    public string? OnDragEnd
    {
        get => _rawAttributes["ondragend"];
        set => _rawAttributes["ondragend"] = value;
    }

    public string? OnDragEnter
    {
        get => _rawAttributes["ondragenter"];
        set => _rawAttributes["ondragenter"] = value;
    }

    public string? OnDragLeave
    {
        get => _rawAttributes["ondragleave"];
        set => _rawAttributes["ondragleave"] = value;
    }

    public string? OnDragOver
    {
        get => _rawAttributes["ondragover"];
        set => _rawAttributes["ondragover"] = value;
    }

    public string? OnDragStart
    {
        get => _rawAttributes["ondragstart"];
        set => _rawAttributes["ondragstart"] = value;
    }

    public string? OnDrop
    {
        get => _rawAttributes["ondrop"];
        set => _rawAttributes["ondrop"] = value;
    }

    public string? OnDurationChange
    {
        get => _rawAttributes["ondurationchange"];
        set => _rawAttributes["ondurationchange"] = value;
    }

    public string? OnEmptied
    {
        get => _rawAttributes["onemptied"];
        set => _rawAttributes["onemptied"] = value;
    }

    public string? OnEnded
    {
        get => _rawAttributes["onended"];
        set => _rawAttributes["onended"] = value;
    }

    public string? OnError
    {
        get => _rawAttributes["onerror"];
        set => _rawAttributes["onerror"] = value;
    }

    public string? OnFocus
    {
        get => _rawAttributes["onfocus"];
        set => _rawAttributes["onfocus"] = value;
    }

    public string? OnFormData
    {
        get => _rawAttributes["onformdata"];
        set => _rawAttributes["onformdata"] = value;
    }

    public string? OnInput
    {
        get => _rawAttributes["oninput"];
        set => _rawAttributes["oninput"] = value;
    }

    public string? OnInvalid
    {
        get => _rawAttributes["oninvalid"];
        set => _rawAttributes["oninvalid"] = value;
    }

    public string? OnKeyDown
    {
        get => _rawAttributes["onkeydown"];
        set => _rawAttributes["onkeydown"] = value;
    }

    public string? OnKeyPress
    {
        get => _rawAttributes["onkeypress"];
        set => _rawAttributes["onkeypress"] = value;
    }

    public string? OnKeyUp
    {
        get => _rawAttributes["onkeyup"];
        set => _rawAttributes["onkeyup"] = value;
    }

    public string? OnLoad
    {
        get => _rawAttributes["onload"];
        set => _rawAttributes["onload"] = value;
    }

    public string? OnLoadedData
    {
        get => _rawAttributes["onloadeddata"];
        set => _rawAttributes["onloadeddata"] = value;
    }

    public string? OnLoadedMetadata
    {
        get => _rawAttributes["onloadedmetadata"];
        set => _rawAttributes["onloadedmetadata"] = value;
    }

    public string? OnLoadStart
    {
        get => _rawAttributes["onloadstart"];
        set => _rawAttributes["onloadstart"] = value;
    }

    public string? OnMouseDown
    {
        get => _rawAttributes["onmousedown"];
        set => _rawAttributes["onmousedown"] = value;
    }

    public string? OnMouseEnter
    {
        get => _rawAttributes["onmouseenter"];
        set => _rawAttributes["onmouseenter"] = value;
    }

    public string? OnMouseLeave
    {
        get => _rawAttributes["onmouseleave"];
        set => _rawAttributes["onmouseleave"] = value;
    }

    public string? OnMouseMove
    {
        get => _rawAttributes["onmousemove"];
        set => _rawAttributes["onmousemove"] = value;
    }

    public string? OnMouseOut
    {
        get => _rawAttributes["onmouseout"];
        set => _rawAttributes["onmouseout"] = value;
    }

    public string? OnMouseOver
    {
        get => _rawAttributes["onmouseover"];
        set => _rawAttributes["onmouseover"] = value;
    }

    public string? OnMouseUp
    {
        get => _rawAttributes["onmouseup"];
        set => _rawAttributes["onmouseup"] = value;
    }

    public string? OnPaste
    {
        get => _rawAttributes["onpaste"];
        set => _rawAttributes["onpaste"] = value;
    }

    public string? OnPause
    {
        get => _rawAttributes["onpause"];
        set => _rawAttributes["onpause"] = value;
    }

    public string? OnPlay
    {
        get => _rawAttributes["onplay"];
        set => _rawAttributes["onplay"] = value;
    }

    public string? OnPlaying
    {
        get => _rawAttributes["onplaying"];
        set => _rawAttributes["onplaying"] = value;
    }

    public string? OnProgress
    {
        get => _rawAttributes["onprogress"];
        set => _rawAttributes["onprogress"] = value;
    }

    public string? OnRateChange
    {
        get => _rawAttributes["onratechange"];
        set => _rawAttributes["onratechange"] = value;
    }

    public string? OnReset
    {
        get => _rawAttributes["onreset"];
        set => _rawAttributes["onreset"] = value;
    }

    public string? OnResize
    {
        get => _rawAttributes["onresize"];
        set => _rawAttributes["onresize"] = value;
    }

    public string? OnScroll
    {
        get => _rawAttributes["onscroll"];
        set => _rawAttributes["onscroll"] = value;
    }

    public string? OnSecurityPolicyViolation
    {
        get => _rawAttributes["onsecuritypolicyviolation"];
        set => _rawAttributes["onsecuritypolicyviolation"] = value;
    }

    public string? OnSeeked
    {
        get => _rawAttributes["onseeked"];
        set => _rawAttributes["onseeked"] = value;
    }

    public string? OnSeeking
    {
        get => _rawAttributes["onseeking"];
        set => _rawAttributes["onseeking"] = value;
    }

    public string? OnSelect
    {
        get => _rawAttributes["onselect"];
        set => _rawAttributes["onselect"] = value;
    }

    public string? OnSlotChange
    {
        get => _rawAttributes["onslotchange"];
        set => _rawAttributes["onslotchange"] = value;
    }

    public string? OnStalled
    {
        get => _rawAttributes["onstalled"];
        set => _rawAttributes["onstalled"] = value;
    }

    public string? OnSubmit
    {
        get => _rawAttributes["onsubmit"];
        set => _rawAttributes["onsubmit"] = value;
    }

    public string? OnSuspend
    {
        get => _rawAttributes["onsuspend"];
        set => _rawAttributes["onsuspend"] = value;
    }

    public string? OnTimeUpdate
    {
        get => _rawAttributes["ontimeupdate"];
        set => _rawAttributes["ontimeupdate"] = value;
    }

    public string? OnToggle
    {
        get => _rawAttributes["ontoggle"];
        set => _rawAttributes["ontoggle"] = value;
    }

    public string? OnVolumeChange
    {
        get => _rawAttributes["onvolumechange"];
        set => _rawAttributes["onvolumechange"] = value;
    }

    public string? OnWaiting
    {
        get => _rawAttributes["onwaiting"];
        set => _rawAttributes["onwaiting"] = value;
    }

    public string? OnWheel
    {
        get => _rawAttributes["onwheel"];
        set => _rawAttributes["onwheel"] = value;
    }
}
