using System;

namespace Densyakun.CSWave
{
    [Serializable]
    public class NotCalculableWaveException : System.Exception
    {
        public NotCalculableWaveException() : base() { }
        public NotCalculableWaveException(string message) : base("These Waves can't be calculated. That is Different channels or Different sample rates.") { }
        public NotCalculableWaveException(string message, System.Exception inner) : base("These Waves can't be calculated. That is Different channels or Different sample rates.", inner) { }

        protected NotCalculableWaveException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}
