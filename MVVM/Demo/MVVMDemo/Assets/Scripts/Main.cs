using MingUnity.MVVM.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private IViewFactory _viewFactory;

    private Dictionary<ViewType, string> _viewDic = new Dictionary<ViewType, string>();

    private void Start()
    {
        Transform canvas = this.transform;

        _viewFactory = new ViewFactory();

        _viewFactory.Create<TestView_01>(new TestViewModel() { Active = true }, canvas, (guid) =>
        {
            _viewDic[ViewType.Test_01] = guid;
        });

        _viewFactory.Create<TestView_02>(new TestViewModel() { Active = false, Text = "界面二" }, canvas, (guid) =>
        {
            _viewDic[ViewType.Test_02] = guid;
        });
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Switch 02"))
        {
            _viewFactory.GetViewModel(_viewDic[ViewType.Test_02]).Active = true;
        }
    }

    private enum ViewType
    {
        Test_01,

        Test_02
    }
}
