using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RotateSwitch : MonoBehaviour
{
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private PlayerController playerController;

    // Get value of the selected multimeter mode.
    public UnityAction<MultimeterModeAngle> OnSelectMode;

    // Default index of _listOfEnumAngle list.
    private int _index = 0;

    // Storage of multimeter modes and angles
    private readonly Dictionary<int, MultimeterModeAngle> _angleOfMode = new Dictionary<int, MultimeterModeAngle>
    {
        { -45, MultimeterModeAngle.Neutral },
        { 0, MultimeterModeAngle.DirectVoltmeter },
        { 90, MultimeterModeAngle.AlternativeVoltmeter },
        { -180, MultimeterModeAngle.Ammeter },
        { -90, MultimeterModeAngle.Ohmmeter }
    };

    private List<int> _listOfEnumAngle;

    /// <summary>
    /// Subscribe to the playerController action and get list of angles.
    /// </summary>
    void Awake()
    {
        _listOfEnumAngle = _angleOfMode.Keys.ToList();
        playerController.OnScroll += mouseScroll => GetRotationMode(mouseScroll);
    }

    /// <summary>
    /// Unsubscribe of the playerController action.
    /// </summary>
    private void OnDestroy()
    {
        playerController.OnScroll -= mouseScroll => GetRotationMode(mouseScroll);
    }


    /// <summary>
    ///  Takes the direction of mouse scroll as input, rotate an object by the given angle
    ///  and send multimeter mode to onSelectMode.
    /// </summary>
    private void GetRotationMode(int directionOfMouseScroll)
    {
        float rotationAngle = GetRotationAngle(directionOfMouseScroll);
        objectToRotate.transform.rotation = Quaternion.Euler(Vector3.up * rotationAngle);
        int dictionaryKey = Convert.ToInt32(rotationAngle);
        OnSelectMode.Invoke(_angleOfMode[dictionaryKey]);
    }


    /// <summary>
    ///  Takes as input the value to which the list index needs to be changed,
    ///  returns the rotation angle by new index.
    /// </summary>
    private float GetRotationAngle(int numToAdd)
    {
        switch (numToAdd)
        {
            case 1:
                _index = (_index + numToAdd >= _listOfEnumAngle.Count) ? 0 : _index + numToAdd;
                return _listOfEnumAngle[_index];

            case -1:
                _index = (_index + numToAdd < 0) ? _listOfEnumAngle.Count - 1 : _index + numToAdd;
                return _listOfEnumAngle[_index];
            default:
                return 0;
        }
    }
}