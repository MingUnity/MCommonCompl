using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MingUnity.AssetBundles;
using System.IO;

public class DepDemo : MonoBehaviour
{
    private DepAssetBundleLoader _loader = new DepAssetBundleLoader();

    private void Start()
    {
        string manifest = Path.Combine(Application.streamingAssetsPath, "AssetBundle/AssetBundle");

        //_loader.LoadManifestAssetBundle(manifest);

        string image1Path = Path.Combine(Application.streamingAssetsPath, "AssetBundle/image1.assetbundle");

        string image2Path = Path.Combine(Application.streamingAssetsPath, "AssetBundle/image2.assetbundle");

        string spherePath = Path.Combine(Application.streamingAssetsPath, "AssetBundle/sphere");

        _loader.LoadManifestAssetBundleAsync(manifest,()=>
        {
            //_loader.GetAssetAsync<GameObject>(image1Path, "Image1", (obj) => { GameObject.Instantiate(obj, this.transform); }, (progress) => { Debug.Log("Image1 " + progress); });

            //_loader.GetAssetAsync<GameObject>(image2Path, "Image2", (obj) => { GameObject.Instantiate(obj, this.transform); }, (progress) => { Debug.Log("Image2 " + progress); });

            _loader.GetAssetAsync<GameObject>(spherePath, "Sphere", (obj) => { GameObject.Instantiate(obj); }, (progress) => { Debug.Log("Sphere " + progress); });

            _loader.GetAssetAsync<GameObject>(spherePath, "Sphere", (obj) => { GameObject.Instantiate(obj); }, (progress) => { Debug.Log("Sphere " + progress); });
        },(progress)=> { Debug.Log("Manifest " + progress); });

        


        //GameObject image1 = _loader.GetAsset<GameObject>(image1Path, "Image1");

        //GameObject.Instantiate(image1, this.transform);

        //GameObject image2 = _loader.GetAsset<GameObject>(image2Path, "Image2");

        //GameObject.Instantiate(image2, this.transform);

        //_loader.GetAssetAsync<GameObject>(image1Path, "Image1", (obj) => { GameObject.Instantiate(obj, this.transform); }, (progress) => { Debug.Log("Image1 " + progress); });

        //_loader.GetAssetAsync<GameObject>(image2Path, "Image2", (obj) => { GameObject.Instantiate(obj, this.transform); }, (progress) => { Debug.Log("Image2 " + progress); });
    }
}
