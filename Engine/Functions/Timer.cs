﻿namespace Engine
{
    // This class provides a standardised timer to be used for various functionality

    public partial class Timer
    {
        private const long TicksPerMillisecond = 10000;
        private const long TicksPerSecond = TicksPerMillisecond * 1000;

        private long _elapsed;
        private long _startTimeStamp;
        private bool _isRunning;

        // "Frequency" stores the frequency of the high-resolution performance counter,
        // if one exists. Otherwise it will store TicksPerSecond.
        // The frequency cannot change while the system is running,
        // so we only need to initialize it once.
        public static readonly long Frequency = QueryPerformanceFrequency();
        //public static readonly bool IsHighResolution = true;

        // performance-counter frequency, in counts per ticks.
        // This can speed up conversion from high frequency performance-counter
        // to ticks.
        private static readonly double s_tickFrequency = (double)TicksPerSecond / Frequency;

        public Timer()
        {
            Reset();
        }

        public void Start()
        {
            // Calling start on a running Stopwatch is a no-op.
            if (!_isRunning)
            {
                _startTimeStamp = GetTimestamp();
                _isRunning = true;
            }
        }

        public static Timer StartNew()
        {
            Timer s = new Timer();
            s.Start();
            return s;
        }

        public void Stop()
        {
            // Calling stop on a stopped Stopwatch is a no-op.
            if (_isRunning)
            {
                long endTimeStamp = GetTimestamp();
                long elapsedThisPeriod = endTimeStamp - _startTimeStamp;
                _elapsed += elapsedThisPeriod;
                _isRunning = false;

                if (_elapsed < 0)
                {
                    // When measuring small time periods the Stopwatch.Elapsed*
                    // properties can return negative values.  This is due to
                    // bugs in the basic input/output system (BIOS) or the hardware
                    // abstraction layer (HAL) on machines with variable-speed CPUs
                    // (e.g. Intel SpeedStep).

                    _elapsed = 0;
                }
            }
        }

        public void Reset()
        {
            _elapsed = 0;
            _isRunning = false;
            _startTimeStamp = 0;
        }

        // Convenience method for replacing {sw.Reset(); sw.Start();} with a single sw.Restart()
        public void Restart()
        {
            _elapsed = 0;
            _startTimeStamp = GetTimestamp();
            _isRunning = true;
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public TimeSpan Elapsed
        {
            get { return new TimeSpan(GetElapsedDateTimeTicks()); }
        }

        public long ElapsedMilliseconds
        {
            get { return GetElapsedDateTimeTicks() / TicksPerMillisecond; }
        }

        public long ElapsedTicks
        {
            get { return GetRawElapsedTicks(); }
        }

        public static long GetTimestamp()
        {
            //System.Diagnostics.Debug.Assert(IsHighResolution);
            return (long)Time.time;
        }

        // Get the elapsed ticks.
        private long GetRawElapsedTicks()
        {
            long timeElapsed = _elapsed;

            if (_isRunning)
            {
                // If the Stopwatch is running, add elapsed time since
                // the Stopwatch is started last time.
                long currentTimeStamp = GetTimestamp();
                long elapsedUntilNow = currentTimeStamp - _startTimeStamp;
                timeElapsed += elapsedUntilNow;
            }
            return timeElapsed;
        }

        // Get the elapsed ticks.
        private long GetElapsedDateTimeTicks()
        {
            //System.Diagnostics.Debug.Assert(IsHighResolution);
            // convert high resolution perf counter to DateTime ticks
            return unchecked((long)(GetRawElapsedTicks() * s_tickFrequency));
        }

        private static long QueryPerformanceFrequency()
        {
            return (long)TicksPerSecond;
        }

        private void QueryPerformanceCounter()
        {

        }
    }
}
