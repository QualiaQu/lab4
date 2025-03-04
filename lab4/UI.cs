﻿namespace lab4;

static class Ui
    {
        public static int AskUser(ref string? nameFile, ref string? attributeName,
            ref string ascending, ref string sorterType)
        {
            Console.WriteLine("Введите имя файла для сортировки");
            nameFile = Console.ReadLine();
            Console.WriteLine("Введите название столбца для сортировки");
            attributeName = Console.ReadLine();
            Console.WriteLine("Введите порядок сортировки: 1 - по возрастанию, 2 - по убыванию");
            ascending = Console.ReadLine()!;
            Console.WriteLine("Введите тип сортировки: 1 - прямая, 2 - натуральная, 3 - многопутевая");
            sorterType = Console.ReadLine()!;
            Console.WriteLine("Введите длительность задержки в миллисекундах: \n1000 миллисекунд = 1 секунда");
            var time = Console.ReadLine();
            return time!.Length == 0 ? 0 : Convert.ToInt32(time);
        }

        public static Condition? AskCondition(TableWorker table)
        {
            Console.Write("В каком столбце зададим условие (если нет условия нажмите Enter): ");
            string conditionAttributeName = Console.ReadLine()!;

            if (conditionAttributeName.Length != 0)
            {
                Console.WriteLine($"Введите условие (например {conditionAttributeName} < 5 или {conditionAttributeName} = 'строка'): ");
                string textOfFunction = Console.ReadLine()!;

                string[] words = ParseLine(textOfFunction);
                IComparable operand = GetOperand(words[2]);

                ConditionDelegate function = GetFunction(words[1], operand);

                return new Condition(table, conditionAttributeName, function);
            }

            return null;

            string[] ParseLine(string line)
            {
                return line.Split(); 
            }
            IComparable GetOperand(string operand)
            {
                if (int.TryParse(operand, out int result))
                    return result;
                return operand;
            }
            ConditionDelegate GetFunction(string operatorStr, IComparable operand)
            {
                switch (operatorStr)
                {
                    case ">":
                        return (value) => value.CompareTo(operand) == 1;
                    case "<":
                        return (value) => value.CompareTo(operand) == -1;
                    case "=":
                    case "==":
                        return (value) => value.CompareTo(operand) == 0;
                    case ">=":
                        return (value) => value.CompareTo(operand) != -1;
                    case "<=":
                        return (value) => value.CompareTo(operand) != 1;
                    default:
                        throw new Exception("Некорректные данные");
                }
            }
        }
    }