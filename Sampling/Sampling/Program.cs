﻿namespace Sampling
{
    public class Program 
    {
        public static void Main()
        {
            List<Measurement> vMesure = new List<Measurement>();
            
            vMesure.Add(new Measurement(DateTime.Parse("2017-01-03T10:04:45"), 35.79, MeasurementType.TEMP));
            vMesure.Add(new Measurement(DateTime.Parse("2017-01-03T10:01:18"), 98.78, MeasurementType.SPO2));
            vMesure.Add(new Measurement(DateTime.Parse("2017-01-03T10:09:07"), 35.01, MeasurementType.TEMP));
            vMesure.Add(new Measurement(DateTime.Parse("2017-01-03T10:03:34"), 96.49, MeasurementType.SPO2));
            vMesure.Add(new Measurement(DateTime.Parse("2017-01-03T10:02:01"), 35.82, MeasurementType.TEMP));
            vMesure.Add(new Measurement(DateTime.Parse("2017-01-03T10:05:00"), 97.17, MeasurementType.SPO2));
            vMesure.Add(new Measurement(DateTime.Parse("2017-01-03T10:05:01"), 95.08, MeasurementType.SPO2));

            Dictionary<MeasurementType, List<Measurement>> vResult = sample(new DateTime(), vMesure);

            foreach (Measurement v in vMesure)
            {
                v.PrintData();
            }
        }

        // start of sampling (from when to take samples, unsampledMeasurements list of measurements
        public static Dictionary<MeasurementType, List<Measurement>> sample(DateTime startOfSampling, List<Measurement> unsampledMeasurements)
        {
            Dictionary<MeasurementType, List<Measurement>> vReturnDict = new Dictionary<MeasurementType, List<Measurement>>();
            /*
             * each type of measurement shall be sampled separately
             * from a 5-minute interval only the last measurement shall be taken // 300
             * if a measurement timestamp will exactly match a 5-minute interval border, it shall be used
                for the current interval
             * the input values are not sorted by time
             * the output shall be sorted by time ascending
             */
            
            List<Measurement> vMeasureTEMP = new List<Measurement>();
            List<Measurement> vMeasureSPO2 = new List<Measurement>();
            List<Measurement> vMeasureRATE = new List<Measurement>();

            foreach (Measurement unsampledMeasure in unsampledMeasurements)
            {
                if (unsampledMeasure.GetMeasurementType() == MeasurementType.TEMP)
                {
                    vMeasureTEMP.Add(unsampledMeasure);
                }
                else if (unsampledMeasure.GetMeasurementType() == MeasurementType.SPO2)
                {
                    vMeasureSPO2.Add(unsampledMeasure);
                }
                else
                {
                    vMeasureRATE.Add(unsampledMeasure);
                }
            }

            filterSample(vMeasureTEMP);

            return vReturnDict;
        }

        private static List<Measurement> filterSample(List<Measurement> unfilteredSample)
        {
            List<Measurement> sampleList = new List<Measurement>();

            foreach(Measurement sample in unfilteredSample)
            {

            }

            return sampleList;
        }
    }
}