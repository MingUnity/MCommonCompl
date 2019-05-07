using UnityEngine;
using UnityEditor;

namespace MingUnity.Editor.AssetbundleModule
{
    internal static class ViewManager
    {
        /// <summary>
        /// 显示
        /// </summary>
        public static void Show<T>(IEditorModel model) where T : EditorWindow, IEditorView
        {
            T t = ScriptableObject.CreateInstance<T>();

            if (model != null)
            {
                model.View = t;

                model.Setup();
            }

            t.ShowView();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void Close(IEditorModel model)
        {
            if (model != null)
            {
                model.UnSetup();

                if (model.View != null)
                {
                    model.View.CloseView();

                    model.View = null;
                }
            }
        }
    }
}
