using System;
using MingUnity.MVVM.View;
using UnityEngine;

public class TestView_01 : ViewBase<TestViewModel_01>
{
    public override bool Active
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override void Create(Transform parent = null, Action callback = null)
    {
        throw new NotImplementedException();
    }

    protected override void PropertyChanged(string propertyName)
    {
        throw new NotImplementedException();
    }

    protected override void Release()
    {
        throw new NotImplementedException();
    }
}
