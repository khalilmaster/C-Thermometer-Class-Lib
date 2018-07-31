namespace Thermometer_Class_Library
{
    public class Threshold
    {
        public string ThresholdStatus;
        public double ThresholdValueCelsius;
        public bool SensitiveToRisingEdge;
        public bool SensitiveToFallingEdge;
        public bool IsReached;
        public TemperatureTolerance TempTolerance;

        public Threshold(string thresholdStatus, double thresholdValue, double temperatureTolerance, bool sensitiveToRisingEdge, bool sensitiveToFallingEdge)
        {
            ThresholdStatus = thresholdStatus;
            ThresholdValueCelsius = thresholdValue;
            SensitiveToRisingEdge = sensitiveToRisingEdge;
            SensitiveToFallingEdge = sensitiveToFallingEdge;
            TempTolerance = new TemperatureTolerance(temperatureTolerance, thresholdValue);
        }
    }
}