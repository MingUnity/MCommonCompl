using MingUnity.FaceRec;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    private FaceDetect _faceDetect;

    private string _faceNum;

    private void Start()
    {
        _faceDetect = new FaceDetect(new FaceAppData("11253066", "WwnwTfmq9ulkzknDBOv9tr6s", "7DNbqdtYvhVr0nR8YMtbIUeFfwyCBVgc"));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("FaceRec"))
        {
            string imgPath = Path.Combine(Application.dataPath, "Resources/Test.jpg");

            FaceDetectRes res = _faceDetect.Detect(imgPath);

            if (res != null)
            {
                int faceNum = res.result.face_num;

                _faceNum = faceNum.ToString();
            }
        }

        GUILayout.Label(_faceNum);
    }
}
