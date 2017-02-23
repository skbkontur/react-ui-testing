using System;
using System.Globalization;
using System.Linq;
using System.Text;

using Humanizer;

using JetBrains.Annotations;

using Newtonsoft.Json;

using OpenQA.Selenium;

using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Context;

namespace SKBKontur.SeleniumTesting
{
    public class ControlBase
    {
        protected ControlBase(ISearchContainer container, ISelector selector)
        {
            this.container = container;
            this.selector = selector;
        }

        public virtual bool IsDisplayed()
        {
            try
            {
                return ExecuteOnElement(element => element.Displayed);
            }
            catch
            {
                return false;
            }
        }

        public void Click()
        {
            EnsureElementExistsAndExecute(x => x.Click(), "Click");
        }

        protected TResult ExecuteOnElement<TResult>(Func<IWebElement, TResult> action)
        {
            try
            {
                return action(GetNativeElement());
            }
            catch(StaleElementReferenceException)
            {
                ClearCachedElement();
                return action(GetNativeElement());
            }
        }

        protected void ExecuteOnElement(Action<IWebElement> action)
        {
            try
            {
                action(GetNativeElement());
            }
            catch(StaleElementReferenceException)
            {
                ClearCachedElement();
                action(GetNativeElement());
            }
        }

        protected void EnsureElementExistsAndExecute(Action<IWebElement> action, string actionDescription)
        {
            try
            {
                Waiter.Wait(() => GetNativeElement().Displayed, (timeout, exception) => GetZ(timeout, actionDescription, exception), AssertionsContext.GetDefaultWaitInterval());
                action(GetNativeElement());
            }
            catch(StaleElementReferenceException)
            {
                ClearCachedElement();
                action(GetNativeElement());
            }
        }

        protected T EnsureElementExistsAndExecute<T>(Func<IWebElement, T> action, string actionDescription)
        {
            try
            {
                Waiter.Wait(() => GetNativeElement().Displayed, (timeout, exception) => GetZ(timeout, actionDescription, exception), AssertionsContext.GetDefaultWaitInterval());
                return action(GetNativeElement());
            }
            catch(StaleElementReferenceException)
            {
                ClearCachedElement();
                return action(GetNativeElement());
            }
        }

        private string GetZ(TimeSpan timeout, string actionDescription, Exception exception)
        {
            var result = new StringBuilder();
            result.AppendLine(string.Format("{0}({1}): требовалось действие {2}, но", GetControlTypeDesription(), GetAbsolutePathBySelectors(), actionDescription));
            if(exception is ElementNotFoundException)
            {
                var notFountException = exception as ElementNotFoundException;
                result.AppendLine(string.Format("  не смогли долждаться присутсвия элемента: {0}({1})", notFountException.Control.GetControlTypeDesription(), notFountException.Control.GetAbsolutePathBySelectors()));
                result.AppendLine(string.Format("Время ожидания: {0}.", timeout.Humanize(culture : CultureInfo.GetCultureInfo("ru-RU"))));
            }
            else
            {
                result.AppendLine(string.Format("  не смогли долждаться присутсвия элемента (время ожидания: {0}), т.к. было получено исключение:", timeout.Humanize(culture : CultureInfo.GetCultureInfo("ru-RU"))));
                result.AppendLine(exception.ToString());
            }
            return result.ToString();
        }

        internal void ClearCachedElement()
        {
            cachedContext = null;
        }

        protected T GetReactProp<T>(string propName)
        {
            var propValue = ExecuteOnElement(x => x.GetAttribute(string.Format("data-prop-{0}", propName)));
            if(typeof(T) == typeof(string))
            {
                return (T)(object)propValue;
            }
            if(propValue == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(propValue);
        }

        [NotNull]
        private IWebElement GetNativeElement()
        {
            try
            {
                if(cachedContext != null)
                {
                    if(!selector.MatchElement(cachedContext))
                    {
                        ClearCachedElement();
                        Console.WriteLine("Cached element cleared for {0}", selector);
                    }
                }
                return (cachedContext ?? (cachedContext = container.Search(selector)));
            }
            catch(NoSuchElementException ex)
            {
                throw new ElementNotFoundException(this, container, GetType(), selector, ex);
            }
        }

        [NotNull]
        [Obsolete]
        public virtual string GetText()
        {
            return ExecuteOnElement(element => element.Text);
        }

        [Obsolete]
        public virtual bool IsDisabled()
        {
            return GetNativeElement().GetAttribute("disabled") == "true";
        }

        [NotNull]
        public string GetAbsolutePathBySelectors()
        {
            return string.Join(
                " ",
                new[] {container.GetAbsolutePathBySelectors(), selector.ToString()}
                    .Where(x => x != null)
                );
        }

        public string GetControlTypeDesription()
        {
            return GetType().Name;
        }

        private IWebElement cachedContext;

        private readonly ISelector selector;
        protected readonly ISearchContainer container;
    }
}