using System;

namespace Task2
{
    public class IntConverter
    {
        public int ConvertStringToInt(string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("The string is empty or consists only of whitespace");
            }

            string trimmedStr = str.Trim();
            bool isNegative = trimmedStr[0] == '-';
            if (isNegative)
            {
                trimmedStr = trimmedStr.Remove(0, 1);
            }

            foreach (char ch in trimmedStr)
            {
                if (!char.IsDigit(ch))
                {
                    throw new ArgumentException("The string contains incorrect symbols");
                }
            }

            int step = 0;
            int resultNumber = 0;

            foreach (char symbol in trimmedStr)
            {
                switch (symbol)
                {
                    case '0':
                        resultNumber = AddValue(resultNumber, 0, isNegative);
                        step++;
                        continue;
                    case '1':
                        resultNumber = AddValue(resultNumber, 1, isNegative);
                        step++;
                        continue;
                    case '2':
                        resultNumber = AddValue(resultNumber, 2, isNegative);
                        step++;
                        continue;
                    case '3':
                        resultNumber = AddValue(resultNumber, 3, isNegative);
                        step++;
                        continue;
                    case '4':
                        resultNumber = AddValue(resultNumber, 4, isNegative);
                        step++;
                        continue;
                    case '5':
                        resultNumber = AddValue(resultNumber, 5, isNegative);
                        step++;
                        continue;
                    case '6':
                        resultNumber = AddValue(resultNumber, 6, isNegative);
                        step++;
                        continue;
                    case '7':
                        resultNumber = AddValue(resultNumber, 7, isNegative);
                        step++;
                        continue;
                    case '8':
                        resultNumber = AddValue(resultNumber, 8, isNegative);
                        step++;
                        continue;
                    case '9':
                        resultNumber = AddValue(resultNumber, 9, isNegative);
                        step++;
                        continue;
                }
            }

            return resultNumber;
        }

        private static int AddValue(int sum, int number, bool isNegative)
        {
            checked
            {
                if (isNegative)
                {
                    sum *= 10;
                    sum -= number;
                }
                else
                {
                    sum *= 10;
                    sum += number;
                }

                return sum;
            }
        }
    }
}
