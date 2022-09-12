using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampling
{
    public enum MeasurementType
    {
        TEMP,
        SPO2,
        RATE
    }

    public class Measurement
    {
        private DateTime measurementTime;
        private Double measurementValue;
        private MeasurementType type;

        public Measurement(DateTime measurementTime, double measurementValue, MeasurementType type)
        {
            this.measurementTime = measurementTime;
            this.measurementValue = measurementValue;
            this.type = type;
        }

        public void PrintData()
        {
            Console.WriteLine("{0}, {1}, {2}", this.measurementTime, this.measurementValue, this.type);
        }
    }
}
