using System.Collections.Generic;

namespace Ming.FSM
{
    /// <summary>
    /// FSM状态
    /// </summary>
    public abstract class FSMState : IFSMState
    {
        /// <summary>
        /// 过渡状态字典
        /// </summary>
        private Dictionary<object, IFSMState> _stateTransitionDic = new Dictionary<object, IFSMState>();

        /// <summary>
        /// 获取目标状态
        /// </summary>
        public IFSMState this[object transition]
        {
            get
            {
                IFSMState res = null;

                _stateTransitionDic?.TryGetValue(transition, out res);

                return res;
            }
            set
            {
                if (_stateTransitionDic != null)
                {
                    _stateTransitionDic[transition] = value;
                }
            }
        }

        /// <summary>
        /// 进入
        /// </summary>
        public abstract void OnEnter(params object[] keys);

        /// <summary>
        /// 退出
        /// </summary>
        public abstract void OnExit();

        /// <summary>
        /// 驻留
        /// </summary>
        public abstract void OnStay();
    }
}
