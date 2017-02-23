using System;
using System.Linq;

using MoreLinq;

using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Bases;
using SKBKontur.SeleniumTesting.Assertions.ErrorMessages.Expecations;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class ControlListAssertions<T> : ControlBaseAssertions<ControlList<T>, ControlListAssertions<T>> where T : ControlBase
    {
        public ControlListAssertions(IAssertable<ControlList<T>> subject)
            : base(subject)
        {
        }

        public ControlListItemsAssertions<T> HaveItems()
        {
            return new ControlListItemsAssertions<T>(Subject);
        }

        public void HaveCount(int exepectedCount)
        {
            Count.EqualTo(exepectedCount);
        }

        public PropertyControlContext<ControlList<T>, int, ControlListAssertions<T>> Count { get { return HaveProperty(x => x.Count, "количество элементов"); } }

        public AndContraint<ControlListAssertions<T>> AllItemsEquivalentTo(Func<T, string> propertySelector, string[] expected)
        {
            Subject.ExecuteAssert(
                x => ArraysEquivalent(x.GetItems().Select(propertySelector).ToArray(), expected),
                (x, m) =>
                    {
                        m = m.WithExpectation(new ArraysEquivalentExpectation(expected));
                        x.GetItems().Select(propertySelector).ForEach(a => m.WithActual(a));
                        return m;
                    });
            return new AndContraint<ControlListAssertions<T>>(this);
        }

        private bool ArraysEquivalent(string[] array1, string[] array2)
        {
            return array1.Length == array2.Length && !array1.Except(array2).Any() && !array2.Except(array1).Any();
        }
    }
}