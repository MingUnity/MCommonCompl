using System;
using System.IO;
using System.Threading;

namespace Ming.Speech
{
    /// <summary>
    /// 语音识别
    /// </summary>
    public class SpeechRecer : SpeechBase, ISpeechRec
    {
        private ISpeechRecHandler _handler;

        public SpeechRecer(SpeechAppData appData, SpeechForamt format = SpeechForamt.WAV, RecType recType = RecType.ChineseAndSimpleEnglish)
        {
            switch (format)
            {
                case SpeechForamt.WAV:
                    _handler = new CommonSpeechRec(appData, "wav", (int)recType);
                    break;

                case SpeechForamt.PCM:
                    _handler = new CommonSpeechRec(appData, "pcm", (int)recType);
                    break;

                case SpeechForamt.AMR:
                    _handler = new CommonSpeechRec(appData, "amr", (int)recType);
                    break;

                case SpeechForamt.MP3:
                    throw new NotImplementedException("<Ming> ## Uni Exception ## Cls:SpeechRecer Func:SpeechRecer Info: don't support mp3");
            }
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
            return _handler?.SpeechRec(data);
        }
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
