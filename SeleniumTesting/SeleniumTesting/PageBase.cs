using System;
using System.Collections.Generic;
using System.Linq;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using SKBKontur.SeleniumTesting.Internals;

namespace SKBKontur.SeleniumTesting
{
    public interface IPageActionAttribute
    {
        void OnInit(PageBase pageInstace);
    }

    internal static class ReflectionExtensions
    {
        public static IEnumerable<T> GetCurrentTypeAttributes<T>(this Type type)
        {
            if(type == null || type == typeof(object))
                return new T[0];
            var baseInterfaces = new List<Type>();
            if(type.BaseType != null)
                baseInterfaces = type.BaseType.GetInterfaces().ToList();
            return new[]
                {
                    GetCurrentTypeAttributes<T>(type.BaseType),
                    type.GetInterfaces().Where(x => !baseInterfaces.Contains(x)).SelectMany(GetCurrentTypeAttributes<T>),
                    type.GetCustomAttributes(typeof(IPageActionAttribute), false).Cast<T>(),
                }.SelectMany(x => x);
        }
    }

    public class PageBase : ISearchContainer
    {
        protected PageBase(RemoteWebDriver webDriver)
        {
            this.webDriver = webDriver;
            ExecuteInitAction();
        }

        private void ExecuteInitAction()
        {
            foreach(var pageActionAttribute in GetType().GetCurrentTypeAttributes<IPageActionAttribute>())
                pageActionAttribute.OnInit(this);
        }

        public IWebElement Search(ISelector selector)
        {
            return webDriver.FindElement(selector.SeleniumBy);
        }

        public IWebElement SearchGlobal(ISelector selector)
        {
            return Search(selector);
        }

        public object ExecuteScript(string script, params object[] arguments)
        {
            if(string.IsNullOrWhiteSpace(script))
                throw new ArgumentException("script");
            try
            {
                webDriver.ExecuteScript("window.callArgs = arguments", arguments);
                return webDriver.ExecuteScript(script, arguments);
            }
            catch(StaleElementReferenceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("Can't execute script \"{0}\"", script), ex);
            }
        }

        public virtual void WaitLoaded()
        {
            WaitControlsMarkedWithAttribute();
        }

        private void WaitControlsMarkedWithAttribute()
        {
            var propertyInfos = GetType().GetProperties().Where(prop => prop.IsDefined(typeof(LoadingCompleteAttribute), false));
            var properties = propertyInfos.Select(x => x.GetValue(this)).OfType<ControlBase>().ToArray();
            Waiter.Wait(() => properties.All(x => x.IsDisplayed()), "Загрузка страницы");
        }

        public string GetAbsolutePathBySelectors()
        {
            return null;
        }

        public RemoteWebDriver DangerousGetWebDriverInstance()
        {
            return webDriver;
        }

        public TPage GoTo<TPage>() where TPage : PageBase
        {
            return InitializePage<TPage>(webDriver);
        }

        public static TPage InitializePage<TPage>(RemoteWebDriver webDriver) where TPage : PageBase
        {
            TPage page;
            if(typeof(ReactPage).IsAssignableFrom(typeof(TPage)))
            {
                page = (TPage)Activator.CreateInstance(typeof(TPage));
                if(page == null)
                    throw new InvalidOperationException("Page cannot be null");
                // ReSharper disable once PossibleNullReferenceException
                (page as ReactPage).SetRemoteWebDriver(webDriver);
            }
            else
            {
                page = (TPage)Activator.CreateInstance(typeof(TPage), webDriver);
                if(page == null)
                    throw new InvalidOperationException("Page cannot be null");
            }
            page.WaitLoaded();
            return page;
        }

        protected internal RemoteWebDriver webDriver;
    }
}