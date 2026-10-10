using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneLook : MonoBehaviour
{
    private AttitudeSensor attitudeSensor;

    // The phone sensor's neutral orientation does not match
    // an upright Unity camera. Rotate that coordinate frame
    // by 90 degrees around X.
    private static readonly Quaternion SensorToCamera =
        Quaternion.Euler(90f, 0f, 0f);

    private void OnEnable()
    {
        InputSystem.settings.compensateForScreenOrientation = true;

        attitudeSensor = AttitudeSensor.current;

        if (attitudeSensor == null)
        {
            Debug.LogWarning("No attitude sensor available on this device.");
            return;
        }

        InputSystem.EnableDevice(attitudeSensor);
    }

    private void OnDisable()
    {
        if (attitudeSensor != null && attitudeSensor.enabled)
        {
            InputSystem.DisableDevice(attitudeSensor);
        }
    }

    private void Update()
    {
        if (attitudeSensor == null || !attitudeSensor.enabled)
            return;

        Quaternion attitude = attitudeSensor.attitude.ReadValue();

        Quaternion unityAttitude = new Quaternion(
            attitude.x,
            attitude.y,
            -attitude.z,
            -attitude.w
        );

        transform.localRotation = SensorToCamera * unityAttitude;
    }
}
