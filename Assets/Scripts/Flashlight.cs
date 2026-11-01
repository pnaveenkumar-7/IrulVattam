using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light lightSource;
    public KeyCode toggleKey = KeyCode.F;

    public float maxBattery = 100f;
    public float drainPerSecond = 2f;      // 100 / 2 = about 50 seconds for testing; raise later
    public float lowBatteryPercent = 20f;

    public float battery;
    private bool isOn = false;
    private float baseIntensity;

    void Start()
    {
        battery = maxBattery;
        baseIntensity = lightSource.intensity;
        lightSource.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey) && battery > 0f)
        {
            isOn = !isOn;
            lightSource.enabled = isOn;
        }

        if (!isOn) return;

        battery -= drainPerSecond * Time.deltaTime;

        if (battery <= 0f)
        {
            battery = 0f;
            isOn = false;
            lightSource.enabled = false;
            return;
        }

        // Flicker when low
        if (battery / maxBattery * 100f <= lowBatteryPercent)
            lightSource.intensity = baseIntensity * Random.Range(0.3f, 1f);
        else
            lightSource.intensity = baseIntensity;
    }

    public void AddBattery(float amount)
    {
        battery = Mathf.Clamp(battery + amount, 0f, maxBattery);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 30), "Battery: " + Mathf.CeilToInt(battery) + "%");
    }
}