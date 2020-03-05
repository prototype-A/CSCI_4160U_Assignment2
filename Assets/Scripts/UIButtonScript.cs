using UnityEngine;

public class UIButtonScript : MonoBehaviour {

    public FlexibleColorPicker colorPicker;
    private bool colorPickerOpen = false;

    public void ToggleColorPicker() {
        colorPickerOpen = !colorPickerOpen;
        colorPicker.gameObject.SetActive(colorPickerOpen);
    }
}
