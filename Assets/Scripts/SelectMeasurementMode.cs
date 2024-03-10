using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SelectMeasurementMode : MonoBehaviour
{
    [SerializeField] private RotateSwitch rotateSwitch;
    [SerializeField] private TextMeshProUGUI displayOutput;
    [SerializeField] private TextMeshProUGUI ohmmeterValue;
    [SerializeField] private TextMeshProUGUI ammeterValue;
    [SerializeField] private TextMeshProUGUI directVoltageValue;
    [SerializeField] private TextMeshProUGUI alternativeVoltageValue;

    const double DeviceResistance = 1000;
    const double DevicePower = 400;
    const double AlternativeVoltage = 0.01;
    const double NeutralValue = 0;


    /// <summary>
    /// Reset all interface values and subscribe to select mode action.
    /// </summary>
    void Awake()
    {
        ResetMeasurement();
        rotateSwitch.OnSelectMode += OnSelectMode;
    }

    /// <summary>
    /// Unsubscribe of select mode action.
    /// </summary>
    private void OnDestroy()
    {
        rotateSwitch.OnSelectMode -= OnSelectMode;
    }

    /// <summary>
    /// Takes multimeter mode as input, depending on the mode, calls a method to display values on the screen.
    /// </summary>
    /// <param name="mode">multimeter mode</param>
    private void OnSelectMode(MultimeterModeAngle mode)
    {
        switch (mode)
        {
            case MultimeterModeAngle.Ammeter:
                ResetMeasurement();
                TurnAmmeterMode();
                break;
            case MultimeterModeAngle.Ohmmeter:
                ResetMeasurement();
                TurnOhmmeterMode();
                break;
            case MultimeterModeAngle.DirectVoltmeter:
                ResetMeasurement();
                TurnDirectVoltmeterMode();
                break;
            case MultimeterModeAngle.AlternativeVoltmeter:
                ResetMeasurement();
                TurnAlternativeVoltmeterMode();
                break;
            default:
                ResetMeasurement();
                displayOutput.text = NeutralValue.ToString("F2");
                break;
        }
    }


    // Next methods calculates values for current multimeter mode and display om the screen.
    private void TurnAmmeterMode()
    {
        double ampere = Math.Sqrt(DevicePower / DeviceResistance);
        displayOutput.text = ampere.ToString("F2");
        ammeterValue.text = "A  " + ampere.ToString("F2");
    }

    private void TurnOhmmeterMode()
    {
        displayOutput.text = DeviceResistance.ToString("F2");
        ohmmeterValue.text = "Ω  " + DeviceResistance.ToString("F2");
    }

    private void TurnDirectVoltmeterMode()
    {
        double voltage = Math.Sqrt(DevicePower * DeviceResistance);
        displayOutput.text = voltage.ToString("F2");
        directVoltageValue.text = "V-  " + voltage.ToString("F2");
    }

    private void TurnAlternativeVoltmeterMode()
    {
        displayOutput.text = AlternativeVoltage.ToString("F2");
        alternativeVoltageValue.text = "V~  " + AlternativeVoltage.ToString("F2");
    }

    // Reset all values on the screen.
    private void ResetMeasurement()
    {
        displayOutput.text = NeutralValue.ToString("F2");
        ammeterValue.text = "A   0";
        ohmmeterValue.text = "Ω   0";
        directVoltageValue.text = "V-  0";
        alternativeVoltageValue.text = "V~  0";
    }
}

public enum MultimeterModeAngle
{
    Neutral = 0,
    Ammeter = 1,
    DirectVoltmeter = 2,
    AlternativeVoltmeter = 3,
    Ohmmeter = 4,
}