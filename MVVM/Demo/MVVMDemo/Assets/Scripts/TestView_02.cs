using MingUnity.MVVM;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TestView_02 : ViewBase
{
    private Text _text;

    public override bool Active
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
        set
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

    public override void Create(Transform parent, bool active, Action callback = null)
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

        Active = active;

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    public override void Show(Action callback = null)
    {
        Active = true;

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    public override void Hide(Action callback = null)
    {
        Active = false;

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    protected override void ViewModelPropertyChanged(string propertyName, IPropertyChangedArgs args)
    {
        if (args == null)
        {
            return;
        }

        switch (propertyName)
        {
            case "Text":
                Text = args.GetCValue<string>();
                break;
        }
    }
}
