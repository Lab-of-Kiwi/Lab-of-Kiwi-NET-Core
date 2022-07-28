using LabOfKiwi.Collections;

namespace LabOfKiwi.Text;

public interface ITraverseableString : IIndexedString, ITraverseableCollection<char>
{
    new string Read(int count);
}
