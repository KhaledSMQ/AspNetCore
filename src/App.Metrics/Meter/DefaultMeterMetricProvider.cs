﻿// Copyright (c) Allan Hardy. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using App.Metrics.Abstractions.Metrics;
using App.Metrics.Core.Options;
using App.Metrics.Interfaces;
using App.Metrics.Meter.Interfaces;
using App.Metrics.Registry.Interfaces;

namespace App.Metrics.Meter
{
    public class DefaultMeterMetricProvider : IProvideMeterMetrics
    {
        private readonly IClock _clock;
        private readonly IBuildMeterMetrics _meterBuilder;
        private readonly IMetricsRegistry _registry;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultMeterMetricProvider" /> class.
        /// </summary>
        /// <param name="meterBuilder">The meter builder.</param>
        /// <param name="registry">The metrics registry.</param>
        /// <param name="clock">The clock.</param>
        public DefaultMeterMetricProvider(IBuildMeterMetrics meterBuilder, IMetricsRegistry registry, IClock clock)
        {
            _registry = registry;
            _clock = clock;
            _meterBuilder = meterBuilder;
        }

        /// <inheritdoc />
        public IMeter Instance(MeterOptions options) { return Instance(options, () => _meterBuilder.Build(_clock)); }

        /// <inheritdoc />
        public IMeter Instance<T>(MeterOptions options, Func<T> builder)
            where T : IMeterMetric { return _registry.Meter(options, builder); }
    }
}