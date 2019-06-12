using UnityEngine;
using MingUnity.VoiceInput;
using System.IO;

public class Main : MonoBehaviour
{
    private VoiceReceiver _receiver;

    private AudioSource _audioSource;

    private void Start()
    {
        _receiver = VoiceReceiver.Default;

        _audioSource = this.GetComponent<AudioSource>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            _receiver.StartRecord();
        }

        if (GUILayout.Button("Stop"))
        {
            _receiver.StopRecord((AudioClip clip) =>
            {
                if (clip != null)
                {
                    _audioSource.PlayOneShot(clip);
                }
            });
        }

        if (GUILayout.Button("Record"))
        {
            _receiver.Record(5, (buffer) =>
            {
                if (buffer != null)
                {
                    File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "output.wav"), buffer);
                }
            });
        }
    }
}
