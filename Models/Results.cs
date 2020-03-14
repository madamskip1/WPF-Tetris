using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class Results
    {
        public ObservableCollection<ResultObject> ResultsValues;

        public Results()
        {
            ResultsValues = new ObservableCollection<ResultObject>();
        }


        public void AddResult(int result, DateTime dateTime)
        {
            ResultsValues.Add(new ResultObject(result, dateTime));


            List<ResultObject> list = ResultsValues.ToList();
            list.Sort((a, b) => b.Result.CompareTo(a.Result));
            ResultsValues.Clear();
            foreach (var item in list)
                ResultsValues.Add(item);
            // ResultsValues = new ObservableCollection<ResultObject>(ResultsValues.OrderBy(x => x.Result));
            // sort DESC
            //*//   ResultsValues.
        }

        public ObservableCollection<ResultObject> GetBestResults(int bestOf = 5)
        {
            return ResultsValues;
            //return ResultsValues.GetRange(0, bestOf);
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
