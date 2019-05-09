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
        /// 视图记录
        /// </summary>
        private Stack<string> _viewHistory = new Stack<string>();

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
        /// 当前视图Id
        /// </summary>
        private string _curGuid;

        /// <summary>
        /// 创建视图
        /// </summary>
        public void Create<T>(IViewModel viewModel, Transform parent = null, Action<string> callback = null) where T : class, IView
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
        public void Destroy(string guid)
        {
            this[guid]?.Dispose();
        }

        /// <summary>
        /// 切换视图
        /// </summary>
        public void Switch(string guid, bool record = false)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                SetActive(_curGuid, false);

                SetActive(guid, true);
                
                if (record && !string.IsNullOrEmpty(_curGuid))
                {
                    _viewHistory?.Push(_curGuid);
                }

                _curGuid = guid;
            }
        }

        /// <summary>
        /// 切换视图
        /// </summary>
        public void Switch(string guid, IViewModel viewModel, bool record = false)
        {
            if (viewModel != null)
            {
                IView view = this[guid];

                if (view != null)
                {
                    view.ViewModel = viewModel;
                }
            }

            Switch(guid, record);
        }

        /// <summary>
        /// 回退视图
        /// </summary>
        public void Backwards()
        {
            if (_viewHistory != null && _viewHistory.Count > 0)
            {
                Switch(_viewHistory.Pop());
            }
        }

        /// <summary>
        /// 设置激活
        /// </summary>
        private void SetActive(string guid, bool active)
        {
            IView view = this[guid];

            if (view != null && view.ViewModel != null)
            {
                view.ViewModel.Active = active;
            }
        }
    }
}
