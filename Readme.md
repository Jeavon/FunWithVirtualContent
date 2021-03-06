# Umbraco - routing Vorto 1:1 repository content with language specific content #

An example using the mystical `UmbracoVirtualNodeByIdRouteHandler` and a custom `UrlProvider`
## 

**Notice: This is only a POC and is full of holes and potential issues, if you want to make it better please get in contact**

## The problem and node structure ##

A library of fruits (I've no idea why I chose fruit, something to do with organic fruit/veg boxes going in my head) that have 1:1 content using Vorto. Also language specific sites, we would like the fruits to render as virtual children of the box node in each languages site, we need the urls to be multi-lingual.

![](https://raw.githubusercontent.com/Jeavon/FunWithVirtualContent/master/Docs/Images/structure.jpg)

## Vorto ##

Determine a Vorto property to use to generate urls from, `FruitName`

![](https://raw.githubusercontent.com/Jeavon/FunWithVirtualContent/master/Docs/Images/vorto.jpg)

## Url Provider ##

Finds urls and returns them to the node in the backoffice so that links are correct and so that usage of `.Url` is correct

![](https://raw.githubusercontent.com/Jeavon/FunWithVirtualContent/master/Docs/Images/urlprovider.jpg)

## The result ##

Same node renders as if it was child within each Box page in the correct language and using the url generated from the Vorto property

![](https://raw.githubusercontent.com/Jeavon/FunWithVirtualContent/master/Docs/Images/result.jpg)

## The files ##

- FruitBoxUrlProvider.cs - Returns the urls to be displayed in the Umbraco back office
- FruitsController.cs - A MVC render controller that is executed to generate the model and return the correct view
- FruitsRouteHandler.cs - Simply implements UmbracoVirtualNodeByIdRouteHandler to find the required IPublishedContent
- UmbracoEvents.cs - Populates the RouteTable and passes the FruitsRouteHandler also plugs in the custom UrlProvider

## Getting Setup ##

Umbraco login:

Username: admin
Password: password

You need to use IIS and your hosts file with three domains:

- en.funwithvirtualcontent.local
- dk.funwithvirtualcontent.local
- se.funwithvirtualcontent.local