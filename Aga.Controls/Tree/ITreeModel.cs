namespace Aga.Controls.Tree
{
    using System.Collections;

    public interface ITreeModel
    {
        IEnumerable GetChildren(object parent);

        bool HasChildren(object parent);
    }
}
