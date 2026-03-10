using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.PXR;

public class TogglePassthrough : MonoBehaviour
{
    [Header("Input Settings")]
    [Tooltip("Which button toggles the passthrough? (e.g., Primary Button on Left Controller)")]
    public InputActionReference toggleAction;

    private bool _isPassthroughEnabled = true;

    void Start()
    {
        // Set the initial state based on our variable
        ApplyPassthroughState();
    }

void OnEnable()
{
    toggleAction.action.Enable();
}
    void Update()
    {
        // Check if the button was pressed this frame
        if (toggleAction.action.WasPressedThisFrame())
        {
            _isPassthroughEnabled = !_isPassthroughEnabled;
            ApplyPassthroughState();
        }
    }

    private void ApplyPassthroughState()
    {
        // tells the PICO OS to show or hide the cameras
        PXR_Manager.EnableVideoSeeThrough = _isPassthroughEnabled;
        
        Debug.Log($"PICO MR: Passthrough is now {(_isPassthroughEnabled ? "ENABLED" : "DISABLED")}");
    }

    // Crucial: Re-enable passthrough when the user returns to the app from the PICO menu
    private void OnApplicationPause(bool pause)
    {
        if (!pause && _isPassthroughEnabled)
        {
            PXR_Manager.EnableVideoSeeThrough = true;
        }
    }
}