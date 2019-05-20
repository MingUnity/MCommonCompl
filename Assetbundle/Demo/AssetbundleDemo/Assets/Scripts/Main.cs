using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using MingUnity.AssetBundles;

public class Main : MonoBehaviour
{
    public Transform canvas;

    private IAssetBundleLoader _abLoader;

    private void Start()
    {
        _abLoader = new AssetBundleLoader();

    }

    private void OnGUI()
    {
        if (GUILayout.Button("Cube"))
        {
            string abPath = string.Format("file:///{0}/AssetBundle/3dobj", Application.streamingAssetsPath);

            _abLoader.GetAssetAsync<GameObject>(abPath, "Cube", (obj) =>
           {
               if (obj != null)
               {
                   GameObject.Instantiate(obj);
               }
           });

            string spherePath= string.Format("{0}/AssetBundle/3dobj", Application.streamingAssetsPath);

            GameObject sp = _abLoader.GetAsset<GameObject>(spherePath, "Sphere");

            if (sp != null)
            {
                GameObject.Instantiate(sp);
            }
        }

        if (GUILayout.Button("Sphere"))
        {
            string abPath = string.Format("file:///{0}/AssetBundle/3dobj", Application.streamingAssetsPath);

            _abLoader.GetAssetAsync<GameObject>(abPath, "Sphere", (obj) =>
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

            _abLoader.GetAssetAsync<GameObject>(abPath, "Image", (obj) =>
            {
                if (obj != null)
                {
                    GameObject.Instantiate(obj, canvas.transform);
                }
            });
        }
    }
}
