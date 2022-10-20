using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
                                LAB2
(+) 1. В качестве абстрактного класса взять класс, разработанный в lab1 (+)
(+) 2. В базовом классе - абстрактный метод, реализовать его в производных (+)
(+) 3. Остальные методы базового класса сделать виртуальными и переопределить в наследниках (+)
(+) 4. Производные классы со своими полями (+)

в main:
(+) Описать массив объектов базового класса -> Занести объекты из файла -> Показать работу методов класса у элементов массива (+)

                                LAB3

*/

namespace classTest
{
    public abstract class Cheat
    {
        #region Field
        public string name; // название чита
        public string author; // ник автора 
        private int version; // версия чита {нужно сделать private}
        #endregion
        #region Properties+Methods
        public int CheatVersion // свойство для безопасности поля "версия чита"
        {
            get
            {
                return version;
            }
            set
            {
                if (value > 0)
                    version = value;
                else
                {
                    Console.WriteLine($"Некорректная попытка установить {value} в качестве версии скрипта! Версия может быть лишь целым положительным числом строго больше нуля!");
                }
            }
        }
        public virtual void NextVersion() { version++; } // метод увеличения версии чита
        public abstract void Inject(); // абстрактный метод инжекта чита в игру
        #endregion
        #region Constructors
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public Cheat()
        {
            name = "None";
            author = "None";
            version = 1;
        }
        /// <summary>
        /// Конструктор для 3 параметров
        /// </summary>
        /// <param name="name">Название чита</param>
        /// <param name="author">Автор</param>
        /// <param name="version">Версия чита</param>
        public Cheat(string name, string author, int version) // конструктор для трех переменных
        {
            this.name = name;
            this.author = author;
            CheatVersion = version;
        }
        /// <summary>
        /// Цепочка из конструкторов
        /// </summary>
        /// <param name="name">Название чита</param>
        public Cheat(string name) : this(name, "None", 1) { } // используем конструктов для трех переменных, где по умолчанию автор None, а версия первая
        #endregion
        #region RedefiningOperations
        public override string ToString() // вывод информации о чите
        {
            return String.Format($"Cheat {name} is loaded! | Author: {author} | Version: {CheatVersion}");
        }
        public override bool Equals(object obj) // сравнение двух читов
        {
            if (obj is Cheat)
            {
                Cheat other = (Cheat)obj;
                return (name == other.name) && (author == other.author) && (CheatVersion == other.version);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Cheat first, Cheat second) // переопределение оператора == для сравнения читов
        {
            return first.Equals(second);
        }
        public static bool operator !=(Cheat first, Cheat second) // переопределение оператора != для сравнения читов
        {
            return !(first.Equals(second));
        }
        #endregion
    }

    public class Aimbot : Cheat
    {
        #region Field
        public bool Enable; // активирован или нет
        private double Range; // дистанция работы
        #endregion
        #region Properties
        public double range // свойство для безопасности поля "дистанция аима" 
        {
            get
            {
                return Range;
            }
            set
            {
                if (value >= 0)
                    Range = value;
                else
                {
                    Console.WriteLine($"Дистанция не может быть отрицательным числом!");
                }
            }
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return base.ToString() + $" | Aimbot status: {Enable} | Range: {range}";
        }
        public override void NextVersion() // переопределение виртуального метода 
        {
            base.NextVersion();
            Console.WriteLine("Версия Aimbot'a успешно обновлена!");
        }
        public override void Inject() // переопределение абстрактного метода 
        {
            Console.WriteLine($"[{name}] Aimbot injected!");
        }
        #endregion
        #region Constructors
        public Aimbot(string name, string author, int version, bool enable, double range) : base(name, author, version)
        {
            Enable = enable;
            this.range = range;
        }
        #endregion
    }

    public class WallHack : Cheat
    {
        #region Field
        public bool bones;
        public bool ESPlines;
        #endregion
        #region Methods
        public override void NextVersion() // переопределение виртуального метода 
        {
            base.NextVersion();
            Console.WriteLine("Версия Wallhack'a успешно обновлена!");
        }
        public override void Inject() // переопределение абстрактного метода 
        {
            Console.WriteLine($"[{name}] Wallhack injected!");
        }
        public override string ToString()
        {
            return base.ToString() + $" | ShowBones: {bones} | ShowLines: {ESPlines}";
        }
        #endregion
        #region Constructors
        public WallHack(string name, string author, int version, bool bones, bool ESPlines) : base(name, author, version)
        {
            this.bones = bones;
            this.ESPlines = ESPlines;
        }
        public WallHack(string name, string author, int version, bool bones) : this(name, author, version, bones, false) { }
        public WallHack(string name, string author, int version) : this(name, author, version, false) { }
        public WallHack(string name, string author) : this(name, author, 1) { }
        public WallHack(string name) : this(name, "None") { }
        public WallHack() : this("None") { }
        #endregion
    }

    public class Antistun : Cheat
    {
        #region Field
        private int delay;
        private int percent;
        #endregion
        #region Properties
        public int Delay // свойство для безопасности поля "задержка"
        {
            get
            {
                return delay;
            }
            set
            {
                if (value >= 0)
                    delay = value;
                else
                {
                    Console.WriteLine($"Задержка не может быть отрицательным числом!");
                }
            }
        }
        public int Percent // свойство для безопасности поля "вероятность стана"
        {
            get
            {
                return percent;
            }
            set
            {
                if (value >= 0)
                    percent = value;
                else
                {
                    Console.WriteLine($"Процент стана не может быть отрицательным числом!");
                }
            }
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return base.ToString() + $" | Delay: {delay} | StanChance: {percent}";
        }
        public override void NextVersion() // переопределение виртуального метода 
        {
            base.NextVersion();
            Console.WriteLine("Версия Antistun'a успешно обновлена!");
        }
        public override void Inject() // переопределение абстрактного метода 
        {
            Console.WriteLine($"[{name}] Antistun injected!");
        }
        #endregion
        #region Constructors
        public Antistun(string name, string author, int version, int delay, int percent) : base(name, author, version)
        {
            Delay = delay;
            Percent = percent;
        }
        #endregion
    }

    public class Smooth : Aimbot
    {
        #region Field
        private double SSmooth;
        private int FOV;
        #endregion
        #region Properties
        public int fov // свойство для безопасности поля "версия чита"
        {
            get
            {
                return FOV;
            }
            set
            {
                if (value >= 0)
                    FOV = value;
                else
                {
                    Console.WriteLine($"FOV не может быть отрицательным числом!");
                }
            }
        }
        public double smoothness // свойство для безопасности поля "версия чита"
        {
            get
            {
                return SSmooth;
            }
            set
            {
                if (value >= 0)
                    SSmooth = value;
                else
                {
                    Console.WriteLine($"Плавность наводки не может быть отрицательным числом!");
                }
            }
        }
        #endregion
        #region Methods
        public override void NextVersion() // переопределение виртуального метода 
        {
            CheatVersion++;
            Console.WriteLine("Успешно обновлены функции Smooth аима!");
        }
        public override void Inject() // переопределение абстрактного метода
        {
            Console.WriteLine($"[{name}] Smooth injected! Settings Available: Smoothness | FOV");
        }
        public override string ToString()
        {
            return base.ToString() + $"\nSmooth: {SSmooth} | FOV: {FOV}";
        }
        #endregion
        #region Constructors
        public Smooth(string name, string author, int version, bool enable, double range, double smooth, int fov) : base(name, author, version, enable, range)
        {
            smoothness = smooth;
            this.fov = fov;
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            int n = int.Parse(sr.ReadLine());
            Cheat[] multihack = new Cheat[n];
            for (int i = 0; i < n; i++)
            {
                string[] s = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //if (s[0] != "4" && s.Length < 5)
                //{
                //    Console.WriteLine("Неверное количество аргументов!");
                //    Console.WriteLine();
                //}
                //else if (s[0] == "4" && s.Length < 7)
                //{
                //    Console.WriteLine("Неверное количество аргументов!");
                //    Console.WriteLine();
                //}
                if (s[0] == "1") //Aim
                {
                    multihack[i] = new Aimbot(s[1], s[2], int.Parse(s[3]), bool.Parse(s[4]), double.Parse(s[5]));
                    multihack[i].NextVersion();
                    multihack[i].NextVersion();
                    multihack[i].Inject();
                    Console.WriteLine(multihack[i]);
                    Console.WriteLine();
                }
                else if (s[0] == "2") //WH
                {
                    multihack[i] = new WallHack(s[1], s[2], int.Parse(s[3]), bool.Parse(s[4]), bool.Parse(s[5]));
                    multihack[i].NextVersion();
                    multihack[i].Inject();
                    Console.WriteLine(multihack[i]);
                    Console.WriteLine();
                }
                else if (s[0] == "3") //Antistun
                {
                    multihack[i] = new Antistun(s[1], s[2], int.Parse(s[3]), int.Parse(s[4]), int.Parse(s[5]));
                    multihack[i].NextVersion();
                    multihack[i].NextVersion();
                    multihack[i].NextVersion();
                    multihack[i].Inject();
                    Console.WriteLine(multihack[i]);
                    Console.WriteLine();
                }
                else if (s[0] == "4") //Smooth
                {
                    multihack[i] = new Smooth(s[1], s[2], int.Parse(s[3]), bool.Parse(s[4]), double.Parse(s[5]), double.Parse(s[6]), int.Parse(s[7]));
                    multihack[i].NextVersion();
                    multihack[i].NextVersion();
                    multihack[i].Inject();
                    Console.WriteLine(multihack[i]);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Выбранного флага объекта не существует!");
                    Console.WriteLine();
                }
            }
            sr.Close();
            Console.ReadKey();
        }
    }
}