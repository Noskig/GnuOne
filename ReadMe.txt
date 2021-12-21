Christopher.

(Ta bort gnu innan du kör programmet, nytt script för DB)
Databas & API har standardiserats med samma namnuppbyggnad överallt.
Discussion/post/comments har compound primary key (ID (10 siffror-unixtime, sätts i API) + Email).
Främmande nyckel finns mellan myFriends & myFriendsFriends.


///////////////////////////////////////////////////////////
ATT GÖRA:

1. Man kan läggat till vän?
2. Man kan Neka ny vän?
3. Man kan acceptera vän?
	# Skickas Duskussions?
	# Posts?
	# Comments?
	# myfriends till myfriendsfriends?
4. Man kan ta bort en vän?
	# Försvinner vännen?
	# Diskussions?
	# Post?
	# Comments?
	# myFriendsFriends?

///////////////////////////////////////////////////////////
PROBLEM:
Lastupdated ändrar inte tid efter att ett mail har läst in?


///////////////////////////////////////////////////////////
FRÅGOR:
*Scriptet är uppdaterat. Men finns både en scripts-klass samt Global-script? Används båda? 
*myFriends ersätter users-tabellen?
*Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag???