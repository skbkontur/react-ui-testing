using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using SKBKontur.SeleniumTesting.Internals.Selectors;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class ControlListBase<TItem> : CompoundControl, IEnumerable<TItem> where TItem : ControlBase
    {
        public ControlListBase([NotNull] ISearchContainer container, [NotNull] ISelector selector, [NotNull] ISelector itemSelector, Func<ISearchContainer, ISelector, TItem> createItem)
            : base(container, selector)
        {
            this.itemSelector = itemSelector;
            this.createItem = createItem;
        }

        [NotNull]
        public TItem this[int index] { get { return GetItemInstance(CreateItemSelector(index)); } }

        private ISelector CreateItemSelector(int index)
        {
            return new SelectorWithIndexWrapper(itemSelector, index);
        }

        public int Count { get { return GetItems().Count; } }

        [NotNull]
        public List<TItem> GetItems()
        {
            var elements = GetValueFromElement(x => x.FindElements(itemSelector.SeleniumBy));
            return Enumerable
                .Range(0, elements.Count)
                .Select(i => GetItemInstance(CreateItemSelector(i)))
                .ToList();
        }

        [NotNull]
        public string GetRelativePathToItems()
        {
            return itemSelector.ToString();
        }

//        [CanBeNull]
//        public TItem GetItemByName(string name)
//        {
//            return GetItems().FirstOrDefault(x => x.GetText() == name);
//        }

        [CanBeNull]
        public TItem GetItemByUniqueTid(UniversalSelector tid)
        {
            return GetItemInstance(tid);
        }

        protected virtual TItem GetItemInstance(ISelector selector)
        {
            return createItem(this, selector);
        }

        private readonly ISelector itemSelector;
        private readonly Func<ISearchContainer, ISelector, TItem> createItem;

        public virtual IEnumerator<TItem> GetEnumerator()
        {
            if (!IsDisplayed)
                return (new List<TItem>()).GetEnumerator();
            return GetItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}