namespace Thermometer_Class_Library
{
    public class TemperatureTolerance
    {
        public double ToleranceValue;
        public double UpperBand;
        public double LowerBand;

        public TemperatureTolerance(double tempTolerance, double thresholdTemperature)
        {
            ToleranceValue = tempTolerance;
            UpperBand = thresholdTemperature + ToleranceValue;
            LowerBand = thresholdTemperature - ToleranceValue;
        }
    }
}