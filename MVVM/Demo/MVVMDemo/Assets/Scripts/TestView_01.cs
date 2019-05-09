using System;
using MingUnity.MVVM.View;
using UnityEngine;

public class TestView_01 : ViewBase<TestViewModel>
{
    public bool Active
    {
        get
        {
            bool res = false;

            if (_root != null)
            {
                res = _root.gameObject.activeSelf;
            }

            return res;
        }
        private set
        {
            if (_root != null)
            {
                _root.gameObject.SetActive(value);
            }
        }
    }

    public override void Create(Transform parent, Action callback)
    {
        GameObject prefab = Resources.Load<GameObject>("TestView_01");

        if (prefab != null)
        {
            _root = GameObject.Instantiate(prefab, parent).GetComponent<RectTransform>();
        }

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    protected override void PropertyChanged(string propertyName)
    {
        if (!string.IsNullOrEmpty(propertyName))
        {
            switch (propertyName)
            {
                case "Active":
                    Active = _viewModel.Active;
                    break;
            }
        }
    }

    protected override void Release()
    {
        if (_root != null)
        {
            GameObject.DestroyImmediate(_root.gameObject);
        }
    }
}
