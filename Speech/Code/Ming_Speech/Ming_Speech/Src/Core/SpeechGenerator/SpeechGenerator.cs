using Baidu.Aip.Speech;
using System;
using System.Threading;

namespace Ming.Speech
{
    /// <summary>
    /// 简单语音合成
    /// </summary>
    public class SpeechGenerator : SpeechBase, ISpeechGenerator
    {
        private ISpeechGenerateHandler _handler;

        public SpeechGenerator(SpeechAppData appData, SpeechForamt foramt = SpeechForamt.MP3, TTSParam ttsParam = default(TTSParam))
        {
            switch (foramt)
            {
                case SpeechForamt.WAV:
                case SpeechForamt.PCM:
                case SpeechForamt.AMR:
                    throw new NotImplementedException("<Ming> ## Uni Exception ## Cls:SpeechGenerator Func:SpeechGenerator Info: only support mp3");

                case SpeechForamt.MP3:
                    _handler = new MP3SpeechGenerator(appData, ttsParam);
                    break;
            }
        }

        public byte[] SpeechGenerate(string content)
        {
            return _handler?.SpeechGenerate(content);
        }

        public void AsyncSpeechGenrate(string content, Action<byte[]> callback)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                callback?.Invoke(SpeechGenerate(content));
            });
        }
    }

    /// <summary>
    /// 发音人
    /// </summary>
    public enum Speaker
    {
        Female = 0,

        Male = 1,

        DuXiaoyao = 3,

        DuYaya = 4,
    }

    /// <summary>
    /// 语音合成参数
    /// </summary>
    public struct TTSParam
    {
        private bool _isValid;

        /// <summary>
        /// 语速
        /// </summary>
        public int speed;

        /// <summary>
        /// 音调
        /// </summary>
        public int pit;

        /// <summary>
        /// 音量
        /// </summary>
        public int vol;

        /// <summary>
        /// 发音人
        /// </summary>
        public Speaker speaker;

        /// <summary>
        /// 可用
        /// </summary>
        internal bool IsValid
        {
            get
            {
                return _isValid;
            }
        }

        public TTSParam(int speed, int pit, int vol, Speaker speaker)
        {
            this.speed = speed;

            this.pit = pit;

            this.vol = vol;

            this.speaker = speaker;

            _isValid = true;
        }
    }
}
