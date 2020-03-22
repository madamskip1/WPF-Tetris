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
        // List of results
        public ObservableCollection<Result> ResultsValues;

        public Results()
        {
            ResultsValues = new ObservableCollection<Result>();
        }


        public void AddResult(int result, DateTime dateTime)
        {
            ResultsValues.Add(new Result(result, dateTime));

            // Sort list DESC
            List<Result> list = ResultsValues.ToList();
            list.Sort((a, b) => b.ResultValue.CompareTo(a.ResultValue));
            ResultsValues.Clear();
            foreach (var item in list)
                ResultsValues.Add(item);
        }
    }
}
