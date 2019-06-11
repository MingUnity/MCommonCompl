using System;

namespace Ming.SpeechRec
{
    /// <summary>
    /// 语音识别接口
    /// </summary>
    public interface ISpeechRec
    {
        /// <summary>
        /// 语音识别
        /// </summary>
        /// <param name="data">语音二进制数据</param>
        /// <returns>语音识别结果</returns>
        SpeechRecRes SpeechRec(byte[] data);

        /// <summary>
        /// 语音识别
        /// </summary>
        /// <param name="filePath">语音文件路径</param>
        /// <returns>语音识别结果</returns>
        SpeechRecRes SpeechRec(string filePath);

        /// <summary>
        /// 异步语音识别
        /// </summary>
        /// <param name="data">语音二进制数据</param>
        /// <param name="callback">语音结果回调</param>
        void AsyncSpeechRec(byte[] data, Action<SpeechRecRes> callback);

        /// <summary>
        /// 异步语音识别
        /// </summary>
        /// <param name="filePath">语音文件路径</param>
        /// <param name="callback">语音结果回调</param>
        void AsyncSpeechRec(string filePath, Action<SpeechRecRes> callback);
    }
}
