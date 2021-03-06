﻿using System;
using System.Collections.Generic;
using System.Threading;
using BosunReporter.Infrastructure;

namespace BosunReporter.Metrics
{
    [ExcludeDefaultTags("host")]
    public class ExternalCounter : BosunMetric
    {
        private int _count;

        public int Count => _count;
        public override string MetricType => "counter";

        public ExternalCounter()
        {
        }

        public void Increment()
        {
            Interlocked.Increment(ref _count);
        }

        protected override void Serialize(MetricWriter writer, DateTime now)
        {
            var increment = Interlocked.Exchange(ref _count, 0);
            if (increment == 0)
                return;

            WriteValue(writer, increment, now);
        }
    }
}