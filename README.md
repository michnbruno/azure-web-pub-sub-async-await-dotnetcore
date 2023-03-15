# azure-webpubsub

Azure has a relatively new PaaS publisher/subscriber service, Azure Web PubSub PaaS Service, which seems to me is a 2nd pass on SignalR.

Origin of this effort:
-Wanted to have a look at this Azure Web PubSub PaaS Service offering as part of my exploring "Dapr" and other Azure PaaS messaging services more generally.
-Thought I would use .NET Async/Await as a context. Specifically, this work is an extension of the Microsoft Learn "Preparing Breakfast" example on Asynchronous Programming.

Demo Illustrates:
1. "Publish" of Microsoft Learn Example "Breakfast Events" to Azure PubSub Service from .NET 6.0 MVC/API Controller.
2. "Subscribe" by ASP.NET MVC View/UI to PubSub service and subsequent "emit" of the events via WebSocket to the UI.

Get the Code:
Github azure-webpubsub



A Few Additional Technical Notes:
If you decide to drill into this Web PubSub.... as with SignalR, there are alot of details/complexities as you get to the level of groups/users and then depending on what you strive to do in the UI, the "websocket" javascript implementation of course can become more complex.



I found some of the documentation on "connectionId" to be a little confusing. And landed on approach that seems to work just fine but may be a little simplified/broad for a production implementation. But it avoided getting to level of complexity that I am not yet clear about.

1. The keep it simple "publish" method is "SendToAll" clients. "SendToAll" does what you'd think it would do - sends the message to any connected "client". So in this context, anyone/everyone will see all messages published to UI.
2. So in this implementation, to avoid publishing to "All", I landed on an approach where I could still use the simple "SendToAll" but used the "group" for "scoping". By randomly generating a new "group" name and then using this "unique" group name for every new client or every hard refresh, it accomplishes similar result to as if there had been a user/browser/session filtering. So this example is similar to a "SPA". Your emitted messages are preserved with AJAX requests but hard refreshes, new browser instances establish "new" connection.
3. Would love to hear feedback from others on your thoughts and/or your approaches.
