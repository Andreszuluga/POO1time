using POO1time.backen.POO1time.backen;

namespace POO1time.backen
{
    namespace POO1time.backen
    {
        public class Time
        {
            private int hour;
            private int minute;
            private int second;
            private int millisecond;

            // 1. 1. No parameters
            public Time() : this(0, 0, 0, 0) { }


            // 2. Just hours
            public Time(int hour) : this(hour, 0, 0, 0) { }


            // 3. Hours and minutes
            public Time(int hour, int minute) : this(hour, minute, 0, 0) { }


            // 4. Hours, minutes, and seconds
            public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }


            // 5. Main builder
            public Time(int hour, int minute, int second, int millisecond)
            {
                if (hour < 0 || hour > 23)
                    throw new ArgumentException("Invalid time");

                if (minute < 0 || minute > 59)
                    throw new ArgumentException("Invalid minute");

                if (second < 0 || second > 59)
                    throw new ArgumentException("Second invalid");

                if (millisecond < 0 || millisecond > 999)
                    throw new ArgumentException("Invalid millisecond");

                this.hour = hour;
                this.minute = minute;
                this.second = second;
                this.millisecond = millisecond;
            }

            public override string ToString()
            {
                int displayHour = hour % 12;
                if (displayHour == 0)
                    displayHour = 12;

                string ampm = hour < 12 ? "AM" : "PM";

                return $"{displayHour:D2}:{minute:D2}:{second:D2}.{millisecond:D3} {ampm}";
            }

            public int ToMilliseconds()
            {
                return ((hour * 60 * 60) + (minute * 60) + second) * 1000 + millisecond;
            }

            public int ToSeconds()
            {
                return (hour * 3600) + (minute * 60) + second;
            }

            public int ToMinutes()
            {
                return (hour * 60) + minute;
            }

            public bool IsOtherDay(Time other)
            {
                return this.ToMilliseconds() + other.ToMilliseconds() >= 24 * 60 * 60 * 1000;
            }

            public Time Add(Time other)
            {
                int totalMs = this.ToMilliseconds() + other.ToMilliseconds();

                int msInDay = 24 * 60 * 60 * 1000;

                totalMs = totalMs % msInDay;

                int h = totalMs / (3600 * 1000);
                totalMs %= (3600 * 1000);

                int m = totalMs / (60 * 1000);
                totalMs %= (60 * 1000);

                int s = totalMs / 1000;
                int ms = totalMs % 1000;

                return new Time(h, m, s, ms);
            }
        }
    }
}
-----------------------------------------------------------------------------------------------------------------------------------------
using System;

namespace POO1time.backen
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Time t1 = new Time();
                Time t2 = new Time(10);
                Time t3 = new Time(2, 30, 15, 500);
                Time t4 = new Time(20, 45, 30, 800);
                Time t5 = new Time(23, 58, 34, 666);

                Console.WriteLine("Horas iniciales:");
                Console.WriteLine($"t1 = {t1}");
                Console.WriteLine($"t2 = {t2}");
                Console.WriteLine($"t3 = {t3}");
                Console.WriteLine($"t4 = {t4}");
                Console.WriteLine($"t5 = {t5}");

                Console.WriteLine("\nSumando t3 a cada hora:");
                Console.WriteLine($"t1 + t3 = {t1.Add(t3)}");
                Console.WriteLine($"t2 + t3 = {t2.Add(t3)}");
                Console.WriteLine($"t3 + t3 = {t3.Add(t3)}");
                Console.WriteLine($"t4 + t3 = {t4.Add(t3)}");
                Console.WriteLine($"t5 + t3 = {t5.Add(t3)}");

                Console.WriteLine("\n¿Pasa al otro día al sumar t4?");
                Console.WriteLine($"t1 + t4 -> {t1.IsOtherDay(t4)}");
                Console.WriteLine($"t2 + t4 -> {t2.IsOtherDay(t4)}");
                Console.WriteLine($"t3 + t4 -> {t3.IsOtherDay(t4)}");
                Console.WriteLine($"t4 + t4 -> {t4.IsOtherDay(t4)}");
                Console.WriteLine($"t5 + t4 -> {t5.IsOtherDay(t4)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}