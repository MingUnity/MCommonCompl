﻿using Newtonsoft.Json.Linq;
using Baidu.Aip.Speech;

namespace Ming.Speech
{
    internal class CommonSpeechRec : ISpeechRecHandler
    {
        private Asr _speechClient;

        private string _speechFormat;

        private int _devPid;
        
        public CommonSpeechRec(SpeechAppData appData, string format,int devPid)
        {
            _speechClient = new Asr(appData.appId, appData.apiKey, appData.secretKey);

            this._speechFormat = format;

            this._devPid = devPid;
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
    }
}
