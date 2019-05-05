using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MingUnity.FaceRec;
using System.IO;

public class Main : MonoBehaviour
{
    private FaceDetect _faceDetect;

    private string _faceNum;

    private void Start()
    {
        _faceDetect = new FaceDetect(new FaceAppData("11328070", "eFVTu2NNHCgIRmLsXU4l0SNQ", "eCmbZXcDkqN9CILrwUqikyL4TuzPdRWD"));
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
