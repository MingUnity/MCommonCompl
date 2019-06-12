using System;

namespace Ming.Speech
{
    /// <summary>
    /// 语音识别接口
    /// </summary>
    public interface ISpeechRecHandler
    {
        /// <summary>
        /// 语音识别
        /// </summary>
        /// <param name="data">语音二进制数据</param>
        /// <returns>语音识别结果</returns>
        SpeechRecRes SpeechRec(byte[] data);
    }
}
