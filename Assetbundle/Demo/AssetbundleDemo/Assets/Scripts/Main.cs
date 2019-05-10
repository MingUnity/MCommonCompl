using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MingUnity.AssetbundleModule;
using System.IO;

public class Main : MonoBehaviour
{
    public Transform canvas;

    private IAssetbundleLoader _abLoader;

    private void Start()
    {
        _abLoader = new AsyncAssetBundleLoader();

    }

    private void OnGUI()
    {
        if (GUILayout.Button("Cube"))
        {
            string abPath = string.Format("file:///{0}/AssetBundle/3dobj", Application.streamingAssetsPath);

            _abLoader.GetAsset<GameObject>(abPath, "Cube", (obj) =>
           {
               if (obj != null)
               {
                   GameObject.Instantiate(obj);
               }
           });
        }

        if (GUILayout.Button("Sphere"))
        {
            string abPath = string.Format("file:///{0}/AssetBundle/3dobj", Application.streamingAssetsPath);

            _abLoader.GetAsset<GameObject>(abPath, "Sphere", (obj) =>
            {
                if (obj != null)
                {
                    GameObject.Instantiate(obj);
                }
            });
        }

        if (GUILayout.Button("Image"))
        {
            string abPath = string.Format("file:///{0}/AssetBundle/image", Application.streamingAssetsPath);

            _abLoader.GetAsset<GameObject>(abPath, "Image", (obj) =>
            {
                if (obj != null)
                {
                    GameObject.Instantiate(obj, canvas.transform);
                }
            });
        }
    }
}
