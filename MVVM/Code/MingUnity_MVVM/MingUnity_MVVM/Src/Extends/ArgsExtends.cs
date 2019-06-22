namespace MingUnity.MVVM
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ArgsExtends
    {
        /// <summary>
        /// 获取类参数的新值
        /// </summary>
        public static T GetCValue<T>(this IPropertyChangedArgs args) where T : class
        {
            T t = null;

            if (args is CPropertyArgs<T>)
            {
                CPropertyArgs<T> cArgs = args as CPropertyArgs<T>;

                t = cArgs.newValue;
            }

            return t;
        }

        /// <summary>
        /// 获取结构体参数的新值
        /// </summary>
        public static T GetSValue<T>(this IPropertyChangedArgs args) where T : struct
        {
            T t = default(T);

            if (args is SPropertyArgs<T>)
            {
                SPropertyArgs<T> sArgs = (SPropertyArgs<T>)args;

                t = sArgs.newValue;
            }

            return t;
        }

        /// <summary>
        /// 获取类参数的旧值
        /// </summary>
        public static T GetOldCValue<T>(this IPropertyChangedArgs args) where T : class
        {
            T t = null;

            if (args is CPropertyArgs<T>)
            {
                CPropertyArgs<T> cArgs = args as CPropertyArgs<T>;

                t = cArgs.newValue;
            }

            return t;
        }

        /// <summary>
        /// 获取结构体参数的旧值
        /// </summary>
        public static T GetOldSValue<T>(this IPropertyChangedArgs args) where T : struct
        {
            T t = default(T);

            if (args is SPropertyArgs<T>)
            {
                SPropertyArgs<T> sArgs = (SPropertyArgs<T>)args;

                t = sArgs.newValue;
            }

            return t;
        }
    }
}
