using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelectManager : MonoBehaviour {
    [SerializeField]
    private ColorSelect none, red, yellow, green, blue, purple;
    private static ColorSelect currentColor;

    public static ColorSelectManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        currentColor = GetColorSelect(ASoundField.selectedColor);
    }

    public void SelectColor(ASoundField.Color color) {
        currentColor.SetHighlight(false);
        currentColor = GetColorSelect(color);
        currentColor.SelectThisColor();
    }

    private ColorSelect GetColorSelect(ASoundField.Color color) {
        switch(color) {
            case ASoundField.Color.RED:
                return red;
            case ASoundField.Color.YELLOW:
                return yellow;
            case ASoundField.Color.GREEN:
                return green;
            case ASoundField.Color.BLUE:
                return blue;
            case ASoundField.Color.PURPLE:
                return purple;
            case ASoundField.Color.NONE:
                return none;
        }
        return null;
    }
}
