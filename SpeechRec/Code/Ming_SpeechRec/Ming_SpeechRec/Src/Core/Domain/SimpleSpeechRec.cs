using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;

namespace Ming.SpeechRec
{
    /// <summary>
    /// 简单语音识别
    /// </summary>
    public class SimpleSpeechRec : SpeechRecBase, ISpeechRec
    {
        private string _speechFormat;

        private int _devPid = (int)RecType.ChineseAndSimpleEnglish;

        public SimpleSpeechRec(SpeechForamt format, RecType type, SpeechAppData appData) : base(appData)
        {
            ParseFormat(format);

            ParseRecType(type);
        }

        public SimpleSpeechRec(SpeechForamt format, SpeechAppData appData) : base(appData)
        {
            ParseFormat(format);
        }

        public void AsyncSpeechRec(string filePath, Action<SpeechRecRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                callback?.Invoke(SpeechRec(filePath));
            });
        }

        public void AsyncSpeechRec(byte[] data, Action<SpeechRecRes> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                callback?.Invoke(SpeechRec(data));
            });
        }

        public SpeechRecRes SpeechRec(string filePath)
        {
            SpeechRecRes res = null;

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                res = SpeechRec(File.ReadAllBytes(filePath));
            }

            return res;
        }

        public SpeechRecRes SpeechRec(byte[] data)
        {
            SpeechRecRes res = null;

            if (data != null)
            {
                JObject result = _speechClient.Recognize(data, _speechFormat, 16000);

                res = result?.ToObject<SpeechRecRes>();
            }

            return res;
        }

        private void ParseFormat(SpeechForamt format)
        {
            switch (format)
            {
                case SpeechForamt.WAV:
                    _speechFormat = "wav";
                    break;

                case SpeechForamt.PCM:
                    _speechFormat = "pcm";
                    break;

                case SpeechForamt.AMR:
                    _speechFormat = "amr";
                    break;
            }
        }

        private void ParseRecType(RecType type)
        {
            _devPid = (int)type;
        }

        /// <summary>
        /// 语音格式
        /// </summary>
        public enum SpeechForamt
        {
            WAV,

            PCM,

            AMR
        }

        /// <summary>
        /// 识别类型
        /// </summary>
        public enum RecType
        {
            /// <summary>
            /// 普通话(支持简单的英文识别) 无标点
            /// </summary>
            ChineseAndSimpleEnglish = 1536,

            /// <summary>
            /// 普通话(纯中文识别) 有标点
            /// </summary>
            Chinese = 1537,

            /// <summary>
            /// 英语 无标点
            /// </summary>
            English = 1737,

            /// <summary>
            /// 粤语 有标点
            /// </summary>
            Cantonese = 1637,

            /// <summary>
            /// 四川话 有标点
            /// </summary>
            Sichuan = 1837,

            /// <summary>
            /// 普通话远场 有标点
            /// </summary>
            LongChinese = 1936
        }
    }
}
