﻿//Дает возможность ссылаться на классы, которые находятся в пространстве имен System, 
//так что их можно использовать, не добавляя System. перед именем типа.
using System;
//Содержит интерфейсы и классы, определяющие универсальные коллекции, 
//которые позволяют пользователям создавать строго типизированные коллекции, 
//обеспечивающие повышенную производительность и безопасность типов 
//по сравнению с неуниверсальными строго типизированными коллекциями.
using System.Collections.Generic;
//Содержит классы и интерфейсы, которые поддерживают запросы, 
//использующие LINQ.
using System.Linq;
//Пространство имен содержит классы, представляющие ASCII и Unicode 
//кодировок; абстрактные базовые классы для преобразования блоков символов
//и из блоков байтов
using System.Text;
//Создает и контролирует поток, задает приоритет и возвращает статус.
using System.Threading.Tasks;
//Пространство имен System.IO содержит типы, позволяющие осуществлять чтение и запись в 
//файлы и потоки данных, а также типы для базовой поддержки файлов и папок.
using System.IO;

// объявления области, которая содержит набор связанных объектов. Можно использовать 
//пространство имён для организации элементов кода, а также для создания глобально 
//уникальных типов.
namespace lesson_2__1__Игра
{
    //Определения класса с именем Program
    class Program
    {
        //Модификатор static используется для объявления статического члена, принадлежащего 
        //собственно типу, а не конкретному объекту.
        //Слово void указывает, что метод не возвращает значение
        //Метод Main является точкой входа EXE-программы, в которой начинается и заканчивается 
        //управление программой.
        //string[] содержит аргументы командной строки, или без него.
        //Массив аргументов, который передается приложению операционной системой.
        static void Main(string[] args)
        {
            // Задаем длительность игры
            TimeSpan duration = new TimeSpan(0, 0, 60);

            // Задаем диапазон случайных значений
            const int MIN = 2;	//	0 и 1 - слишком просто
            const int MAX = 100;
            const int MAX_MULT = 10;	//	предел верхнего значения для умножения

            // Создаем генератор случайных чисел
            Random rnd = new Random(DateTime.Now.Millisecond);

            int result = 0;	// результат вычисления каждой задачи
            int points = 0;	// количество набранных очков за правильные ответы

            char[] opSigns ={ '+',	'/', '*', '/' };	//	массив	знаков	операций
            string s;
            bool err = false; // допущена ли ошибка при ответе 
            
            // Правила игры
            Console.WriteLine("Игра \"БЫСТРЫЙ СЧЕТ\"");
            Console.WriteLine();//Пустая строка
            Console.WriteLine("Постарайтесь за 1 минуту решить как можно больше"); 
            Console.WriteLine("несложных арифметических примеров.");
            Console.WriteLine();//Пустая строка
            Console.WriteLine("Нажмите любую клавишу, чтобы начать игру...");
            Console.WriteLine();//Пустая строка
            Console.ReadKey();//Ожидание

            // Засекаем время, определяем время остановки
            DateTime start = DateTime.Now;
            DateTime end = start + duration;

            // Продолжаем цикл, пока не истечет время while (DateTime.Now < end) 
            while (DateTime.Now < end)
            {
                // Генерируем случайные операнды и операцию 
                char op = opSigns[rnd.Next(opSigns.Length)]; 
                int a = rnd.Next(MIN, MAX); 
                int b = rnd.Next(MIN, MAX);

                // Анализируем выпавшую операцию 
                switch (op)	
                { 
                    // СЛОЖЕНИЕ 
                    case '+':
                        result = a + b; 
                        break;

                    // ВЫЧИТАНИЕ 
                    case '-':
                        // В случае вычитания проверяем равенство чисел 
                        // Если числа равны, то - слишком просто.
                        // Запускаем цикл рандомизации заново.
                        // При этом знак операции может измениться, 
                        if (a == b) 
                            continue;

                        // Менять местами большее с меньшим не стоит,
                        // т.к. сложность все равно не настолько высока.
                        result = a - b;
                        break;

                    // УМНОЖЕНИЕ 
                    case '*':
                        //В случае умножения проверяем, чтобы оба значения 
                        // не были слишком велики, и при необходимости 
                        // понижаем степень сложности задачи.
                        if (a > MAX_MULT && b > MAX_MULT)
                        {
                            a = rnd.Next(MIN, MAX_MULT); 
                            b = rnd.Next(MIN, MAX_MULT);
                        }

                        result = a * b;
                        break;

                    // ДЕЛЕНИЕ 
                    case '/':
                        // В случае деления проверяем,
                        // чтобы операнды делились без остатка.

                        // Меняем местами, если a < b
                        if (a < b)
                        {
                            int c = a; 
                            a = b; 
                            b = c;
                        }

                        // Если а не делится на b без остатка,
                        // перевычисляем его так, чтобы делилось, 
                        if (a % b != 0)
                            a = b * (a / b);

                        // Если числа были/стали равны, то - слишком просто, 
                        // Запускаем цикл рандомизации заново.
                        // При этом знак операции может измениться 
                        if (a == b) 
                            continue;
                        
                        result = a / b;
                        break;
                }

                // Выводим пример и ждем ответа
                Console.Write (" {0} {1} {2} = ", a, op, b);
                s = Console.ReadLine();
                int answer;

                // При ошибке (и при неправильном вводе!) выходим из цикла до его окончания 
                if (!int.TryParse(s, out answer) || (answer != result))
                {
                    err = true; // фиксируем, что была ошибка 
                    break;
                }

                // Если не ошибка, увеличиваем правильные очки 
                ++points;
            }
                
            Console.WriteLine();

            // В зависимости от флага ошибки выводим соответствующее сообщение.
            Console.Write(err ? "Вы допустили ошибку. " : "Время истекло. ");
               
            Console.WriteLine("Игра окончена!");
            Console.WriteLine("Правильных ответов: {0}", points);
            
            // Фиксирование рекорда

            // Имя файла, где мы будем хранить данные о рекордах.
            string filename = "record.txt";

            // Служебные переменные 
            string[] content; 
            int record = 0;
            
            // Существует ли файл с данными? 
            if (File.Exists(filename))
            {
                // Читаем файл, там должно быть две строки:
                // - количество очков 
                // - дата рекорда
                content = File.ReadAllLines(filename);
                
                // Приводим количество очков к целочисленному типу.
                record = int.Parse(content[0]);
                
                // Если рекорд не был побит, выводим информацию о нем. 
                if (record >= points)
                    Console.WriteLine("Последний рекорд: {0} от {1}", record, content[1]);
            }
            // Если файл не существует или рекорд побит в текущей игре,
            if (!File.Exists(filename) || record < points)
            {
                // записываем в файл текущий результат игры.
                Console.WriteLine("Поздравляем! Ваш результат лучший!");
                //2 строки
                content = new string[2];
                record = points;
                //1 строка - рекорд
                content[0] = record.ToString();
                //Дата и время
                content[1] = DateTime.Now.ToString();
                //Создаеи файл и записываем в него указанный массив строк 
                //и затем закрываем файл
                File.WriteAllLines(filename, content);
            }


            Console.ReadKey();//Ожидание

        }
    }
}
