using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sampling;
using System.Diagnostics.Metrics;

namespace SamplingTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFilterSample()
        {
            Dictionary<MeasurementType, List<Measurement>> pExpectedSampledData = new Dictionary<MeasurementType, List<Measurement>>();

            List<Measurement> sampledDataTEMP = new List<Measurement>();
            List<Measurement> sampledDataSPO2 = new List<Measurement>();
            List<Measurement> sampledDataRATE = new List<Measurement>();

            sampledDataTEMP.Add(new Measurement(DateTime.Parse("2017-01-03T10:05:00"), 35.79, MeasurementType.TEMP));
            sampledDataTEMP.Add(new Measurement(DateTime.Parse("2017-01-03T10:10:00"), 35.01, MeasurementType.TEMP));

            sampledDataSPO2.Add(new Measurement(DateTime.Parse("2017-01-03T10:05:00"), 97.17, MeasurementType.SPO2));
            sampledDataSPO2.Add(new Measurement(DateTime.Parse("2017-01-03T10:10:00"), 95.08, MeasurementType.SPO2));

            sampledDataRATE.Add(new Measurement(DateTime.Parse("2017-01-03T10:05:00"), 78.33, MeasurementType.RATE));
            sampledDataRATE.Add(new Measurement(DateTime.Parse("2017-01-03T10:10:00"), 74.08, MeasurementType.RATE));

            pExpectedSampledData.Add(MeasurementType.TEMP, sampledDataTEMP);
            pExpectedSampledData.Add(MeasurementType.SPO2, sampledDataSPO2);
            pExpectedSampledData.Add(MeasurementType.RATE, sampledDataRATE);

            DateTime vSampleStartDate = DateTime.Parse("2017-01-03T10:00:00");
            List<Measurement> vSamplingTestData = new List<Measurement>();

            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:04:45"), 35.79, MeasurementType.TEMP));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:09:07"), 35.01, MeasurementType.TEMP));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:03:34"), 96.49, MeasurementType.SPO2));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:01:18"), 98.78, MeasurementType.SPO2));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:02:01"), 35.82, MeasurementType.TEMP));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:05:00"), 97.17, MeasurementType.SPO2));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:05:01"), 95.08, MeasurementType.SPO2));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:09:21"), 74.08, MeasurementType.RATE));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:04:59"), 78.33, MeasurementType.RATE));
            vSamplingTestData.Add(new Measurement(DateTime.Parse("2017-01-03T10:04:01"), 76.21, MeasurementType.RATE));

            Dictionary<MeasurementType, List<Measurement>> vSampledData = Sampling.Program.sample(vSampleStartDate, vSamplingTestData);

            // wasn't able to make working Assert.AreEqual(pExpectedSampledData, vSampledData)

            Assert.AreEqual(pExpectedSampledData.Count(), vSampledData.Count());
            
            foreach (var _key in pExpectedSampledData.Keys)
            {
                foreach (var _value in pExpectedSampledData[_key])
                {
                    Assert.AreEqual(_value.GetMeasurementValue(), vSampledData[_key][pExpectedSampledData[_key].IndexOf(_value)].GetMeasurementValue());
                    Assert.AreEqual(_value.GetMeasurementTime(), vSampledData[_key][pExpectedSampledData[_key].IndexOf(_value)].GetMeasurementTime());
                    Assert.AreEqual(_value.GetMeasurementType(), vSampledData[_key][pExpectedSampledData[_key].IndexOf(_value)].GetMeasurementType());
                }
            }
            
        }
    }
}