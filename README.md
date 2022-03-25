# content-rating
This is my first educational application.  
This project allows you to estimate content together with other users. This application is posted for criticism and in order to point out the main mistakes. 
Therefore, it is not difficult for anyone, point to them!

The project consists of 3 parts.

The first part is the Rating.Hub. Link:https://github.com/kirillf1/content-rating/tree/master/src/Services/Rating.
It provides authentication and passes data from the database. Interaction occurs through Rest API and SignalR. 
The application project implements the basic commands and queries necessary for the interaction of presentation projects.

The second part is Blazor app. Link: https://github.com/kirillf1/content-rating/tree/master/src/Web
At the moment, the entire visual part is made schematically and ugly. Since I don't have sufficient knowledge in the frontend. But I will try to solve this problem in the near future. You can also guess the content. At the moment, YouTube video and audio files are implemented, which are transmitted by link. The principle of operation is to hide the video or audio and transfer the answer options.
Through the visual part, you can create rooms for estimate, you can view already rated rooms, etc. You must register to create and rate. Then you can 
Unfortunately all the text in html is in Russian.

The third part is the REST API which contains the CRUD operation for the content to guess. Linq: https://github.com/kirillf1/content-rating/tree/master/src/Services/ContentGuess Content contains author title, link to content, tags and content type (currently YouTube video and audio files)


