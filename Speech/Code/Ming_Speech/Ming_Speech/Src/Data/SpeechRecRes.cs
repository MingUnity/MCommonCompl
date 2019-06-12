using System.Collections.Generic;

namespace Ming.Speech
{
    /// <summary>
    /// 语音识别结果
    /// </summary>
    public class SpeechRecRes
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int err_no;

        /// <summary>
        /// 错误码描述
        /// </summary>
        public string err_msg;

        /// <summary>
        /// 语料库
        /// </summary>
        public string corpus_no;

        /// <summary>
        /// 语音数据唯一标识，系统内部产生，用于 debug
        /// </summary>
        public string sn;

        /// <summary>
        /// 识别结果数组，提供多个候选结果，无论返回多少个请取第一个
        /// </summary>
        public List<string> result;
    }
}
