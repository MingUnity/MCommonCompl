using MingUnity.MVVM.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private IViewService _viewService;

    private Dictionary<ViewType, string> _viewDic = new Dictionary<ViewType, string>();

    private void Start()
    {
        Transform canvas = this.transform;

        _viewService = new ViewService();

        _viewService.Create<TestView_01>(new TestViewModel() { Active = false }, canvas, (guid) =>
        {
            _viewDic[ViewType.Test_01] = guid;
        });

        _viewService.Create<TestView_02>(new TestViewModel() { Active = false, Text = "界面二" }, canvas, (guid) =>
        {
            _viewDic[ViewType.Test_02] = guid;
        });

        _viewService.Switch(_viewDic[ViewType.Test_01]);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Switch 01"))
        {
            _viewService.Switch(_viewDic[ViewType.Test_01], true);
        }

        if (GUILayout.Button("Switch 02"))
        {
            _viewService.Switch(_viewDic[ViewType.Test_02], true);
        }

        if (GUILayout.Button("Backwards"))
        {
            _viewService.Backwards();
        }

        if (GUILayout.Button("Switch 02 With \"ABC\""))
        {
            _viewService.Switch(_viewDic[ViewType.Test_02], new TestViewModel() { Text = "ABC" });
        }
    }

    private enum ViewType
    {
        Test_01,

        Test_02
    }
}
