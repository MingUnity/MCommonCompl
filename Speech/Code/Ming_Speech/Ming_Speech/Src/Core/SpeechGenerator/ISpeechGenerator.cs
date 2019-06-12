using System;

namespace Ming.Speech
{
    /// <summary>
    /// 语音合成
    /// </summary>
    public interface ISpeechGenerator : ISpeechGenerateHandler
    {
        /// <summary>
        /// 异步合成
        /// </summary>
        void AsyncSpeechGenrate(string content, Action<byte[]> callback);
    }
}
