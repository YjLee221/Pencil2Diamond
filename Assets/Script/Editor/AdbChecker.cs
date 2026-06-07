using System.Diagnostics;
using Unity.Android.Gradle;
using UnityEditor;
using UnityEngine;

public class AdbChecker : EditorWindow
{
    [MenuItem("Tools/Check ADB Devices")]
    public static void CheckDevices()
    {
        // 안드로이드 SDK 경로 가져오기 (환경에 따라 비어있을 수 있음)
        string androidSdkRoot = @"C:\Program Files\Unity\Hub\Editor\6000.4.0f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK";

        if (string.IsNullOrEmpty(androidSdkRoot))
        {
            UnityEngine.Debug.LogError("안드로이드 SDK 경로를 찾을 수 없어. Edit > Preferences > External Tools를 확인하거나 아래 adbPath를 절대 경로로 하드코딩해줘.");
            return;
        }

        string adbPath = androidSdkRoot + @"\platform-tools\adb.exe";

        // 외부 프로세스(ADB) 실행 설정
        ProcessStartInfo processInfo = new ProcessStartInfo(adbPath, "devices")
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        try
        {
            using (Process process = Process.Start(processInfo))
            {
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();

                // 결과를 유니티 콘솔창에 깔끔하게 출력
                UnityEngine.Debug.Log("<b>[ADB 연결된 기기 목록]</b>\n" + output);
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("ADB 실행 중 오류가 터졌어: " + e.Message);
        }
    }
}