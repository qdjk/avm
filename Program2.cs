using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Вычисление интеграла двумя КФ: правые прямоугольники и трапеций\n");

        Console.WriteLine("Выберите функцию:");
        Console.WriteLine("1) x^2");
        Console.WriteLine("2) sin(x)");
        Console.WriteLine("3) exp(x)");
        Console.WriteLine("4) 1/(1+x^2)");
        Console.Write("Номер функции (1-4): ");
        int fnum = int.Parse(Console.ReadLine());

        Console.Write("Введите a (левая граница): ");
        double a = double.Parse(Console.ReadLine());
        Console.Write("Введите b (правая граница): ");
        double b = double.Parse(Console.ReadLine());
        if (b < a) { double t = a; a = b; b = t; }

        Console.Write("Введите точность eps (например 1e-6): ");
        double eps = double.Parse(Console.ReadLine());

        // Начальное число разбиений
        int n = 2;


        Console.WriteLine("\n\t Метод правых прямоугольников.");
        ComputeWithRunge(a, b, fnum, eps, n, "rp");


        Console.WriteLine("\n\t Метод трапеций.");
        ComputeWithRunge(a, b, fnum, eps, n, "trap");

    }

    static double Func(double x, int fnum)
    {
        if (fnum == 1) return x * x;
        else if (fnum == 2) return Math.Sin(x);
        else if (fnum == 3) return Math.Exp(x);
        else if (fnum == 4) return 1.0 / (1.0 + x * x);
        else return x * x;
    }

    static double RightRectangles(double a, double b, int n, int fnum)
    {
        double h = (b - a) / n;
        double sum = 0;
        for (int i = 1; i <= n; i++)
        {
            double xi = a + i * h;
            sum += Func(xi, fnum);
        }
        return sum * h;
    }

    static double Trapezoid(double a, double b, int n, int fnum)
    {
        double h = (b - a) / n;
        double sum = 0.5 * (Func(a, fnum) + Func(b, fnum));
        for (int i = 1; i < n; i++)
        {
            double xi = a + i * h;
            sum += Func(xi, fnum);
        }
        return sum * h;
    }

    static void ComputeWithRunge(double a, double b, int fnum, double eps, int n, string method)
    {
        Console.WriteLine();
        if (method == "r")
            Console.WriteLine("\t Метод правых прямоугольников.");
        else
            Console.WriteLine("\t Метод трапеций.");

        // Определяем порядок точности метода:
        int p;
        if (method == "rp") p = 1;
        else p = 2;

        // Вычисляем первый интеграл при n разбиениях
        double I1;
        if (method == "rp")
            I1 = RightRectangles(a, b, n, fnum);
        else
            I1 = Trapezoid(a, b, n, fnum);

        // Цикл уточнения по правилу Рунге
        while (true)
        {
            int n2 = n * 2; 

            // Для удвоенного числа разбиений
            double I2;
            if (method == "rp")
                I2 = RightRectangles(a, b, n2, fnum);
            else
                I2 = Trapezoid(a, b, n2, fnum);

            // Правило Рунге
            double denominator = Math.Pow(2, p) - 1;
            double pogreshnost = Math.Abs((I2 - I1) / denominator);
            double I_runge = I2 + (I2 - I1) / denominator; // уточнённое значение интеграла

            

            // Проверяем, достигнута ли требуемая точность
            if (pogreshnost < eps)
            {
                double h = (b - a) / n2;
                Console.WriteLine();
                Console.WriteLine("Результат:");
                Console.WriteLine($"  Приближённое значение интеграла = {I_runge}");
                Console.WriteLine($"  Оценка погрешности (Рунге) = {pogreshnost}");
                Console.WriteLine($"  Шаг h = {h}");
                Console.WriteLine($"  Количество разбиений n = {n2}");
                break; 
            }

            // Если точность ещё не достигнута — повторяем с большим n
            n = n2;
            I1 = I2;

           
            if (n > 1_000_000)
            {
                Console.WriteLine("Слишком много разбиений, остановка.");
                break;
            }
        }
    }

}
