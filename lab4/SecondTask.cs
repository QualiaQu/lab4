namespace lab4;

public static class SecondTask
{
    internal static void Demonstration()
    {
        string? nameFile = null;
        string? attributeName = null;
        string ascendingString = "1";
        string sorterType = "2";
        int time = Ui.AskUser(ref nameFile, ref attributeName, ref ascendingString, ref sorterType);
        TableWorker inputTable = new TableWorker(nameFile + ".csv");
        string outputFile = nameFile + "Sorted.csv";
        bool ascending = int.Parse(ascendingString) == 1;
        SortType sortType = (SortType)(int.Parse(sorterType) - 1);
        
        Condition? condition = Ui.AskCondition(inputTable);
        
        if (condition != null)
        {
            TableWorker.GetFilteredTable(inputTable, "temp.csv", condition);
            inputTable = new TableWorker("temp" + ".csv");
        }
        inputTable.GetSortedTable(outputFile, ascending, attributeName, sortType, time);
    }
}