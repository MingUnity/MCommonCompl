using MingUnity.AssetBundles;
using UnityEngine;

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
           },_=>
           {
               Debug.Log($"Cube Progress {_}");
           });
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
            }, _ =>
            {
                Debug.Log($"Sphere Progress {_}");
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
            }, _ =>
            {
                Debug.Log($"Image Progress {_}");
            });
        }
    }
}
