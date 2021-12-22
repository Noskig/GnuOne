Christopher.

(Ta bort gnu innan du kör programmet, nytt script för DB)
Databas & API har standardiserats med samma namnuppbyggnad överallt.
Discussion/post/comments har compound primary key (ID (10 siffror-unixtime, sätts i API) + Email).
Främmande nyckel finns mellan myFriends & myFriendsFriends.


///////////////////////////////////////////////////////////
ATT GÖRA:
1. Skicka databasen åt motsatt håll.
2. Vänner som inte är true skickas över till myfriendsfriends.

EXTRA:


///////////////////////////////////////////////////////////
PROBLEM:
Vänner som inte är true skickas över till myfriendsfriends.


///////////////////////////////////////////////////////////
FRÅGOR:
*Scriptet är uppdaterat. Men finns både en scripts-klass samt Global-script? Används båda
*myFriends ersätter users-tabellen
*Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag? Sync-vänner-knapp i efterhand



///////////////////////////////////////////////////////////
KLART:
Man kan skicka friendRequest				
Man kan Neka friendRequest (Tar bort båda user från myFriends-tabellen i bådas databaser)					
Man kan acceptera vän (och då:	
# User i myfriends-tabellen blir TRUE i båda databaser,
# mina diskussions,						
# mina Posts,									
# mina Comments,									
# mina myfriends till andras myfriendsfriends
)			
Man kan ta bort en vän(och då:						
# Tar bort user i myfriends-tabellen i båda databaser,						
# mina Diskussions,								
# mina Posts,										
# mina Comments,									
# mina personens vänner i myFriendsFriends tas bort.
)
Man kan Get/Post/Put/Delete discussion			
Man kan Get/Post/Put/Delete post
Man kan Get/Post/Put/Delete comments


Lastupdated ändrar inte tid efter att ett mail har läst in*
Kombinera flera Mailsender-funktioner*


TILL STIG:
* Vad ska man kunna se?
	# När man blir vänner, ska man kunna se alla varandras diskussioner, inlägg och kommentarer? Men även inlägg & kommentarer från andra, som inte är ens vänner? 
* Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag? Sync-vänner-knapp i efterhand