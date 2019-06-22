using MingUnity.MVVM;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private IViewFactory _viewFactory;

    private Dictionary<ViewType, IView> _viewDic = new Dictionary<ViewType, IView>();

    private Dictionary<ViewType, IViewModel> _viewModelDic = new Dictionary<ViewType, IViewModel>();

    private void Start()
    {
        Transform canvas = this.transform;

        _viewFactory = new ViewFactory();

        _viewFactory.Create<TestView_01, TestViewModel>(canvas, (view, viewmodel) =>
         {
             _viewDic[ViewType.Test_01] = view;

             _viewModelDic[ViewType.Test_01] = viewmodel;
         });

        IView view2 = new TestView_02();

        IViewModel viewmodel2 = new TestViewModel() { Text = "界面二" };

        _viewFactory.Create(view2, viewmodel2, canvas, () =>
         {
             _viewDic[ViewType.Test_02] = view2;

             _viewModelDic[ViewType.Test_02] = viewmodel2;
         });
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Switch 01"))
        {
            _viewDic[ViewType.Test_01].Show();

            _viewDic[ViewType.Test_02].Hide();
        }

        if (GUILayout.Button("Switch 02"))
        {
            _viewDic[ViewType.Test_02].Show();

            _viewDic[ViewType.Test_01].Hide();
        }

        if (GUILayout.Button("Helloworld"))
        {
            (_viewModelDic[ViewType.Test_02] as TestViewModel).Text = "HelloWorld";
        }
    }

    private enum ViewType
    {
        Test_01,

        Test_02
    }
}
