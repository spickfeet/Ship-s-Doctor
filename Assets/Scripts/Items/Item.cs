using UnityEngine.Events;

public abstract class Item
{
    private int _count;
    public int Count
    {
        get { return _count; }
        set
        {
            _count = value;
        }
    }

    protected string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
        }
    }

    protected ItemType _type;
    public ItemType Type => _type;

    public UnityAction<int> CountChanged;
}
