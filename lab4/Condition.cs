namespace lab4;

internal delegate bool ConditionDelegate(IComparable value);

internal class Condition
{
    private string Attribute { get; }
    private readonly int _attributeNum;
    private readonly ConditionDelegate _function;

    public Condition(TableWorker table, string attribute, ConditionDelegate conditionFunction)
    {
        Attribute = attribute;
        _function = conditionFunction;
        _attributeNum = Array.IndexOf(table.Attributes, Attribute);
    }

    public bool Satisfies(Element[] elements)
    {
        return _function((IComparable)elements[_attributeNum].Value);
    }
}