using System;
using System.Text;

namespace NumberInWords
{
    public class RusNumber
    {
        //Наименования сотен
        private static readonly string[] Hunds =
        {
            "", "сто ", "двести ", "триста ", "четыреста ",
            "пятьсот ", "шестьсот ", "семьсот ", "восемьсот ", "девятьсот "
        };
        //Наименования десятков
        private static readonly string[] Tens =
        {
            "", "десять ", "двадцать ", "тридцать ", "сорок ", "пятьдесят ",
            "шестьдесят ", "семьдесят ", "восемьдесят ", "девяносто "
        };
        /// <summary>
        /// Перевод в строку числа с учётом падежного окончания относящегося к числу существительного
        /// </summary>
        /// <param name="val">Число</param>
        /// <param name="male">Род существительного, которое относится к числу</param>
        /// <param name="one">Форма существительного в единственном числе</param>
        /// <param name="two">Форма существительного от двух до четырёх</param>
        /// <param name="five">Форма существительного от пяти и больше</param>
        /// <returns></returns>
        public static string Str(int val, bool male, string one, string two, string five)
        {
            string[] frac20 =
            {
                "", "один ", "два ", "три ", "четыре ", "пять ", "шесть ",
                "семь ", "восемь ", "девять ", "десять ", "одиннадцать ",
                "двенадцать ", "тринадцать ", "четырнадцать ", "пятнадцать ",
                "шестнадцать ", "семнадцать ", "восемнадцать ", "девятнадцать "
            };

            var num = val % 1000;
            if (0 == num) return "";
            if (num < 0) throw new ArgumentOutOfRangeException(nameof(val), "Параметр не может быть отрицательным");
            if (!male)
            {
                frac20[1] = "одна ";
                frac20[2] = "две ";
            }

            var r = new StringBuilder(Hunds[num / 100]);

            if (num % 100 < 20)
            {
                r.Append(frac20[num % 100]);
            }
            else
            {
                r.Append(Tens[num % 100 / 10]);
                r.Append(frac20[num % 10]);
            }

            r.Append(Case(num, one, two, five));

            if (r.Length != 0) r.Append(" ");
            return r.ToString();
        }
        /// <summary>
        /// Выбор правильного падежного окончания сущесвительного
        /// </summary>
        /// <param name="val">Число</param>
        /// <param name="one">Форма существительного в единственном числе</param>
        /// <param name="two">Форма существительного от двух до четырёх</param>
        /// <param name="five">Форма существительного от пяти и больше</param>
        /// <returns>Возвращает существительное с падежным окончанием, которое соответсвует числу</returns>
        public static string Case(int val, string one, string two, string five)
        {
            var t = (val % 100 > 20) ? val % 10 : val % 20;

            switch (t)
            {
                case 1: return one;
                case 2: case 3: case 4: return two;
                default: return five;
            }
        }
        /// <summary>
        /// Перевод целого числа в строку
        /// </summary>
        /// <param name="val">Число</param>
        /// <returns>Возвращает строковую запись числа</returns>
        public static string Str(decimal val)
        {
            // 1. Используем Math.Truncate, чтобы работать только с целой частью
            decimal n = Math.Abs(Math.Truncate(val));
            bool minus = val < 0;

            var r = new StringBuilder();

            if (0 == n) return "Ноль"; // Быстрый выход для нуля

            // Логика разделения по разрядам (по 1000)
            // Остаток от деления на 1000 даст нам текущий класс (единицы, тысячи и т.д.)

            // Единицы
            if (n % 1000 != 0)
                r.Append(Str((int)(n % 1000), true, "", "", ""));
            n = Math.Truncate(n / 1000);

            // Тысячи
            if (n % 1000 != 0)
                r.Insert(0, Str((int)(n % 1000), false, "тысяча ", "тысячи ", "тысяч "));
            n = Math.Truncate(n / 1000);

            // Миллионы
            if (n % 1000 != 0)
                r.Insert(0, Str((int)(n % 1000), true, "миллион ", "миллиона ", "миллионов "));
            n = Math.Truncate(n / 1000);

            // Миллиарды
            if (n % 1000 != 0)
                r.Insert(0, Str((int)(n % 1000), true, "миллиард ", "миллиарда ", "миллиардов "));
            n = Math.Truncate(n / 1000);

            // Триллионы
            if (n % 1000 != 0)
                r.Insert(0, Str((int)(n % 1000), true, "триллион ", "триллиона ", "триллионов "));

            // И так далее для квадриллионов, если нужно...

            if (minus) r.Insert(0, "минус ");

            // Делаем первую букву заглавной и убираем лишние пробелы в конце
            string result = r.ToString().Trim();
            if (string.IsNullOrEmpty(result)) return "";

            return char.ToUpper(result[0]) + result.Substring(1);
        }
    }
}
