# Переезд
Содержимое репозитория react-ui-testing переехало в [подрепозеторий reatil-ui](https://github.com/skbkontur/retail-ui/tree/master/packages/react-ui-testing)

# react-ui-testing

[![TeamCity](https://tcat.skbkontur.ru/app/rest/builds/buildType:(id:ReactUiSeleniumTesting_Tests)/statusIcon)](https://tcat.skbkontur.ru/project.html?projectId=ReactUiSeleniumTesting&tab=projectOverview)

Набор инструментов для тестирования фронтэнд приложений написаных на React-е, в том числе с использованием библиотеки
[react-ui](https://github.com/skbkontur/retail-ui).

## Как это работает

Библиотека состоит из двух частей:

* [Скрипт](http://tech.skbkontur.ru/react-ui-testing/#/expose-tids-to-dom), поключаемый на страницу, который транслирует props и другую полезную информацию о React-компонентах в DOM.
* [Набор PageObject'ов](http://tech.skbkontur.ru/react-ui-testing/#/page-objects-dot-net) для доступа к [компонентам react-ui](https://github.com/skbkontur/retail-ui) через [Selenium для .NET](http://www.seleniumhq.org/docs/03_webdriver.jsp#c).

## А что дальше?

* Прочитайте [инструкцию по быстрому старту](http://tech.skbkontur.ru/react-ui-testing/#/quick-start), чтобы проверить что всё работает
* Узнайте как [подключить скрипт](http://tech.skbkontur.ru/react-ui-testing/#/expose-tids-to-dom) к своему приложению
* Используйте [API reference по PageObject'ам для .NET](http://tech.skbkontur.ru/react-ui-testing/#/page-objects-dot-net)
* Используйте [Bookmarket](http://tech.skbkontur.ru/react-ui-testing/#/bookmarklet) для просмотра tid-атрибутов страницы.
