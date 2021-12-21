(Ta bort gnu innan du kör programmet, nytt script för DB)


Databas & API har standardiserats med samma namnuppbyggnad överallt.
Scriptet är uppdaterat. Men finns både en scripts-klass samt Global-script? Används båda? 


myFriends ersätter users-tabellen?


Discussion/post/comments har compound primary key (ID (10 siffror-unixtime, sätts i API) + Email).
Främmande nyckel finns mellan myFriends & myFriendsFriends.

1. Man kan läggat till vän.
2. Man kan acceptera vän, då skickas Diskussions & myfriends(till myfriendsfriends).
