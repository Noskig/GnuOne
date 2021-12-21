Christopher.

(Ta bort gnu innan du kör programmet, nytt script för DB)
Databas & API har standardiserats med samma namnuppbyggnad överallt.
Discussion/post/comments har compound primary key (ID (10 siffror-unixtime, sätts i API) + Email).
Främmande nyckel finns mellan myFriends & myFriendsFriends.


///////////////////////////////////////////////////////////
ATT GÖRA:
1. Skicka databasen åt motsatt håll.

///////////////////////////////////////////////////////////
PROBLEM:



///////////////////////////////////////////////////////////
FRÅGOR:
*Scriptet är uppdaterat. Men finns både en scripts-klass samt Global-script? Används båda? 
*myFriends ersätter users-tabellen?
*Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag???


///////////////////////////////////////////////////////////
KLART:
Man kan skicka friendsrequest?				
Man kan Neka ny vän?						
Man kan acceptera vän?						 
# Skickas Duskussions?						
# Posts?									
# Comments?									
# myfriends till myfriendsfriends?			
Man kan ta bort en vän?						
# Försvinner vännen?						
# Diskussions?								
# Post?										
# Comments?									
# myFriendsFriends?							
Man kan ändra/ta bort en discussion			
Man kan ändra/ta bort en post				
Tar man bort vän, försvinner myfriendsfriends, disc, post, comment YES

Lastupdated ändrar inte tid efter att ett mail har läst in?