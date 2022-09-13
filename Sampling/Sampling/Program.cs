using System.Data;
using System.Linq;

namespace Sampling
{
    public class Program 
    {
        public static void Main()
        {
        }

        public static Dictionary<MeasurementType, List<Measurement>> sample(DateTime startOfSampling, List<Measurement> unsampledMeasurements)
        {
            Dictionary<MeasurementType, List<Measurement>> vReturnDict = new Dictionary<MeasurementType, List<Measurement>>();

            List<Measurement> vMeasureTEMP = new List<Measurement>();
            List<Measurement> vMeasureSPO2 = new List<Measurement>();
            List<Measurement> vMeasureRATE = new List<Measurement>();

            int vMeasurementStepMinutes = 5;

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
                else if (unsampledMeasure.GetMeasurementType() == MeasurementType.RATE)
                {
                    vMeasureRATE.Add(unsampledMeasure);
                }
            }

            List<Measurement> vMeasureTEMPF = filterSample(vMeasureTEMP, startOfSampling, vMeasurementStepMinutes);
            List<Measurement> vMeasureSPO2F = filterSample(vMeasureSPO2, startOfSampling, vMeasurementStepMinutes);
            List<Measurement> vMeasureRATEF = filterSample(vMeasureRATE, startOfSampling, vMeasurementStepMinutes);

            vReturnDict.Add(MeasurementType.TEMP, vMeasureTEMPF);
            vReturnDict.Add(MeasurementType.SPO2, vMeasureSPO2F);
            vReturnDict.Add(MeasurementType.RATE, vMeasureRATEF);

            return vReturnDict;
        }

        private static List<Measurement> filterSample(List<Measurement> unfilteredSample, DateTime startOfSample, int minuteStep)
        {
            List<Measurement> sampleList = new List<Measurement>();

            for (int i = 0; i < unfilteredSample.Count(); i++)
            {
                DateTime stepStart = startOfSample.AddMinutes(i * minuteStep);
                DateTime stepEnd = startOfSample.AddMinutes((i + 1) * minuteStep);

                foreach (Measurement measure in unfilteredSample)
                {
                    if (stepStart < measure.GetMeasurementTime() && measure.GetMeasurementTime() <= stepEnd)
                    {
                        if (!sampleList.Where(x => x.GetMeasurementTime() > stepStart).Any())
                        {
                            sampleList.Add(new Measurement(stepEnd, measure.GetMeasurementValue(), measure.GetMeasurementType()));
                        }
                        else if (sampleList.Where(x => x.GetMeasurementTime() > stepStart && x.GetMeasurementTime() <= measure.GetMeasurementTime()).Any())
                        {
                            int replaceIndex = sampleList.FindIndex(x => x.GetMeasurementTime() > stepStart && x.GetMeasurementTime() <= measure.GetMeasurementTime());

                            if (replaceIndex > -1)
                            {
                                sampleList[replaceIndex] = new Measurement(stepEnd, measure.GetMeasurementValue(), measure.GetMeasurementType());  
                            }
                        }
                    }
                }
            }

            return sampleList;
        }
    }
}