using System;
using System.IO;

namespace Densyakun.CSWave
{
    public class Wave
    {

        // TODO Serializable
        // TODO Wave file importer
        // TODO Change channnels and samplerate
        // TODO 16 bits only

        public ushort channels;
        public uint samplerate;
        private ushort bits_;
        public ushort bits
        {
            get
            {
                return bits_;
            }
            private set { }
        }
        public double time;
        public double[] wave;

        public Wave(ushort channels, uint samplerate, double time)
        {
            wave = new double[(int)Math.Floor((this.channels = channels) * (this.samplerate = samplerate) * (this.time = time))];
            bits_ = 16;
        }

        public static Wave operator +(Wave l, double r)
        {
            var wave = new Wave(l.channels, l.samplerate, l.time);

            for (var n = 0; n < l.wave.Length; n++)
                wave.wave[n] = l.wave[n] + r;

            return wave;
        }

        public static Wave operator +(Wave l, Wave r)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Max(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = l.wave[n] + r.wave[n];
                if (l.wave.Length > r.wave.Length)
                    for (var n = l_; n < l.wave.Length; n++)
                        wave.wave[n] = l.wave[n];
                else
                    for (var n = l_; n < r.wave.Length; n++)
                        wave.wave[n] = r.wave[n];

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public static Wave operator -(Wave r)
        {
            var wave = new Wave(r.channels, r.samplerate, r.time);

            for (var n = 0; n < r.wave.Length; n++)
                wave.wave[n] = -r.wave[n];

            return wave;
        }

        public static Wave operator -(Wave l, double r)
        {
            var wave = new Wave(l.channels, l.samplerate, l.time);

            for (var n = 0; n < l.wave.Length; n++)
                wave.wave[n] = l.wave[n] - r;

            return wave;
        }

        public static Wave operator -(Wave l, Wave r)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Max(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = l.wave[n] - r.wave[n];
                if (l.wave.Length > r.wave.Length)
                    for (var n = l_; n < l.wave.Length; n++)
                        wave.wave[n] = l.wave[n];
                else
                    for (var n = l_; n < r.wave.Length; n++)
                        wave.wave[n] = r.wave[n];

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public static Wave operator ++(Wave a)
        {
            return a + 1f;
        }

        public static Wave operator --(Wave a)
        {
            return a - 1f;
        }

        public static bool operator true(Wave a)
        {
            return true;
        }

        public static bool operator false(Wave a)
        {
            return false;
        }

        public static Wave operator *(Wave l, double r)
        {
            var wave = new Wave(l.channels, l.samplerate, l.time);

            for (var n = 0; n < l.wave.Length; n++)
                wave.wave[n] = l.wave[n] * r;

            return wave;
        }

        public static Wave operator *(Wave l, Wave r)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Max(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = l.wave[n] * r.wave[n];
                if (l.wave.Length > r.wave.Length)
                    for (var n = l_; n < l.wave.Length; n++)
                        wave.wave[n] = l.wave[n];
                else
                    for (var n = l_; n < r.wave.Length; n++)
                        wave.wave[n] = r.wave[n];

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public static Wave operator /(Wave l, double r)
        {
            var wave = new Wave(l.channels, l.samplerate, l.time);

            for (var n = 0; n < l.wave.Length; n++)
                wave.wave[n] = l.wave[n] / r;

            return wave;
        }

        public static Wave operator /(Wave l, Wave r)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Max(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = l.wave[n] / r.wave[n];
                if (l.wave.Length > r.wave.Length)
                    for (var n = l_; n < l.wave.Length; n++)
                        wave.wave[n] = l.wave[n];
                else
                    for (var n = l_; n < r.wave.Length; n++)
                        wave.wave[n] = r.wave[n];

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public static Wave operator %(Wave l, double r)
        {
            var wave = new Wave(l.channels, l.samplerate, l.time);

            for (var n = 0; n < l.wave.Length; n++)
                wave.wave[n] = l.wave[n] % r;

            return wave;
        }

        public static Wave operator %(Wave l, Wave r)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Max(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = l.wave[n] % r.wave[n];
                if (l.wave.Length > r.wave.Length)
                    for (var n = l_; n < l.wave.Length; n++)
                        wave.wave[n] = l.wave[n];
                else
                    for (var n = l_; n < r.wave.Length; n++)
                        wave.wave[n] = r.wave[n];

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public static Wave operator &(Wave l, Wave r)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Min(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = (l.wave[n] != 0) && (r.wave[n] != 0) ? 1f : 0f;

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public static Wave operator |(Wave l, Wave r)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Min(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = (l.wave[n] != 0) || (r.wave[n] != 0) ? 1f : 0f;

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public override bool Equals(object obj)
        {
            if (this.GetType() == obj.GetType() && channels == ((Wave)obj).channels && samplerate == ((Wave)obj).samplerate && bits_ == ((Wave)obj).bits_ && time == ((Wave)obj).time)
            {
                for (var n = 0; n < wave.Length; n++)
                    if (wave[n] != ((Wave)obj).wave[n])
                        return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return wave.GetHashCode();
        }


        public void saveFile(string path)
        {
            using (var fs = File.Create(path))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    var fmtChunkSize = 16;
                    var dataChunkSize = wave.Length * (int)bits_ / 8;

                    var e = System.Text.Encoding.GetEncoding("ascii");
                    bw.Write(e.GetBytes("RIFF"));
                    bw.Write(BitConverter.GetBytes(dataChunkSize + fmtChunkSize + 20));
                    bw.Write(e.GetBytes("WAVE"));

                    bw.Write(e.GetBytes("fmt "));
                    bw.Write(BitConverter.GetBytes(fmtChunkSize));
                    bw.Write(BitConverter.GetBytes((short)1));
                    bw.Write(BitConverter.GetBytes(channels));
                    bw.Write(BitConverter.GetBytes(samplerate));
                    bw.Write(BitConverter.GetBytes((uint)channels * samplerate * 2));
                    bw.Write(BitConverter.GetBytes((ushort)(channels * bits_)));
                    bw.Write(BitConverter.GetBytes(bits_));

                    bw.Write(e.GetBytes("data"));
                    bw.Write(BitConverter.GetBytes(dataChunkSize));
                    var b = Math.Pow(2d, bits_ - 1d);
                    foreach (var a in wave)
                        if (a > 0)
                            bw.Write(BitConverter.GetBytes((ushort)Math.Round(a * (b - 1d))));
                        else
                            bw.Write(BitConverter.GetBytes((ushort)Math.Round(a * b)));
                }
            }
        }


        public void abs()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Abs(wave[n]);
        }

        public void Acos()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Acos(wave[n]);
        }

        public void Asin()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Asin(wave[n]);
        }

        public void Atan()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Atan(wave[n]);
        }

        public void Ceil()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Ceiling(wave[n]);
        }

        public void ClosestPowerOfTwo()
        {
            for (var n = 0; n < wave.Length; n++)
            {
                var m = 0d;
                while (!(Math.Pow(2d, m) <= wave[n] && wave[n] < Math.Pow(2d, m + 1d)))
                    m++;
                wave[n] = Math.Pow(2d, m);
            }
        }

        public void Cos()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Cos(wave[n]);
        }

        public void Cosh()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Cosh(wave[n]);
        }

        public void Exp()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Exp(wave[n]);
        }

        public void Floor()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Floor(wave[n]);
        }

        public void Log()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Log(wave[n]);
        }

        public void Log10()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Log10(wave[n]);
        }

        public void Max(double a)
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Max(wave[n], a);
        }

        public void Min(double a)
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Min(wave[n], a);
        }

        public void Round()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Round(wave[n]);
        }

        public Wave Lerp(Wave l, Wave r, double a)
        {
            if (l.channels == r.channels && l.samplerate == r.samplerate)
            {
                var wave = new Wave(l.channels, l.samplerate, Math.Max(l.time, r.time));

                var l_ = Math.Min(l.wave.Length, r.wave.Length);
                for (var n = 0; n < l_; n++)
                    wave.wave[n] = l.wave[n] + (r.wave[n] - l.wave[n]) * a;
                if (l.wave.Length > r.wave.Length)
                    for (var n = l_; n < l.wave.Length; n++)
                        wave.wave[n] = l.wave[n];
                else
                    for (var n = l_; n < r.wave.Length; n++)
                        wave.wave[n] = r.wave[n];

                return wave;
            }
            new NotCalculableWaveException();
            return null;
        }

        public void Sign()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Sign(wave[n]);
        }

        public void Sin()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Sin(wave[n]);
        }

        public void Sinh()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Sinh(wave[n]);
        }

        public void Sqrt()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Sqrt(wave[n]);
        }

        public void Tan()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Tan(wave[n]);
        }

        public void Tanh()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Tanh(wave[n]);
        }

        public void Truncate()
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = Math.Truncate(wave[n]);
        }


        public bool isZero()
        {
            foreach (var a in wave)
                if (a != 0)
                    return false;
            return true;
        }

        public void set(double a)
        {
            for (var n = 0; n < wave.Length; n++)
                wave[n] = a;
        }

        public void delete(int s, int l)
        {
            var newWave = new double[(int)Math.Floor(channels * samplerate * (time -= l / samplerate))];

            for (var n = 0; n < s; n++)
                newWave[n] = wave[n];
            var l_ = l * channels;
            for (var n = s + l_; n < wave.Length; n++)
                newWave[n - l_] = wave[n];

            wave = newWave;
        }

        public Wave cut(int s, int l)
        {
            var wave1 = new Wave(channels, samplerate, l / samplerate);
            var newWave = new double[(int)Math.Floor(channels * samplerate * (time -= l / samplerate))];

            for (var n = 0; n < s; n++)
                newWave[n] = wave[n];
            var l_ = l * channels;
            for (var n = s; n < s + l_; n++)
                wave1.wave[n - s] = wave[n];
            for (var n = s + l_; n < wave.Length; n++)
                newWave[n - l_] = wave[n];

            wave = newWave;
            return wave1;
        }

        public void combine(Wave wave)
        {
            if (channels == wave.channels && samplerate == wave.samplerate)
            {
                var newWave = new double[this.wave.Length + wave.wave.Length];

                for (var n = 0; n < this.wave.Length; n++)
                    newWave[n] = this.wave[n];
                for (var n = 0; n < wave.wave.Length; n++)
                    newWave[this.wave.Length + n] = wave.wave[n];

                time += wave.time;
                this.wave = newWave;
            }
            new NotCalculableWaveException();
        }

        public void insert(Wave wave, int s)
        {
            if (channels == wave.channels && samplerate == wave.samplerate)
            {
                var newWave = new double[this.wave.Length + wave.wave.Length];

                for (var n = 0; n < s; n++)
                    newWave[n] = this.wave[n];
                for (var n = 0; n < wave.wave.Length; n++)
                    newWave[s + n] = wave.wave[n];
                for (var n = 0; n < this.wave.Length - s - wave.wave.Length; n++)
                    newWave[s + wave.wave.Length + n] = this.wave[n];

                time += wave.time;
                this.wave = newWave;
            }
            new NotCalculableWaveException();
        }

        public void chop(double frequency, double duty)
        {
            double a;
            for (var n1 = 0; n1 < channels; n1++)
                for (var n = n1; n < wave.Length; n += channels)
                {
                    a = (double)(n - n1) * frequency / samplerate / channels;
                    a -= Math.Floor(a);
                    if (duty <= a)
                        wave[n] = 0d;
                }
        }



        public void generateSawtooth(double frequency)
        {
            double a;
            for (var n1 = 0; n1 < channels; n1++)
                for (var n = n1; n < wave.Length; n += channels)
                {
                    a = (double)(n - n1) * frequency * 4d / samplerate / channels + 1d;
                    wave[n] = a - Math.Floor(a / 2d + 1d) * 2d + 1d;
                }
        }

        public void generateTriangle(double frequency)
        {
            double a;
            for (var n1 = 0; n1 < channels; n1++)
                for (var n = n1; n < wave.Length; n += channels)
                {
                    a = (double)(n - n1) * frequency * 4d / samplerate / channels + 1d;
                    if (Math.Floor(a / 2d) % 2d == 0d)
                        wave[n] = a - Math.Floor(a / 2d + 1d) * 2d + 1d;
                    else
                        wave[n] = Math.Floor(a / 2d + 1d) * 2d - a - 1d;
                }
        }

        public void generateSine(double frequency)
        {
            for (var n1 = 0; n1 < channels; n1++)
                for (var n = n1; n < wave.Length; n += channels)
                    wave[n] = Math.Sin((double)(n - n1) * frequency * 2d * Math.PI / samplerate / channels);
        }

        public void generateSquare(double frequency)
        {
            for (var n1 = 0; n1 < channels; n1++)
                for (var n = n1; n < wave.Length; n += channels)
                    wave[n] = Math.Round(Math.Sin(((double)(n - n1) * frequency * 2d * Math.PI / samplerate / channels)) + 1d / 2d) * 2d - 1d;
        }
    }
}
