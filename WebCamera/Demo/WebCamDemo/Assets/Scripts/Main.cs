using UnityEngine;
using UnityEngine.UI;
using MingUnity.WebCamera;

public class Main : MonoBehaviour
{
    private string _camIndex = "0";

    public RawImage imgCamera;

    public RawImage imgSnapshot;

    private void Start()
    {
        WebCamera.Initialize((result) =>
        {
            Debug.LogFormat("WebCamera init {0}", result ? "success" : "fail");
        });
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        _camIndex = GUILayout.TextField(_camIndex);

        if (GUILayout.Button("Open"))
        {
            int camIndex = 0;

            if (int.TryParse(_camIndex, out camIndex))
            {
                WebCamera webCam = WebCamera.Get(camIndex);

                webCam.Open(1280, 720);

                if (imgCamera != null)
                {
                    imgCamera.texture = webCam.CamTexture;
                }
            }
        }

        if (GUILayout.Button("Close"))
        {
            int camIndex = 0;

            if (int.TryParse(_camIndex, out camIndex))
            {
                WebCamera.Get(camIndex).Close();

                if (imgCamera != null)
                {
                    imgCamera.texture = null;
                }
            }
        }

        if (GUILayout.Button("Reconnect"))
        {
            int camIndex = 0;

            if (int.TryParse(_camIndex, out camIndex))
            {
                WebCamera.Get(camIndex).ReConnect(1280, 720);
            }
        }

        if (GUILayout.Button("Snapshot"))
        {
            int camIndex = 0;

            if (int.TryParse(_camIndex, out camIndex))
            {
                WebCamera.Get(camIndex).Snapshot((Texture2D tex) =>
                {
                    if (imgSnapshot != null)
                    {
                        imgSnapshot.texture = tex;
                    }
                });
            }
        }

        GUILayout.EndVertical();
    }
}
