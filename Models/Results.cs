using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class Results
    {
        public List<ResultObject> ResultsValues;

        private static Results _instance;

        private Results()
        {
            ResultsValues = new List<ResultObject>();
        }

        public static Results Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Results();

                return _instance;
            }
        }

        public void AddResult(int result, DateTime dateTime)
        {
            ResultsValues.Add(new ResultObject(result, dateTime));

            // sort DESC
            ResultsValues.Sort((a, b) => b.Result.CompareTo(a.Result));
        }

        public List<ResultObject> GetBestResults(int bestOf = 5)
        {
            return ResultsValues.GetRange(0, bestOf);
        }
    }

    class ResultObject
    {
        public int Result { get; set; }

        public DateTime DateTime { get; set; }

        public String DateTimeString
        {
            get
            {
                return DateTime.ToString();
            }
        }
        public ResultObject(int value, DateTime dateTime)
        {
            Result = value;
            DateTime = dateTime;
        }
    }
}
