using MingUnity.MVVM;
using System;
using UnityEngine;

public class TestView_01 : ViewBase
{
    public override bool Active
    {
        get
        {
            return _root.gameObject.activeSelf;
        }
    }

    public override void Create(Transform parent, bool active, Action callback = null)
    {
        GameObject prefab = Resources.Load<GameObject>("TestView_01");

        if (prefab != null)
        {
            _root = GameObject.Instantiate(prefab, parent).GetComponent<RectTransform>();

            SetActive(active);
        }

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    public override void Show(Action callback = null)
    {
        SetActive(true);

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    public override void Hide(Action callback = null)
    {
        SetActive(false);

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    protected override void ViewModelPropertyChanged(string propertyName, IPropertyChangedArgs args)
    {

    }

    private void SetActive(bool active)
    {
        _root.gameObject.SetActive(active);
    }
}
