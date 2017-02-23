using System;
using System.Linq;
using System.Text.RegularExpressions;

using SKBKontur.SeleniumTesting.Assertions.ErrorMessages.Expecations;
using SKBKontur.SeleniumTesting.Internals.Commons;

namespace SKBKontur.SeleniumTesting
{
    public static class PropertyControlContextExtension
    {
        public static AndContraint<TAssertions> BeOneOf<TAssertions>(this IPropertyControlContext<string, TAssertions> context, params string[] values)
        {
            return context.Satisfy(
                values.Contains,
                "ожидалось одним из",
                "ожидалось не одним из",
                Helpers.FormatStringValues(values, 2));
        }

        public static AndContraint<TAssertions> Match<TAssertions>(this IPropertyControlContext<string, TAssertions> context, string matchString)
        {
            var regex = MatchStringToRegex(matchString);
            return context.Satisfy(
                x => regex.IsMatch(x),
                "ожидалось подходящим под шаблон",
                "ожидалось не подходящим под шаблон",
                Helpers.FormatStringValue(matchString));
        }

        private static Regex MatchStringToRegex(string matchString)
        {
            return new Regex(
                matchString
                    .Replace("*", "__ASTERISK__")
                    .Replace("?", "__QUESTION__")
                    .With(Regex.Escape)
                    .Replace("__ASTERISK__", ".+?")
                    .Replace("__QUESTION__", "."));
        }

        public static AndContraint<TAssertions> HaveLength<TAssertions>(this IPropertyControlContext<string, TAssertions> context, int expectedLength)
        {
            return context.Satisfy(
                x => x.Length == expectedLength,
                string.Format("ожидалось, что будет иметь длину {0}", expectedLength),
                string.Format("ожидалось, что не будет иметь длину {0}", expectedLength));
        }

        public static AndContraint<TAssertions> BeEmpty<TAssertions>(this IPropertyControlContext<string, TAssertions> context)
        {
            return context.Satisfy(
                x => x == "",
                "ожидалось пустым",
                "ожидалось непустым");
        }

        public static AndContraint<TAssertions> EqualTo<TAssertions>(this IPropertyControlContext<string, TAssertions> context, string value)
        {
            return context.ExecuteAssert(x => x == value, m => m.WithExpectation(new ExactValueExpectation(value.ToString())));
        }

        public static AndContraint<TAssertions> Satisfy<TAssertions>(
            this IPropertyControlContext<string, TAssertions> context,
            Func<string, bool> condition,
            string expectsText, string negationExpectText, string expectedValue = null)
        {
            return context.ExecuteAssert(
                condition,
                m => m.WithExpectation(
                    new CustomStringExpectation(expectsText, negationExpectText, expectedValue)
                         )
                );
        }

        public static AndContraint<TAssertions> Contain<TAssertions>(this IPropertyControlContext<string, TAssertions> context, string value)
        {
            return context.ExecuteAssert(x => x.Contains(value), m => m.WithExpectation(new ContainsValueExpectation(value.ToString())));
        }

        public static AndContraint<TAssertions> EqualTo<TAssertions>(this IPropertyControlContext<int, TAssertions> context, int value)
        {
            return context.ExecuteAssert(x => x == value, m => m.WithExpectation(new ExactValueExpectation(value.ToString())));
        }

        public static AndContraint<TAssertions> BeTrue<TAssertions>(this IPropertyControlContext<bool, TAssertions> context)
        {
            return context.ExecuteAssert(x => x, m => m.WithExpectation(new BooleanValueExpectation(true)));
        }

        public static AndContraint<TAssertions> BeFalse<TAssertions>(this IPropertyControlContext<bool, TAssertions> context)
        {
            return context.ExecuteAssert(
                x => x,
                m => m.WithExpectation(new BooleanValueExpectation(false)));
        }

        public static AndContraint<TAssertions> MatchToRegex<TAssertions>(this IPropertyControlContext<string, TAssertions> context, Regex regex)
        {
            return context.ExecuteAssert(regex.IsMatch, m => m.WithExpectation(new MatchValueExpectation(regex.ToString())));
        }
    }
}