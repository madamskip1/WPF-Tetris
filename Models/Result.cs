using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris
{
    class Result
    {
        public int ResultValue { get; set; }

        public DateTime DateTime { get; set; }

        public String DateTimeString
        {
            get { return DateTime.ToString(); }
        }

        public Result(int value, DateTime dateTime)
        {
            ResultValue = value;
            DateTime = dateTime;
        }
    }
}
