using System.Collections.Generic;

using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _allPatterns
    //   _currentPattern
    //   _currentPatternIndex
    //   _dropdownPatterns
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private Pattern[]    _allPatterns;
    private Pattern      _currentPattern;
    private int          _currentPatternIndex = 0;
    private TMP_Dropdown _dropdownPatterns;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   OnButtonNextClicked
    //   OnButtonPreviousClicked
    //   OnButtonPlayClicked
    //   OnDropdownValueChanged
    //   GetCurrentPattern
    // -------------------------------------------------------------------------

    #region .  OnButtonNextClicked()  .
    public void OnButtonNextClicked()
    {
        this._currentPatternIndex = ++this._dropdownPatterns.value % this._allPatterns.Length;
        this._currentPattern      =   this._allPatterns[this._currentPatternIndex];

        this.OnButtonPlayClicked();

    }   // OnButtonNextClicked()
    #endregion


    #region .  OnButtonPreviousClicked()  .
    public void OnButtonPreviousClicked()
    {
        this._currentPatternIndex = --this._dropdownPatterns.value % this._allPatterns.Length;
        this._currentPattern      =   this._allPatterns[this._currentPatternIndex];

        this.OnButtonPlayClicked();

    }   // OnButtonPreviousClicked()
    #endregion


    #region .  OnButtonPlayClicked()  .
    public void OnButtonPlayClicked()
    {
        GameBoard.Instance.Play(this._currentPattern);

    }   // ButtonRestartClicked()
    #endregion


    #region .  OnDropdownValueChanged()  .
    public void OnDropdownValueChanged()
    {
        // Handle the value change.
        this._currentPattern = GetCurrentPattern();

        Debug.Log($"New Selected Option:  {this._currentPattern.name}");

        GameBoard.Instance.Play(this._currentPattern);

    }   // OnDropdownValueChanged()

    #endregion


    #region .  GetCurrentPattern()  .
    public Pattern GetCurrentPattern()
    {
        return this._allPatterns[_dropdownPatterns.value];
    }
    // GetPattern()
    #endregion



    // -------------------------------------------------------------------------    
    // Private Methods:
    // ----------------
    //   Awake()
    // -------------------------------------------------------------------------

    #region .  Awake()  .

    private void Awake()
    {
        this._dropdownPatterns = FindObjectOfType<TMP_Dropdown>();
        this._allPatterns      = Pattern.CreateInstance<Pattern>().AllPatterns;

        // Clear existing options.
        this._dropdownPatterns.ClearOptions();

        // Create a list of new options.
        List<TMP_Dropdown.OptionData> patternsList = new List<TMP_Dropdown.OptionData>();

        foreach (Pattern pattern in this._allPatterns)
        {
            // Create a new option and add it to the Dropdown.
            //var newOption = new List<TMP_Dropdown.OptionData>
            //{
            //    new TMP_Dropdown.OptionData(pattern.name)
            //};
            //this._dropdownPatterns.AddOptions(newOption);

            patternsList.Add(new TMP_Dropdown.OptionData(pattern.name));

            this._dropdownPatterns.AddOptions(
                new List<TMP_Dropdown.OptionData>
                {
                    new TMP_Dropdown.OptionData(pattern.name)
                }
            );
        }

        // Add the patterns and set the default values.
        this._dropdownPatterns.AddOptions(patternsList);
        this._dropdownPatterns.value = 0;

        this._currentPattern = this._allPatterns[0];

    }   // Awake()

    #endregion


}   // class UIManager
