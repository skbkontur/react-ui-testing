﻿using System;

using JetBrains.Annotations;

namespace SKBKontur.SeleniumTesting.Internals.ArgumentAssertions
{
    internal static class Argument
    {
        [ContractAnnotation("value:null => halt")]
        public static void ShouldBeNotNullOrWhiteSpace(string value, [InvokerParameterName] string argumentName)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(string.Format("{0} should be not empty", argumentName), argumentName);
        }
    }
}