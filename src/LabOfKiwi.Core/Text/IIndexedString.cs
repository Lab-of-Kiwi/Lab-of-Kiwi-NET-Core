using LabOfKiwi.Collections;

namespace LabOfKiwi.Text;

public interface IIndexedString : IIndexedCollection<char>
{
    bool NextEquals(string? value);

    new string Peek(int count);

    new string PeekAt(int index, int count);

    new string PeekAhead(int offset, int count);
}
