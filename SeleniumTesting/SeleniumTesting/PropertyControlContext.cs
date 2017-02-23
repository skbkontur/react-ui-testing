using System;
using System.Linq.Expressions;

using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.ErrorMessages;

namespace SKBKontur.SeleniumTesting
{
    public class PropertyControlContext<TControl, T, TAssertions> : IPropertyControlContext<T, TAssertions> where TControl : ControlBase
    {
        public PropertyControlContext(
            IAssertable<TControl> subject,
            Expression<Func<TControl, T>> propertyPicker,
            string target,
            TAssertions assertions
            )
        {
            this.subject = subject;
            this.propertyPicker = propertyPicker;
            this.target = target;
            this.assertions = assertions;
            this.compiledPropertyPicker = propertyPicker.Compile();
        }

        public PropertyControlContext<TControl, T, TAssertions> Not()
        {
            this.negation = true;
            return this;
        }

        public AndContraint<TAssertions> ExecuteAssert(Func<T, bool> func, Func<IErrorMessageBuilder, IErrorMessageBuilder> messageBuilder)
        {
            if(negation)
            {
                subject.ExecuteAssert(
                    x => !func(compiledPropertyPicker(x)),
                    (x, m) =>
                        {
                            var result = messageBuilder(m).WithNegation().WithPropertyDescription(target);
                            if(x != null && x.IsDisplayed())
                                result.WithActual(compiledPropertyPicker(x).ToString());
                            return result;
                        }
                    );
                return new AndContraint<TAssertions>(assertions);
            }
            subject.ExecuteAssert(
                x => func(compiledPropertyPicker(x)),
                (x, m) =>
                    {
                        var result = messageBuilder(m).WithPropertyDescription(target);
                        if(x != null && x.IsDisplayed())
                            result.WithActual(compiledPropertyPicker(x).ToString());
                        return result;
                    }
                );
            return new AndContraint<TAssertions>(assertions);
        }

        private readonly IAssertable<TControl> subject;
        private readonly Expression<Func<TControl, T>> propertyPicker;
        private readonly string target;
        private readonly TAssertions assertions;
        private readonly Func<TControl, T> compiledPropertyPicker;
        private bool negation;
    }
}