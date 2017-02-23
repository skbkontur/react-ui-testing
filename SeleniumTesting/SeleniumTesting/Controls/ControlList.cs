using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using SKBKontur.SeleniumTesting.Internals.Selectors;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class ControlList<TItem> : CompoundControl where TItem : ControlBase
    {
        public ControlList([NotNull] ISearchContainer container, [NotNull] ISelector selector, [NotNull] ISelector itemSelector)
            : base(container, selector)
        {
            this.itemSelector = itemSelector;
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
            var elements = ExecuteOnElement(x => x.FindElements(itemSelector.SeleniumBy));
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

        [CanBeNull]
        public TItem GetItemByName(string name)
        {
            return GetItems().FirstOrDefault(x => x.GetText() == name);
        }

        [CanBeNull]
        public TItem GetItemByUniqueTid(UniversalSelector tid)
        {
            return GetItemInstance(tid);
        }

        private TItem GetItemInstance(ISelector selector)
        {
            return Activator.CreateInstance(typeof(TItem), this, selector) as TItem;
        }

        private readonly ISelector itemSelector;
    }
}