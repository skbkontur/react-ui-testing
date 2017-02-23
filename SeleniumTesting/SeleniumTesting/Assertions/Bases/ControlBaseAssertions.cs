using System;
using System.Linq.Expressions;

using SKBKontur.SeleniumTesting.Assertions.ErrorMessages;
using SKBKontur.SeleniumTesting.Assertions.ErrorMessages.Expecations;

namespace SKBKontur.SeleniumTesting.Assertions.Bases
{
    public class ControlBaseAssertions<TControl, TAssertions>
        where TControl : ControlBase
        where TAssertions : ControlBaseAssertions<TControl, TAssertions>
    {
        public ControlBaseAssertions(IAssertable<TControl> subject)
        {
            Subject = subject;
        }

        protected IAssertable<TControl> Subject { get; set; }

        public AndContraint<TAssertions> Satisfy(Func<TControl, bool> condition, string z)
        {
            Subject.ExecuteAssert(condition, (x, m) => m.WithExpectation(new CustomMessageExpectation(z)));
            return new AndContraint<TAssertions>(this as TAssertions);
        }
 
        public AndContraint<TAssertions> Satisfy(Func<TControl, bool> condition, IValueExpectationFormatter expectationFormatter)
        {
            Subject.ExecuteAssert(condition, (x, m) => m.WithExpectation(expectationFormatter));
            return new AndContraint<TAssertions>(this as TAssertions);
        }
        
        public AndContraint<TAssertions> Satisfy(Func<TControl, bool> condition, Func<TControl, IValueExpectationFormatter> expectationFormatterFactory)
        {
            Subject.ExecuteAssert(condition, (x, m) => m.WithExpectation(expectationFormatterFactory(x)));
            return new AndContraint<TAssertions>(this as TAssertions);
        }

        public PropertyControlContext<TControl, TProperty, TAssertions> HaveProperty<TProperty>(Expression<Func<TControl, TProperty>> propertyPicker, string a)
        {
            return new PropertyControlContext<TControl, TProperty, TAssertions>(Subject, propertyPicker, a, this as TAssertions);
        }

        public PropertyControlContext<TControl, TProperty, TAssertions> HaveComplexProperty<TProperty>(Expression<Func<TControl, TProperty>> propertyPicker, string a)
        {
            return new PropertyControlContext<TControl, TProperty, TAssertions>(Subject, propertyPicker, a, this as TAssertions);
        }

        public AndContraint<TAssertions> BePresent()
        {
            Subject.ExecuteAssert(x => x.IsDisplayed(), (x, u) => u.WithExpectation(new PresenseExpectation()));
            return new AndContraint<TAssertions>(this as TAssertions);
        }

        public AndContraint<TAssertions> BeAbsent()
        {
            Subject.ExecuteAssert(x => !x.IsDisplayed(), (x, u) => u.WithExpectation(new AbsentExpectation()));
            return new AndContraint<TAssertions>(this as TAssertions);
        }
    }
}