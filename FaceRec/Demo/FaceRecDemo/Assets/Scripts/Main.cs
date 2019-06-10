using MingUnity.FaceRec;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    private FaceDetect _faceDetect;

    private FaceSearch _faceSearch;

    private string _faceNum;

    private string _user;

    private string _score;

    private void Start()
    {
        FaceAppData faceAppData = new FaceAppData("11253066", "WwnwTfmq9ulkzknDBOv9tr6s", "7DNbqdtYvhVr0nR8YMtbIUeFfwyCBVgc");

        _faceDetect = new FaceDetect(faceAppData);

        _faceSearch = new FaceSearch("FaceRec", faceAppData);
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

        if (GUILayout.Button("FaceSearch"))
        {
            string imgPath = Path.Combine(Application.dataPath, "Resources/Test.jpg");

            FaceSearchRes res = _faceSearch.Search(imgPath);

            if (res != null)
            {
                _user = res.result.user_list[0].user_id;

                float score = res.result.user_list[0].score;

                _score = score.ToString();
            }
        }

        GUILayout.Label(_user);

        GUILayout.Label(_score);
    }
}
