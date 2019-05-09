using System.Collections.Generic;

namespace Ming.FSM
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    public sealed class FSMSystem : IFSMSystem
    {
        /// <summary>
        /// 状态列表
        /// </summary>
        private List<IFSMState> _stateList = new List<IFSMState>();

        /// <summary>
        /// 当前状态
        /// </summary>
        private IFSMState _curState;

        /// <summary>
        /// 默认状态
        /// </summary>
        private IFSMState _defaultState;

        /// <summary>
        /// 添加状态
        /// </summary>
        public void AddState(IFSMState state, bool isDefault = false)
        {
            if (state != null)
            {
                _stateList?.Add(state);

                if (isDefault)
                {
                    _defaultState = state;

                    _curState = state;

                    state.OnEnter();
                }
            }
        }

        /// <summary>
        /// 跳转默认状态
        /// </summary>
        public void TurnDefault()
        {
            TurnState(_curState, _defaultState);
        }

        /// <summary>
        /// 设置过渡
        /// </summary>
        public void SetTransition(object transition, params object[] keys)
        {
            if (_curState != null)
            {
                IFSMState targetState = _curState[transition];

                TurnState(_curState, targetState, keys);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Update()
        {
            _curState?.OnStay();
        }

        /// <summary>
        /// 跳转状态
        /// </summary>
        private void TurnState(IFSMState oldState, IFSMState newState, params object[] keys)
        {
            if (newState != null && _stateList != null && _stateList.Contains(newState))
            {
                oldState?.OnExit();

                _curState = newState;

                newState.OnEnter(keys);
            }
        }
    }
}
