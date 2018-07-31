using System;
using System.Collections.Generic;

namespace Thermometer_Class_Library
{
    public class Termometer
    {
        const string Celsius = "Celsius";
        const string Fahrenheit = "Fahrenheit";
        double PreviousTemperature;
        double NormalizedTemperature;
        public double CurrentTemperature;
        public ThermometerProperties ThermometerProperties = new ThermometerProperties();
        public List<double> HistoricalTemperatures = new List<double>();
        public List<Threshold> NewlyReachedThresholds = new List<Threshold>();

        public void CreateThermometerThreshold(string thresholdStatus, double thresholdValue, double temperatureTolerance, bool sensativeToRisingEdge, bool sensativeToFallingEdge)
        {
            ThermometerProperties.Thresholds.Add(new Threshold(thresholdStatus, thresholdValue, temperatureTolerance, sensativeToRisingEdge, sensativeToFallingEdge));
        }

        public void StorePreviousTemperature()
        {
            PreviousTemperature = NormalizedTemperature;
        }

        public void RegisterTemperatureChange(double temperatureReading, string measurementUnits, string displayUnits)
        {
            NewlyReachedThresholds.Clear();
            UpdateThermometerUnits(measurementUnits, displayUnits);
            StorePreviousTemperature();

            NormalizedTemperature = ConvertUnits(temperatureReading, measurementUnits, Celsius);
            NewlyReachedThresholds = GetNewlyReachedThresholds();
            CheckIfThresholdsAreStillReached();
            CurrentTemperature = ConvertUnits(NormalizedTemperature, Celsius, displayUnits);
        }

        public void UpdateThermometerUnits(string measurementUnits, string displayUnits)
        {
            ThermometerProperties.InputIsCelsius = measurementUnits.Equals(Celsius);
            ThermometerProperties.DisplayInCelsius = displayUnits.Equals(Celsius);
        }

        //array of data points
        public void SetupDefaultThresholds()
        {
            CreateThermometerThreshold("Boiling", 100, 5, true, false);
            CreateThermometerThreshold("Freezing", 0, 1, false, true);

        }

        public double ConvertUnits(double temperatureReading, string measurementUnits, string displayUnits)
        {

            if (measurementUnits == "" || displayUnits == "")
            {
                return temperatureReading;
            }

            if (measurementUnits == Celsius && displayUnits == Fahrenheit)
            {

                return Math.Round((temperatureReading * 1.8) + 32, 2);

            }

            if (measurementUnits == Fahrenheit && displayUnits == Celsius)
            {
                return Math.Round((temperatureReading - 32) * 0.5556, 2);
            }

            if ((measurementUnits == Celsius && displayUnits == Celsius) || (measurementUnits == Fahrenheit && displayUnits == Fahrenheit))
            {
                return Math.Round(temperatureReading, 2);
            }

            throw new InvalidCastException();
        }

        public List<Threshold> GetNewlyReachedThresholds()
        {
            var newlyReachedThresholds = new List<Threshold>();
            foreach (var threshold in ThermometerProperties.Thresholds)
            {
                if (threshold.IsReached) continue;

                if (NormalizedTemperature >= threshold.ThresholdValueCelsius && PreviousTemperature < threshold.ThresholdValueCelsius && threshold.SensitiveToRisingEdge)
                {
                    threshold.IsReached = true;
                    newlyReachedThresholds.Add(threshold);
                }

                else if (NormalizedTemperature <= threshold.ThresholdValueCelsius && PreviousTemperature > threshold.ThresholdValueCelsius && threshold.SensitiveToFallingEdge)
                {
                    threshold.IsReached = true;
                    newlyReachedThresholds.Add(threshold);
                }
            }
            return newlyReachedThresholds;
        }


        public void CheckIfThresholdsAreStillReached()
        {
            foreach (var threshold in ThermometerProperties.Thresholds)
            {
                if (threshold.IsReached)
                {
                    IsThresholdStillReached(threshold);
                }
            }
        }

        public bool IsThresholdStillReached(Threshold threshold)
        {

            if (threshold.SensitiveToRisingEdge && NormalizedTemperature < threshold.TempTolerance.LowerBand)
            {
                threshold.IsReached = false;
                return false;
            }
            if (threshold.SensitiveToFallingEdge && NormalizedTemperature > threshold.TempTolerance.UpperBand)
            {
                threshold.IsReached = false;
                return false;
            }
            threshold.IsReached = true;
            return true;
        }
    }


}
