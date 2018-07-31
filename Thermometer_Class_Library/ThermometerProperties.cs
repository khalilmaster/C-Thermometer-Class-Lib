using System.Collections.Generic;

namespace Thermometer_Class_Library
{
    public class ThermometerProperties
    {
        public List<Threshold> Thresholds = new List<Threshold>();
        public bool DisplayInCelsius;
        public bool InputIsCelsius;
    }
}