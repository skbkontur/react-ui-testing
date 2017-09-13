using System;
using System.Collections.Generic;
using System.Linq;

using Kontur.RetryableAssertions.Configuration;
using Kontur.RetryableAssertions.ValueProviding;

using NUnit.Framework.Constraints;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Tests.Helpers
{
    public static class AssertionExtensions
    {
        public static IValueProvider<T, T> Wait<T>(this IControlProperty<T> property)
        {
            return ValueProvider.Create(property.Get, property.GetDescription);
        }

        public static IValueProvider<T[], T[]> Wait<T>(this IEnumerable<IControlProperty<T>> properties)
        {
            return ValueProvider.Create(() => properties.Select(x => x.Get()).ToArray(), string.Join("\n", properties.Select(x => x.GetDescription())));
        }

        public static IAssertionResult<T, TSource> That<T, TSource>(this IValueProvider<T, TSource> provider, IResolveConstraint constraint)
        {
            var configuration = new AssertionConfiguration<T>
                {
                    Timeout = 2000,
                    Interval = 100,
                    Assertion = Assertion.FromDelegate<T>(x => NUnit.Framework.Assert.That(x, new ReusableConstraint(constraint))),
                    ExceptionMatcher = ExceptionMatcher.FromTypes(typeof(WebDriverException), typeof(InvalidOperationException), typeof(ElementNotFoundException))
                };
            return Kontur.RetryableAssertions.Wait.Assertion(provider, configuration);
        }

        public static void Assert<T>(this IControlProperty<T> property, IResolveConstraint constraint)
        {
            NUnit.Framework.Assert.That(property.Get(), new ReusableConstraint(constraint), property.GetDescription());
        }

        public static void Assert<T>(this T value, IResolveConstraint constraint, string message = null)
        {
            NUnit.Framework.Assert.That(value, new ReusableConstraint(constraint), message);
        }
    }
}
