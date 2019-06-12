using UnityEngine;
using Ming.Speech;

public class Main : MonoBehaviour
{
    private string _filePath = string.Empty;

    private string _recRes = string.Empty;

    private ISpeechRec _rec;

    private void Start()
    {
        _rec = new SpeechRecer( new SpeechAppData("15470261", "Dlrj6PcxtjXbzaKAO2Fp0ZuA", "oYt0ED8bwrkXKh6WR1c8XYdMt4EWiEYZ"));
    }

    private void OnGUI()
    {
        _filePath = GUILayout.TextField(_filePath);

        if (GUILayout.Button("SpeechRec"))
        {
            _rec.AsyncSpeechRec(_filePath, (res) =>
             {
                 if (res != null && res.result != null && res.result.Count > 0)
                 {
                     _recRes = res.result[0];
                 }
             });
        }

        GUILayout.Label(_recRes);
    }
}
