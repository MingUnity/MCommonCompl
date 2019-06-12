using UnityEngine;
using Ming.Speech;
using System.IO;

public class Main : MonoBehaviour
{
    private string _content = string.Empty;

    private string _recRes = string.Empty;

    private string _dir = string.Empty;

    private void Awake()
    {
        _dir = Application.streamingAssetsPath;
    }

    private void OnGUI()
    {
        _content = GUILayout.TextField(_content);

        if (GUILayout.Button("SpeechRec"))
        {
            ISpeechRec rec = new SpeechRecer(new SpeechAppData("15470261", "Dlrj6PcxtjXbzaKAO2Fp0ZuA", "oYt0ED8bwrkXKh6WR1c8XYdMt4EWiEYZ"));

            rec.AsyncSpeechRec(_content, (res) =>
             {
                 if (res != null && res.result != null && res.result.Count > 0)
                 {
                     _recRes = res.result[0];
                 }
             });
        }

        GUILayout.Label(_recRes);

        if (GUILayout.Button("SpeechGenrate"))
        {
            ISpeechGenerator generator = new SpeechGenerator(new SpeechAppData("15470261", "Dlrj6PcxtjXbzaKAO2Fp0ZuA", "oYt0ED8bwrkXKh6WR1c8XYdMt4EWiEYZ"));
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            sw.Start();
            generator.AsyncSpeechGenrate(_content, bytes =>
            {
                Debug.Log(sw.ElapsedMilliseconds);

                if (bytes != null)
                {
                    File.WriteAllBytes(Path.Combine(_dir, "output.mp3"), bytes);
                }
            });
        }
    }
}
