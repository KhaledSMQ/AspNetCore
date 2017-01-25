﻿// Copyright (c) Allan Hardy. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using App.Metrics.Abstractions.Metrics;
using App.Metrics.Gauge.Interfaces;

namespace App.Metrics.Gauge
{
    /// <summary>
    ///     A Gauge metric using a function to provide the instantaneous value to record
    /// </summary>
    /// <seealso cref="IGaugeMetric" />
    public class FunctionGauge : IGaugeMetric
    {
        private readonly Func<double> _valueProvider;

        public FunctionGauge(Func<double> valueProvider) { _valueProvider = valueProvider; }

        /// <inheritdoc />
        public double Value
        {
            get
            {
                try
                {
                    return _valueProvider();
                }
                catch (Exception)
                {
                    return double.NaN;
                }
            }
        }

        /// <inheritdoc />
        public double GetValue(bool resetMetric = false) { return Value; }
    }
}