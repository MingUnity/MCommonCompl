namespace Ming.Speech
{
    internal class MP3SpeechRec : ISpeechRecHandler
    {
        public ISpeechRecHandler _wavSpeechRec;

        public MP3SpeechRec(SpeechAppData appData, int devPid)
        {
            _wavSpeechRec = new CommonSpeechRec(appData, "wav", devPid);
        }

        public SpeechRecRes SpeechRec(byte[] data)
        {
            return _wavSpeechRec?.SpeechRec(SpeechConverter.MP3ToWAV(data));
        }
    }
}
