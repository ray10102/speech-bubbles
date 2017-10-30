using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorSelect : EventTrigger {

    [SerializeField]
    private ASoundField.Color color;
    private GameObject highlight;

    private void Start() {
        highlight = transform.GetChild(0).gameObject;
        if (ASoundField.selectedColor == color) {
            highlight.SetActive(true);
        } else {
            highlight.SetActive(false);
        }
    }

    public override void OnPointerDown(PointerEventData data) {
        ColorSelectManager.instance.SelectColor(color);
    }

    public void SelectThisColor() {
        SetHighlight(true);
        SetSelectedColor();
    }

    public void SetHighlight(bool on) {
        highlight.SetActive(on);
    }

    private void SetSelectedColor() {
        ASoundField.selectedColor = color;
    }
}
