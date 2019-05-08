using MingUnity.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MingUnity.MVVM.View
{
    /// <summary>
    /// 视图服务
    /// </summary>
    public class ViewService : IViewService
    {
        /// <summary>
        /// 视图字典
        /// </summary>
        private Dictionary<string, IView> _viewDic = new Dictionary<string, IView>();

        /// <summary>
        /// 视图索引
        /// </summary>
        private IView this[string guid]
        {
            get
            {
                IView result = null;

                if (!string.IsNullOrEmpty(guid))
                {
                    _viewDic.TryGetValue(guid, out result);
                }

                return result;
            }
        }

        /// <summary>
        /// 创建视图
        /// </summary>
        public void CreateView<T>(IViewModel viewModel, Transform parent = null, Action<string> callback = null) where T : class, IView
        {
            T view = Activator.CreateInstance<T>();

            view.Create(parent, () =>
            {
                string guid = Guid.NewGuid().ToString();

                _viewDic[guid] = view;

                view.ViewModel = viewModel;

                callback?.Invoke(guid);
            });
        }

        /// <summary>
        /// 销毁视图
        /// </summary>
        public void DestroyView(string guid)
        {

        }
    }
}
