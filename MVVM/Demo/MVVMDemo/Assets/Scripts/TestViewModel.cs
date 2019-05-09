using MingUnity.MVVM.Model;
using MingUnity.MVVM.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestViewModel : ViewModelBase<IModel>
{
    private string _text;

    public override bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;

            RaisePropertyChanged("Active");
        }
    }

    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;

            RaisePropertyChanged("Text");
        }
    }

    public override void Setup()
    {
        Active = _active;

        Text = _text;
    }
}
