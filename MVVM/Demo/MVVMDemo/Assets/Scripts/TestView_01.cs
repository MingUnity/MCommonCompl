using System;
using MingUnity.MVVM.View;
using UnityEngine;

public class TestView_01 : ViewBase<TestViewModel_01>
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
            _root?.gameObject.SetActive(value);
        }
    }

    public override void Create(Transform parent = null, Action callback = null)
    {
        GameObject prefab = Resources.Load<GameObject>("TestView_01");

        if (prefab != null)
        {
            _root = GameObject.Instantiate(prefab, parent)?.GetComponent<RectTransform>();
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
