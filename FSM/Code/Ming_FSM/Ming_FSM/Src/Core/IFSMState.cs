namespace Ming.FSM
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IFSMState
    {
        /// <summary>
        /// 条件过渡
        /// </summary>
        IFSMState this[object transition] { get; set; }

        /// <summary>
        /// 进入
        /// </summary>
        void OnEnter(params object[] keys);

        /// <summary>
        /// 退出
        /// </summary>
        void OnExit();

        /// <summary>
        /// 驻留
        /// </summary>
        void OnStay();
    }
}
