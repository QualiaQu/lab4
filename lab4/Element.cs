namespace lab4;

internal readonly struct Element : IComparable
{
    public object Value { get; }
    private ColumnType Type { get; }
    public Element(string value, ColumnType type)
    {
        Type = type;
        switch (type)
        {
            case ColumnType.Integer:
                Value = int.Parse(value);
                break;
            case ColumnType.String:
                Value = value;
                break;
            default:
                throw new Exception("Тип не реализован");
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is Element otherElement)
        {
            if (Type == otherElement.Type)
            {
                IComparable value1 = (IComparable)Value;
                IComparable value2 = (IComparable)otherElement.Value;
                return value1.CompareTo(value2);
            }
            else
            {
                throw new Exception("Попытка сравнить элементы, хронящие разные типы данных");
            }
        }
        else
        {
            throw new Exception("Нельзя сравнить с другими типами");
        }
    }
}