namespace MingUnity.ElementCore
{
    /// <summary>
    /// 参数扩展
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

                t = cArgs.value;
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

                t = sArgs.value;
            }

            return t;
        }

        /// <summary>
        /// 获取类参数的旧值
        /// </summary>
        public static void GetCValue<T>(this IPropertyChangedArgs args, out T oldValue, out T newValue) where T : class
        {
            oldValue = null;

            newValue = null;

            if (args is CPropertyArgsB<T>)
            {
                CPropertyArgsB<T> cArgs = args as CPropertyArgsB<T>;

                oldValue = cArgs.oldValue;

                newValue = cArgs.newValue;
            }
        }

        /// <summary>
        /// 获取结构体参数的旧值
        /// </summary>
        public static void GetSValue<T>(this IPropertyChangedArgs args, out T oldValue, out T newValue) where T : struct
        {
            oldValue = default(T);

            newValue = default(T);

            if (args is SPropertyArgsB<T>)
            {
                SPropertyArgsB<T> sArgs = (SPropertyArgsB<T>)args;

                oldValue = sArgs.oldValue;

                newValue = sArgs.newValue;
            }
        }
    }
}
