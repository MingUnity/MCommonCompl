using System;
using MingUnity.MVVM.View;
using UnityEngine;
using UnityEngine.UI;

public class TestView_02 : ViewBase<TestViewModel>
{
    private Text _text;

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

    public string Text
    {
        get
        {
            string res = string.Empty;

            if (_text != null)
            {
                res = _text.text;
            }

            return res;
        }
        private set
        {
            if (_text != null)
            {
                _text.text = value;
            }
        }
    }

    public override void Create(Transform parent, Action callback)
    {
        GameObject prefab = Resources.Load<GameObject>("TestView_02");

        if (prefab != null)
        {
            _root = GameObject.Instantiate(prefab, parent).GetComponent<RectTransform>();

            if (_root != null)
            {
                _text = _root.Find("Text").GetComponent<Text>();
            }
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

                case "Text":
                    Text = _viewModel.Text;
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
