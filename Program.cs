using System;
using System.IO;

namespace CurseAche
{
    class Program
    {
        static void Main()
        {
            char men = '6';
            while (men != '5')
            {
                Console.WriteLine("\t\t    MENU    ");
                Console.WriteLine("\t1. Ввести уравнение вручную");
                Console.WriteLine("\t2. Ввести уравнение из файла");
                Console.WriteLine("\t3. Сохранить последнее введенное вручную уравнение");
                Console.WriteLine("\t4. Начать расчеты");
                Console.WriteLine("\t5. Выход ");
                try
                {
                    men = Char.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Неверно введен пункт");
                }
                switch (men)
                {
                    case '1':
                        {
                            Program.Vod();
                            break;
                        }
                    case '2':
                        {
                            Program.File();
                            break;
                        }
                    case '3':
                        {
                            Program.Save();
                            break;
                        }
                    case '4':
                        {
                            Program.Vich();
                            break;
                        }
                    case '5':
                        {
                            break;
                        }
                    case '6':
                        {
                            break;
                        }
                }
                if (men == '5')
                    break;
                men = '6';
            }
        }
        static public int n;
        static public double[,] a;// кэффициэнты иксов 
        static public double[,] a1;//клон массива а
        static public double[] b;//после знака равенства 
        static public double[] x;// ответы
        static public double det;//определитель
        static public double A1, A2;//определитель измененной матрицы
        static public bool s1, s2;
        static public void Vod()
        {
            try
            {
                Console.WriteLine("Введите размерность");
                n = Convert.ToInt32(Console.ReadLine());
                a = new double[n, n];
                a1 = new double[n, n];
                b = new double[n];
                x = new double[n];
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Введите элементы {0}й строки", i + 1);
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("x{0}*", (j + 1));

                        a[i, j] = Double.Parse(Console.ReadLine());
                        a1[i, j] = a[i, j];
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    Console.Write("\n\n");
                    Console.Write("\t\t");
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("{0}x{1}", a1[i, j], (j + 1));
                        if (j < n - 1)
                        {
                            Console.Write("  +  ");
                        }
                    }
                    Console.Write("  =  ");
                    b[i] = Int32.Parse(Console.ReadLine());
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("\t{0}", a[i, j]);
                    }
                    Console.Write("|\t{0}", b[i]);
                    Console.WriteLine("\n\n\n");
                }
                s1 = true;
                s2 = true;
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Ошибка при вводе");
            }
        }
        static public void File()
        {
            try
            {
                int y, z = 0;
                string a3;
                Console.WriteLine("Введите имя файла");
                try
                {
                    StreamReader f = new StreamReader(Console.ReadLine());
                    a3 = f.ReadLine();
                    n = Int32.Parse(a3);
                    a = new double[n, n];
                    a1 = new double[n, n];
                    b = new double[n];
                    x = new double[n];
                    Console.WriteLine(n);
                    string[] str;
                    while (z!=n)
                    {
                        a3 = f.ReadLine();
                        str = a3.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                        for (y = 0; y < str.Length - 1; y++)
                        {
                            a[z, y] = double.Parse(str[y]);
                        }
                        b[z] = double.Parse(str[(str.Length) - 1]);
                        z++;
                    }
                    for (z = 0; z < n; z++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write("{0}*x{1}", a[z, j], j + 1);
                            if (j < n - 1)
                                Console.Write("+");
                        }
                        Console.WriteLine("=" + b[z]);
                    }
                    s2 = true;
                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine("Файл не найден");
                }
            }
            catch
            {
              Console.WriteLine("Файл имеет некоректные данные");
            }
        }
        static public void Save()
        {
            if (s1)
            {
                Console.WriteLine("Введите имя файла");
                string save = Console.ReadLine();
                FileStream aFile = new FileStream(save, FileMode.OpenOrCreate);
                StreamWriter s = new StreamWriter(aFile);
                s.WriteLine(n);
                for (int z = 0; z < n; z++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        s.Write(a[z, j] + " ");
                    }
                    s.Write(a[z, n - 1]);
                    s.WriteLine("|{0}", b[z]);
                }
                s.Close();
                Console.WriteLine("Сохранено");
            }
            else
                Console.WriteLine("Вы не ввели уравнение");
        }
        static public void Vich()
        {
            if (s2)
            {

                det = 1;
                a1 = (double[,])a.Clone();
                if (n == 2)
                {
                    det = a[0, 0] * a[1, 1] - a[0, 1] * a[1, 0];
                    A1 = b[0] * a[1, 1] - a[0, 1] * b[1];
                    A2 = a[0, 0] * b[1] - b[0] * a[1, 0];
                    if (det == 0)
                    {
                        x[0] = 1;
                        x[1] = 1;
                    }
                    else
                    {
                        x[0] = A1 / det;
                        x[1] = A2 / det;
                    }
                }
                else if (n == 1)
                {
                    x[0] = b[0] / a[0, 0];
                }
                else if (n > 2)
                {
                    for (int g = 0; g < n; g++)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (a[g, g] == 0)
                            {
                                det = 0;
                            }
                            else
                            {
                                A1 = a[i, g] / a[g, g];
                                for (int j = 0; j < n; j++)
                                {
                                    if ((i > g) & (j >= g))
                                    {
                                        a[i, j] = (a[g, j] * A1) - a[i, j];
                                    }
                                }
                            }
                        }
                    }
                    for (int g = 0; g < n; g++)
                    {
                        det *= a[g, g];
                    }
                    if (det != 0)
                    {
                        for (int g1 = 0; g1 < n; g1++)
                        {
                            a = (double[,])a1.Clone();
                            for (int b1 = 0; b1 < n; b1++)
                            {
                                a[b1, g1] = b[b1];
                            }
                            for (int g = 0; g < n; g++)
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    if (a[g, g] == 0)
                                    {
                                        for (int j = 0; j < n; j++)
                                        {
                                            if ((i > g) & (j >= g))
                                            {
                                                a[i, j] = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        A1 = a[i, g] / a[g, g];
                                        for (int j = 0; j < n; j++)
                                        {
                                            if ((i > g) & (j >= g))
                                            {
                                                a[i, j] = (a[g, j] * A1) - a[i, j];
                                            }
                                        }
                                    }
                                }
                            }
                            A1 = 1;
                            for (int g = 0; g < n; g++)
                            {
                                A1 *= a[g, g];
                            }
                            x[g1] = A1 / det;
                        }
                    }
                }
                if (det == 0)
                {
                    Console.WriteLine("Определитель равен нулю");
                }
                else
                {
                    Console.WriteLine();
                    for (int g = 0; g < n; g++)
                    {
                        Console.Write("\tx{0}", g + 1);
                    }
                    Console.WriteLine();
                    for (int g = 0; g < n; g++)
                    {
                        Console.Write("\t{0:#.###}", x[g]);
                    }
                }
                Console.WriteLine("\n\n\n\n");
            }
            else
                Console.WriteLine("Вы не ввели уравнение");
        }
    }
}
