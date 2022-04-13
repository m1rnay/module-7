using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lesson_6._6
{
    public struct Sotrudnik:IComparable<Sotrudnik>
    {
        public int ID;
        public DateTime Data;
        public string FIO;
        public int age;
        public int height;
        public string date_birthday;
        public string mesto_rozhdeniya;

        public int CompareTo(Sotrudnik item)
        {
            return DateTime.Compare(this.Data, item.Data);
        }
    }



    class Program
    {
        public static string stroka = "";
        public static char[] al = { };
        public static int kolvo;
        static List<Sotrudnik> mass = new List<Sotrudnik>();
        static void Main(string[] args)
        {
            Console.WriteLine("Сделайте выбор:");
            Console.WriteLine("1 - Добавление нового сотрудника(в конец списка)");
            Console.WriteLine("2 - Чтение файла");
            Console.WriteLine("3 - Удаление строки(по номеру)");
            Console.WriteLine("4 - Редактирование всей строки");
            Console.WriteLine("5 - Вывод записей по определённому диапазону дат создания");
            Console.WriteLine("6 - Сортировка записей по возрастанию/убыванию даты создания");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите желаемое количество добавления сотрудников:");
                        kolvo = int.Parse(Console.ReadLine());
                        Dobavlenie();
                        break;
                    case 2:
                        Chtenie();
                        break;
                    case 3:
                        Udalenie();
                        break;
                    case 4:
                        Redactirovanie();
                        break;
                    case 5:
                        Diapazon_dat();
                        break;
                    case 6:
                        Sortirovka();
                        break;
                    default:
                        Console.WriteLine("Вы ввели неверное число!");
                        break;
                }
        }
        static void Dobavlenie()
        {
            StreamWriter writer = new StreamWriter("D://Sotrudniki.txt", true);
            for (int i = 0; i < kolvo; i++)
            {
                Sotrudnik sotrudnik = new Sotrudnik();       
                sotrudnik.Data = DateTime.Now;
                Console.WriteLine("Введите ID");
                sotrudnik.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите ФИО сотрудника");
                sotrudnik.FIO = Console.ReadLine();
                Console.WriteLine("Введите возраст сотрудника");
                sotrudnik.age = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите рост сотрудника");
                sotrudnik.height = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите место рождения сотрудника");
                sotrudnik.mesto_rozhdeniya = Console.ReadLine();
                string targetDateFormat = "dd/MM/yyyy";
                DateTime dt;   
                Console.WriteLine("Введите дату рождения сотрудника(день/месяц/год)");
                string enteredDateString = Console.ReadLine();
                dt = DateTime.ParseExact(enteredDateString, targetDateFormat, CultureInfo.InvariantCulture);
                sotrudnik.date_birthday = dt.ToString("dd/MM/yyyy");
                mass.Add(sotrudnik);
                writer.WriteLine($"{sotrudnik.ID}#{sotrudnik.Data}#{sotrudnik.FIO}#{sotrudnik.age}#{sotrudnik.height}#{sotrudnik.date_birthday}#{sotrudnik.mesto_rozhdeniya}");

            }
            writer.Close();
            foreach (var item in mass)
            {
                Console.WriteLine(item);
            }
        }
        static void Udalenie()
        {
            string[] readText = File.ReadAllLines("D://Sotrudniki.txt");
            Console.WriteLine("Выберите какую строку вы хотите удалить(первая строка - 0)");
            int number = int.Parse(Console.ReadLine());

            readText[number] = "";
            var result = readText.Where(x => !string.IsNullOrWhiteSpace(x));
            File.WriteAllLines("D://Sotrudniki.txt", result);
        }
        static void Redactirovanie()
        {
            string[] readText = File.ReadAllLines("D://Sotrudniki.txt");
            Console.WriteLine("Введите номер строки которую хотите редактировать:");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Внимание!");
            Console.WriteLine("Заполнение строки необходимо производить в следующем формате:");
            Console.WriteLine("ID#Дата добавления#ФИО#возраст#Рост#Дата рождения#Место рождения");
            readText[number] = Console.ReadLine();
            File.WriteAllLines("D://Sotrudniki.txt", readText);
        }
        static void Diapazon_dat()
        {
            Console.WriteLine("Введите первую дату диапазона(dd.mm.yyyy)");
            DateTime firstDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите вторую дату диапазона(dd.mm.yyyy)");
            DateTime secondDate = DateTime.Parse(Console.ReadLine());
            List<string> notes = new List<string>();
            string[] readText = File.ReadAllLines("D://Sotrudniki.txt");
            foreach (string str in readText)
            {
                string date = str.Split(' ')[0].Split('#')[1];
                DateTime dateTime = DateTime.Parse(date);
                if (dateTime >= firstDate && dateTime <= secondDate)
                {
                    notes.Add(str);
                }

            }
            Console.WriteLine("Записи расположенные в выбранном диапазоне дат:");
            foreach (string note in notes)
            {
                Console.WriteLine(note);
            }
        }
        static void Sortirovka()
        {
            Console.WriteLine("Выберите, какую из сортировок вы хотите выполнить:");
            Console.WriteLine("1 - по возрастанию");
            Console.WriteLine("2 - по убыванию");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    foreach (string line in File.ReadLines("D://Sotrudniki.txt"))
                    {
                        var newline = line.Split("#");
                        Sotrudnik sotrudnik = new Sotrudnik();
                        sotrudnik.ID = int.Parse(newline[0]);
                        sotrudnik.Data = DateTime.Parse(newline[1]);
                        sotrudnik.FIO = newline[2];
                        sotrudnik.age = int.Parse(newline[3]);
                        sotrudnik.height = int.Parse(newline[4]);
                        sotrudnik.date_birthday = newline[5];
                        sotrudnik.mesto_rozhdeniya = newline[6];
                        mass.Add(sotrudnik);
                        
                    }
                    mass.Sort();
                    foreach (var item in mass)
                    {
                        Console.WriteLine($"{item.ID}#{item.Data}#{item.FIO}#{item.age}#{item.height}#{item.date_birthday}#{item.mesto_rozhdeniya}");
                    }
                    break;
                case 2:
                    foreach (string line in File.ReadLines("D://Sotrudniki.txt"))
                    {
                        var newline = line.Split("#");
                        Sotrudnik sotrudnik = new Sotrudnik();
                        sotrudnik.ID = int.Parse(newline[0]);
                        sotrudnik.Data = DateTime.Parse(newline[1]);
                        sotrudnik.FIO = newline[2];
                        sotrudnik.age = int.Parse(newline[3]);
                        sotrudnik.height = int.Parse(newline[4]);
                        sotrudnik.date_birthday = newline[5];
                        sotrudnik.mesto_rozhdeniya = newline[6];
                        mass.Add(sotrudnik);
                    }
                    mass.Sort();
                    mass.Reverse();
                    foreach (var item in mass)
                    {
                        Console.WriteLine($"{item.ID}#{item.Data}#{item.FIO}#{item.age}#{item.height}#{item.date_birthday}#{item.mesto_rozhdeniya}");
                    }
                    break;
                default:
                    Console.WriteLine("Вы ввели неверное число!");
                    break;
            }
            
        }
        static void Chtenie()
        {
            Console.WriteLine("Выберите какое чтение хотите выполнить");
            Console.WriteLine("1 - Весь файл");
            Console.WriteLine("2 - Определённую запись по ID");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    FileStream stream = new FileStream("D://Sotrudniki.txt", FileMode.Open);
                    StreamReader reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    stream.Close();
                    Console.WriteLine(str);
                    break;
                case 2:
                    Console.WriteLine("Введите ID записи которую хотите прочитать");

                    string vved_ID = Console.ReadLine();
                    foreach (string line in File.ReadLines("D://Sotrudniki.txt"))
                    {

                        if (line[0] == char.Parse(vved_ID))
                        {
                            Console.WriteLine(line);
                        }

                    }
                    break;
                default:
                    Console.WriteLine("Вы ввели неверное число!");
                    break;
            }
        }
    }
}