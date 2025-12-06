using UnityEngine;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    string folderName = "Nerve Screenshots";

    void Update() { if (Input.GetKeyDown(KeyCode.F12) || Input.GetKeyDown(KeyCode.F2)) { TakeScreenshot(); } }

    public void TakeScreenshot()
    {

        string autoName = $"nerve_screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
        string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string folderPath = Path.Combine(desktop, folderName);

        if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); } //create a folder if none

        string path = Path.Combine(folderPath, autoName);

        ScreenCapture.CaptureScreenshot(path);
        Debug.Log("Screenshot saved: " + path);
    }
}