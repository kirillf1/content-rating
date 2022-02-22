# content-rating
This is my first educational application.  
This project allows you to estimate content together with other users. This application is posted for criticism and in order to point out the main mistakes. 
Therefore, it is not difficult for anyone, point to them!

This project consists of two parts.

The first part is the Rating.Hub. Link:https://github.com/kirillf1/content-rating/tree/master/src/Services/Rating.
It provides authentication and passes data from the database. Interaction occurs through Rest API and SignalR. 
The application project implements the basic commands and queries necessary for the interaction of presentation projects.

The second part is Blazor app. Link: https://github.com/kirillf1/content-rating/tree/master/src/Web
At the moment, the entire visual part is made schematically and ugly. Since I don't have sufficient knowledge in the frontend. But I will try to solve this problem in the near future.
Since I am from Russia and actively use it with friends, unfortunately all the text in html is in Russian.
Through the visual part, you can create rooms for estimate, you can view already rated rooms, etc. You must register to create and rate.

So far, to run locally, you need to run two projects separately and Web.Client/wwwroot/appsettings.json add the addresses that the client part will access
