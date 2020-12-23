using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace Laba2
{
    public class Student
    {
        public int id;
        public string full_name;
        public DateTime date;
        public string insitute;
        public string group;
        public int course;
        public double mean_score;

        public void fill_obj_from_line(string line)
        {
            var array = new List<string>();
            foreach (var val in line.Split())
            {
                array.Add(val);
            }
            id = Convert.ToInt32(array[0]);
            full_name = array[1] + " " + array[2] + " " + array[3];
            string[] temp = new string[3];
            temp = array[4].Split(".");
            date = new DateTime(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[1]),
                                    Convert.ToInt32(temp[1]));
            insitute = array[5];
            group = array[6];
            course = Convert.ToInt32(array[7]);
            mean_score = Convert.ToDouble(array[8]);
        }

        public string get_string()
        {
            return id + " " + full_name + " " + date.ToString("MM/dd/yyyy") + " "
                             + insitute + " " + group + " " + course + " " + mean_score;
        }
        public void Show()
        {
            Console.WriteLine(get_string());
        }
    }
    class Program
    {
        static List<Student> data = new List<Student>();
        static string path = @"D:\\data.txt";
        static void Main(string[] args)
        {

            static void show_data()
            {
                foreach (var obj in data)
                {
                    obj.Show();
                }
            }

            static void get_statistic()
            {
                double max = data.Max(x => x.mean_score);
                double min = data.Min(x => x.mean_score);
                double avarage = data.Average(x => x.mean_score);
                Console.WriteLine($"Максимальное значение: {max}");
                Console.WriteLine($"Минимальное значение: {min}");
                Console.WriteLine($"Среднее значение: {avarage}");

            }

            static void sorting()
            {
                var result = from obj in data
                             orderby obj.full_name, obj.date
                             select obj;
                foreach (var obj in result)
                {
                    obj.Show();
                }
            }

            static void reverse_sorting()
            {
                var result = from obj in data
                             orderby obj.full_name, obj.date
                             orderby obj.full_name, obj.date descending
                             select obj;
                foreach (var obj in result)
                {
                    obj.Show();
                }
            }

            static void from_file_to_data()
            {
                try
                {
                    using (var sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            add_record_to_data(line);
                        }

                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            static void add_record()
            {
                string line = get_line_from_console();
                add_record_to_data(line);
                rewrite();
            }

            static string get_line_from_console()
            {
                Console.WriteLine("Введите запись в данном формате:");
                Console.WriteLine("ID Фамилия Имя Отчество dd.mm.yyyy " +
                                    "Институт Группа Курс Кол-во баллов\n");
                string line = Console.ReadLine();
                return line;
            }

            static void delete()
            {
                Console.Write("Введите ID записи, которую хотите удалить: ");
                int key = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                var obj = data.Find(x => x.id == key);
                data.Remove(obj);
                rewrite();
            }
            static void change()
            {
                Console.Write("Введите ID записи, которую хотите изменить: ");
                int key = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                var obj = data.Find(x => x.id == key);
                obj.fill_obj_from_line(get_line_from_console());
                rewrite();
            }
            static void add_record_to_data(string line)
            {
                var obj = new Student();
                obj.fill_obj_from_line(line);
                data.Add(obj);
            }

            static void rewrite()
            {
                string line = "";
                try
                {
                    using (var sw = new StreamWriter(path))
                    {
                        foreach (var obj in data)
                        {
                            sw.WriteLine(obj.get_string());
                        }

                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            from_file_to_data();

            string userCommand = Console.ReadLine();
            Console.Write("Это обычная программа для работы с базой данных. \nНажмите Enter для продолжения: ");
            Console.ReadKey();
            Console.WriteLine();
            Console.Clear();
            while (userCommand != "Да")
            {
                Console.WriteLine("Что Вы хотите сделать? \n\na. Показать базу данных \nb. Добавить запись в базу данных \nc. Изменить запись в базе данных \nd. Удалить запись в базе данных" +
                    "\ne. Отсортировать по дате рождения и ФИО \nf. Отсортировать по дате рождения и ФИО в обратном порядке \ng. Найти максимальное и минимальное значение баллов среди студентов" +
                    " \nh. Выйти из программы");
                Console.Write("\nВыберите нужный пункт: ");
                string Way = Console.ReadLine();
                Console.Clear();
                switch (Way)
                {
                    case "a":
                        show_data();
                        Console.WriteLine("\nВыйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        Console.Clear();
                        break;

                    case "b":
                        add_record();
                        Console.WriteLine("\nВыйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        Console.Clear();
                        break;

                    case "c":
                        change();
                        Console.WriteLine("\nВыйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        Console.Clear();
                        break;

                    case "d":
                        delete();
                        Console.WriteLine("\nВыйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        Console.Clear();
                        break;

                    case "e":
                        sorting();
                        Console.WriteLine("\nВыйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        Console.Clear();
                        break;

                    case "f":
                        reverse_sorting();
                        Console.WriteLine("\nВыйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        Console.Clear();
                        break;

                    case "g":
                        get_statistic();
                        Console.WriteLine("\nВыйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        Console.Clear();
                        break;

                    case "h":
                        Console.WriteLine("Выйти из программы? Да/Нет");
                        userCommand = Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("Выбран неизвестный пункт, повторите попытку");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                }
            }


        }
    }
}