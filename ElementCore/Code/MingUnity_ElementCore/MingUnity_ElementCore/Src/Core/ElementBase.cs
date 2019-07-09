using System;
using UnityEngine;

namespace MingUnity.ElementCore
{
    /// <summary>
    /// 元素对象基类
    /// </summary>
    public abstract class ElementBase : IElement, IDisposable
    {
        protected IElementModel _elementModel;

        public abstract bool Active { get; set; }

        public IElementModel ElementModel
        {
            get
            {
                return _elementModel;
            }
            set
            {
                if (_elementModel != null)
                {
                    _elementModel.OnPropertyChangedEvent -= OnModelPropertyChanged;
                }

                _elementModel = value;

                if (_elementModel != null)
                {
                    _elementModel.OnPropertyChangedEvent += OnModelPropertyChanged;

                    _elementModel.Setup();
                }
            }
        }

        public abstract void Create(Transform parent = null, Action callback = null);

        public void Dispose()
        {
            if (_elementModel != null)
            {
                _elementModel.OnPropertyChangedEvent -= OnModelPropertyChanged;
            }

            Disposing();
        }

        protected virtual void Disposing() { }

        protected abstract void OnModelPropertyChanged(string propertyName, IPropertyChangedArgs args);
    }
}
