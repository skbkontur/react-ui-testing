﻿using System;
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

        public TAssertions During(TimeSpan? timeout)
        {
            if (timeout.HasValue)
                Subject.WaitInterval = timeout.Value;
            return this as TAssertions;
        }

        public TAssertions During(double? timeoutInMilliseconds)
        {
            return During(timeoutInMilliseconds.HasValue ? TimeSpan.FromMilliseconds(timeoutInMilliseconds.Value) : (TimeSpan?)null);
        }

        protected IAssertable<TControl> Subject { get; set; }

        protected IAndContraint<TAssertions> AndThis()
        {
            return new AndContraint<TAssertions>(this as TAssertions);
        }

        public IAndContraint<TAssertions> Satisfy(Func<TControl, bool> condition, string z)
        {
            Subject.ExecuteAssert(condition, (x, m) => m.WithExpectation(new CustomMessageExpectation(z)));
            return AndThis();
        }
 
        public IAndContraint<TAssertions> Satisfy(Action<TControl> action, string z)
        {
            ExecFluentAssertings(action, z);
            return AndThis();
        }
 
        public IAndContraint<TAssertions> ExecFluentAssertings(Action<TControl> condition, string z)
        {
            Subject.ExecuteAssert((x) =>
                {
                    try
                    {
                        condition(x);
                    }
                    catch(Exception exception)
                    {
                        return new ExceptionResult(exception);
                    }
                    return new ExceptionResult(null);
                }, (x, m, y) => m.WithExpectation(new CustomMessageExpectation(z + "\n" + y.Exception.Message)));
            return AndThis();
        }
 
        public IAndContraint<TAssertions> Satisfy(Func<TControl, bool> condition, IValueExpectationFormatter expectationFormatter)
        {
            Subject.ExecuteAssert(condition, (x, m) => m.WithExpectation(expectationFormatter));
            return AndThis();
        }
        
        public IAndContraint<TAssertions> Satisfy(Func<TControl, bool> condition, Func<TControl, IValueExpectationFormatter> expectationFormatterFactory)
        {
            Subject.ExecuteAssert(condition, (x, m) => m.WithExpectation(expectationFormatterFactory(x)));
            return AndThis();
        }

        public PropertyControlContext<TControl, TProperty> HaveProperty<TProperty>(Expression<Func<TControl, TProperty>> propertyPicker, string a)
        {
            return new PropertyControlContext<TControl, TProperty>(Subject, propertyPicker, a);
        }

        public PropertyControlContext<TControl, TProperty> HaveComplexProperty<TProperty>(Expression<Func<TControl, TProperty>> propertyPicker, string a)
        {
            return new PropertyControlContext<TControl, TProperty>(Subject, propertyPicker, a);
        }

        public IAndContraint<TAssertions> BeDisplayed()
        {
            Subject.ExecuteAssert(x => x.IsDisplayed, (x, u) => u.WithExpectation(new PresenseExpectation()));
            return AndThis();
        }

        public IAndContraint<TAssertions> BeNotDisplayed()
        {
            Subject.ExecuteAssert(x => !x.IsDisplayed, (x, u) => u.WithExpectation(new AbsentExpectation()));
            return AndThis();
        }
    }

    public class ExceptionResult : ICheckResult
    {
        public Exception Exception { get; private set; }

        public ExceptionResult(Exception exception)
        {
            Exception = exception;
        }

        public bool Valid { get { return Exception == null; } }
    }
}