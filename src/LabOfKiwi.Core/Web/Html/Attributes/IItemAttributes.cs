using System;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Attributes;

public interface IItemAttributes
{
    Uri? Id { get; }

    IList<string> Properties { get; }

    IList<string> ReferencedElements { get; }

    bool IsScope { get; }

    IList<string> Types { get; }
}
