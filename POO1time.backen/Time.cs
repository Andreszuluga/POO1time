using POO1time.backen.POO1time.backen;

namespace POO1time.backen;

    public class Time
    {
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;

            // 1. 1. No parameters
        public Time() 
        {
            hour = 0;
            minute = 0;
            second = 0;
            millisecond = 0;
        }



    // 2. Just hours
        public Time(int hour)
        {
            Hour = hour;
        }

    // 3. Hours and minutes
        public Time(int hour, int minute)
        {   Hour = hour; 
        Minute = minute;  
       

        }


    // 4. Hours, minutes, and seconds
        public Time(int hour, int minute, int second)
        {
             Hour = hour;
            Minute = minute;
            Seconds = second;
        }

            // 5. Main builder
            public Time(int hour, int minute, int second, int millisecond)
            {
            Hour = hour;
            Minute = minute;
            Seconds = second;
            Millisecond = millisecond;

            }
    public int Hour
    {
        get => _hour;
        set=> _hour =  ValidHour(value);
    }

    public int Minute
    {
        get => _minute; 
        set => _minute = ValidMinute(value);

    }

    public int Seconds
    {
        get => _second;
        set => _second = ValidSecond(value);
    }
     public int Millisecond

     {  get => _millisecond;
        set => _millisecond = ValidMillisecond(value);
     }

    public override string ToString()
            {
                int displayHour = Hour % 12;
                if (displayHour == 0)
                    displayHour = 12;

                string ampm = Hour < 12 ? "AM" : "PM";

                return $"{displayHour:00}:{Minute:00}:{Second:00}.{Millisecond:00} {ampm}";
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
                int totaLHours = this.hour + other.hour;
            if (totaLHours >= 24)
                    return true;
                else
                    return false;
            }

            public Time Add(Time other)
            {
                int totalMilliseconds = this.ToMilliseconds() + other.ToMilliseconds();

                int msInDay = 24 * 60 * 60 * 1000;

            totalMilliseconds = totalMilliseconds % msInDay;

                int h = totalMilliseconds / (3600 * 1000);
            totalMilliseconds %= (3600 * 1000);

                int m = totalMilliseconds / (60 * 1000);
            totalMilliseconds %= (60 * 1000);

                int s = totalMilliseconds / 1000;
                int ms = totalMilliseconds % 1000;

                return new Time(h, m, s, ms);
            }
        private int ValidHour(int hour)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "Hour must be between 0 and 23.");
            return hour;
        }
        private int ValidMinute(int minute)
        {
            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Minute must be between 0 and 59.");
            return minute;
        }
        private int ValidSecond(int second)
        {
            if (second < 0 || second > 59)
                throw new ArgumentOutOfRangeException(nameof(second), "Second must be between 0 and 59.");
            return second;
        }
        private int ValidMillisecond(int millisecond)
        {
            if (millisecond < 0 || millisecond > 999)
                throw new ArgumentOutOfRangeException(nameof(millisecond), "Millisecond must be between 0 and 999.");
            return millisecond;
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