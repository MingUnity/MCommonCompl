using Baidu.Aip.Speech;
using System.Collections.Generic;

namespace Ming.Speech
{
    internal class MP3SpeechGenerator : ISpeechGenerateHandler
    {
        private TTSParam _ttsParam;

        private Tts _tts;

        public MP3SpeechGenerator(SpeechAppData appData, TTSParam ttsParam = default(TTSParam))
        {
            _tts = new Tts(appData.apiKey, appData.secretKey);
        }

        public byte[] SpeechGenerate(string content)
        {
            byte[] bytes = null;

            if (!string.IsNullOrEmpty(content))
            {
                Dictionary<string, object> options = null;

                if (_ttsParam.IsValid)
                {
                    options = new Dictionary<string, object>()
                    {
                         { "spd",_ttsParam.speed},
                         { "vol",_ttsParam.vol},
                         { "pit",_ttsParam.pit},
                         { "per",(int)_ttsParam.speaker}
                    };
                }

                TtsResponse resp = _tts.Synthesis(content, options);

                bytes = resp?.Data;
            }

            return bytes;
        }
    }
}
