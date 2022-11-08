namespace lab4;

    enum ColumnType
    {
        Integer,
        String
    }

    enum SortType
    {
        Direct,
        Natural,
        Multipath
    }
    class TableWorker
    {
        private const char SeparatingSign = ';';
        private readonly string _filePath;
        private int ColumnCount { get; }
        private int RowCount { get; set; }
        public string[] Attributes { get; }
        private ColumnType[] Types { get; }

        public TableWorker(string path)
        {
            _filePath = path;
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                
                ColumnCount = file.ReadLine()!.Split(";").Length;
                RowCount = File.ReadAllLines(path).Count() - 1;
                Attributes = File.ReadLines(path).First().Split(";");
                Types = ParseToType();
                file.Close();
            }
            else
            {
                throw new Exception("Файл не найден");
            }
        }

        private TableWorker(string path, TableWorker cloneableTable, bool cloneData)
        {
            _filePath = path;
            ColumnCount = cloneableTable.ColumnCount;
            Attributes = cloneableTable.Attributes;
            Types = cloneableTable.Types;

            StreamWriter newFile = new StreamWriter(path);
            StreamReader cloneableFile = new StreamReader(cloneableTable._filePath);

            if (cloneData)
                newFile.WriteLine(cloneableFile.ReadLine()); 
            else
            {
                newFile.WriteLine(ParseLine(cloneableFile.ReadLine())[0] + SeparatingSign + "0" + SeparatingSign);
            }
            newFile.WriteLine(cloneableFile.ReadLine());
            newFile.WriteLine(cloneableFile.ReadLine());

            if (cloneData)
            {
                while (!cloneableFile.EndOfStream)
                {
                    newFile.WriteLine(cloneableFile.ReadLine()); 
                }
            }
            newFile.Close();
            cloneableFile.Close();
        }

        static string[] ParseLine(string? line)
        {
            return line!.Split(SeparatingSign);
        }

        ColumnType[] ParseToType()
        {
            ColumnType[] output = new ColumnType[ColumnCount];
            for (int i = 0; i < ColumnCount; i++)
            {
                if (i == 2 | i == 3)
                {
                    output[i] = ColumnType.Integer;
                }
                else
                {
                    output[i] = ColumnType.String;
                }
            }
            return output;
        }

        static Element[] ParseToElements(string[] words, ColumnType[] types)
        {
            Element[] output = new Element[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                output[i] = new Element(words[i], types[i]);
            }
            return output;
        }

        
        public TableWorker GetSortedTable(string outputPath, bool ascending, string? attribute, SortType sortType)
        {
            int attributeNum = -1;
            for (int i = 0; i < ColumnCount; i++)
            {
                if (Attributes[i] == attribute)
                    attributeNum = i;
            }
            if (attributeNum == -1)
                throw new Exception("Неверное имя атрибута");

            switch (sortType)
            {
                case SortType.Direct:
                    SubSortDirectly(outputPath, ascending, attributeNum, 0);

                    return new TableWorker(outputPath);
                case SortType.Natural:
                {
                    string directoryPath = "temp";

                    SplitIntoTablesNaturally(directoryPath, attributeNum, ascending);

                    string newDirectoryPath = directoryPath;
                    int j = 0;
                    do
                    {
                        string[] files = Directory.GetFiles(newDirectoryPath);
                        newDirectoryPath = directoryPath + @"\temp_" + j;
                        Directory.CreateDirectory(newDirectoryPath);

                        for (int i = 1; i < files.Length; i += 2)
                        {
                            MergeSortedTables(newDirectoryPath + @"\temp_" + i / 2 + ".txt", new[] { files[i - 1], files[i] }, attributeNum, ascending);
                        }
                        if (files.Length % 2 == 1)
                        {
                            Console.WriteLine("Оставшийся файл не с чем слить, копируем его");
                            File.Copy(files[^1], newDirectoryPath + @"\temp_" + files.Length / 2 + ".txt");
                        }
                        j++;
                    } while (Directory.GetFiles(newDirectoryPath).Length > 1);

                    File.Copy(Directory.GetFiles(newDirectoryPath)[0], outputPath, true);

                    return new TableWorker(outputPath);
                }
                case SortType.Multipath:
                {
                    string directoryPath = "temp";

                    SplitIntoTablesNaturally(directoryPath, attributeNum, ascending);
                    MergeSortedTables(outputPath, Directory.GetFiles(directoryPath), attributeNum, ascending);

                    return (new TableWorker(outputPath));
                }
                default:
                    throw new Exception("Нереализованный тип сортировки");
            }
        }
        void SubSortDirectly(string outputPath, bool ascending, int columnNum, int depth)
        {
            if (RowCount > 1)
            {
                string path1 = @"temp\temp_" + depth + "_1" + ".txt";
                string path2 = @"temp\temp_" + depth + "_2" + ".txt";
                Console.WriteLine($"Создаем 2 файла:{path1} и {path2}");
                Console.WriteLine("Заполняем файлы делением корневого файла");
                SplitIntoTwoTableDirectly(path1, path2);
                TableWorker table1 = new TableWorker(path1);
                TableWorker table2 = new TableWorker(path2);
                table1.SubSortDirectly(path1, ascending, columnNum, depth + 1);
                table2.SubSortDirectly(path2, ascending, columnNum, depth + 1);
                MergeSortedTables(outputPath, new[] { path1, path2 }, columnNum, ascending);
            }
        }

        void SplitIntoTwoTableDirectly(string outputPath1, string outputPath2)
        {
            if (!File.Exists(outputPath1))
                File.Delete(outputPath1);
            if (!File.Exists(outputPath2))
                File.Delete(outputPath2);

            TableWorker table1 = new TableWorker(outputPath1, this, false);
            TableWorker table2 = new TableWorker(outputPath2, this, false);

            StreamWriter file1 = new StreamWriter(outputPath1);
            StreamWriter file2 = new StreamWriter(outputPath2);
            StreamReader originFile = new StreamReader(_filePath);

            for (int i = 0; i < 3; i++)
            {
                string line = originFile.ReadLine()!;
                file1.WriteLine(line);
                file2.WriteLine(line);
            }

            int j = 0;
            while (!originFile.EndOfStream)
            {
                if (j % 2 == 0)
                {
                    file1.WriteLine(originFile.ReadLine());
                }
                else
                {
                    file2.WriteLine(originFile.ReadLine());
                }
                j++;
            }

            file1.Close();
            file2.Close();
            originFile.Close();

            table1.SetRowCount(RowCount / 2 + RowCount % 2);
            table2.SetRowCount(RowCount / 2);
        }

        private void SplitIntoTablesNaturally(string outputDirectoryPath, int checkedColumnNum, bool ascending)
        {
            int dir = ascending ? 1 : -1;
            List<TableWorker> tables = new List<TableWorker>();

            StreamReader originFile = new StreamReader(_filePath);
            
            string[] metadata = new string[2];
            for (int i = 0; i < 2; i++)
            {
                metadata[i] = originFile.ReadLine()!;
            }

            if (Directory.Exists(outputDirectoryPath))
                Directory.Delete(outputDirectoryPath, true);
            Directory.CreateDirectory(outputDirectoryPath);

            int j = 0;
            string? pastLine = originFile.ReadLine();
            Element[] pastElements = ParseToElements(ParseLine(pastLine), Types);
            while (pastLine != null)
            {
                string path = outputDirectoryPath + @"\temp_" + j + ".txt";
                Console.WriteLine($"Создаем файл {path} и вставляем в него идущие подряд отсортированные элементы");
                tables.Add(new TableWorker(path, this, false));
                var currentFile = new StreamWriter(path);
                int rowCount = 0;

                foreach (string line in metadata)
                    currentFile.WriteLine(line);

                while (true)
                {
                    currentFile.WriteLine(pastLine);
                    rowCount++;

                    if (!originFile.EndOfStream)
                    {
                        string? line = originFile.ReadLine();
                        Element[] elements = ParseToElements(ParseLine(line), tables[j].Types);
                        if (pastElements[checkedColumnNum].CompareTo(elements[checkedColumnNum]) == dir)
                        {
                            pastLine = line;
                            pastElements = elements;
                            break;
                        }
                        pastLine = line;
                        pastElements = elements;
                    }
                    else
                    {
                        pastLine = null;
                        break;
                    }
                }

                currentFile.Close();
                tables[j].SetRowCount(rowCount);
                j++;
            }

            originFile.Close();
        }
        void MergeSortedTables(string outputPath, IReadOnlyList<string> inputPath, int columnNum, bool ascending)
        {
            Console.WriteLine("Сливаем файлы:");
            foreach (var t in inputPath)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine($"В файл {outputPath}");
            int dir = ascending ? 1 : -1;
            TableWorker[] tables = new TableWorker[inputPath.Count];
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i] = new TableWorker(inputPath[i]);
            }

            TableWorker outputTable = new TableWorker(outputPath, tables[0], false);
            StreamReader[] files = new StreamReader[inputPath.Count];

            for (int i = 0; i < inputPath.Count; i++)
            {
                files[i] = new StreamReader(inputPath[i]);
            }

            StreamWriter outputFile = new StreamWriter(outputPath);

            for (int j = 0; j < 2; j++)
            {
                outputFile.WriteLine(files[0].ReadLine());
            }
            for (int i = 1; i < inputPath.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    files[i].ReadLine();
                }
            }

            string?[] lines = new string?[inputPath.Count];
            for (int i = 0; i < inputPath.Count; i++)
                lines[i] = files[i].ReadLine();
            Element[][] elements = new Element[inputPath.Count][];
            for (int i = 0; i < inputPath.Count; i++)
                elements[i] = ParseToElements(ParseLine(lines[i]), Types);
            do
            {
                int j = GetMinOrMaxElementNum(elements);
                if (j != -1)
                {
                    outputFile.WriteLine(lines[j]);
                    if (!files[j].EndOfStream)
                    {
                        lines[j] = files[j].ReadLine();
                        elements[j] = ParseToElements(ParseLine(lines[j]), Types);
                    }
                    else
                    {
                        lines[j] = null;
                        elements[j] = null!;
                    }
                }
                else
                {
                    break;
                }
            } while (true);

            foreach (var file in files)
                file.Close();
            outputFile.Close();

            int totalRowCount = 0;
            foreach (TableWorker table in tables)
                totalRowCount += table.RowCount;
            outputTable.SetRowCount(totalRowCount);

            int GetMinOrMaxElementNum(Element[][] elements)
            {
                Element[] currentLine = null!;
                int output = -1;
                for (int i = 0; i < inputPath.Count; i++)
                {
                    if (elements[i] != null && (currentLine == null || (currentLine[columnNum].CompareTo(elements[i][columnNum]) == dir)))
                    {
                        currentLine = elements[i];
                        output = i;
                    }
                }
                return output;
            }
        }
        void SetRowCount(int count)
        {
            RowCount = count;
            RewriteLine(_filePath, 0, ColumnCount.ToString() + SeparatingSign + RowCount.ToString() + SeparatingSign);
        }

        public static TableWorker GetFilteredTable(TableWorker table, string outputPath, Condition condition)
        {
            if (File.Exists(outputPath))
                File.Delete(outputPath);

            TableWorker outputTable = new TableWorker(outputPath, table, false);

            StreamWriter outputFile = new StreamWriter(outputPath);
            StreamReader inputFile = new StreamReader(table._filePath);

            for (int i = 0; i < 3; i++)
            {
                outputFile.WriteLine(inputFile.ReadLine());
            }
            int j = 0;
            while (!inputFile.EndOfStream)
            {
                string? line = inputFile.ReadLine();
                Element[] elements = ParseToElements(ParseLine(line), table.Types);
                if (condition.Satisfies(elements))
                {
                    outputFile.WriteLine(line);
                    j++;
                }
            }

            outputFile.Close();
            inputFile.Close();

            outputTable.SetRowCount(j);

            return outputTable;
        }

        private static void RewriteLine(string path, int lineIndex, string newValue)
        {
            int i = 0;
            string tempPath = path + ".tmp";
            using (StreamReader sr = new StreamReader(path))
            using (StreamWriter sw = new StreamWriter(tempPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine()!;
                    sw.WriteLine(lineIndex == i ? newValue : line);
                    i++;
                }
            }
            File.Delete(path);
            File.Move(tempPath, path);
        }
        
    }