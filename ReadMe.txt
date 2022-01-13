﻿Christopher.

(Ta bort gnu innan du kör programmet, nytt script för DB)
Databas & API har standardiserats med samma namnuppbyggnad överallt.
Discussion/post/comments har compound primary key (ID (10 siffror-unixtime, sätts i API) + Email).
Främmande nyckel finns mellan myFriends & myFriendsFriends.

///////////////////////////////////////////////////////////
NY VERSION:
Man kan skicka friendRequest				
Man kan Neka friendRequest (Tar bort båda user från myFriends-tabellen i bådas databaser)					
Man kan acceptera vän (och då:	
# User i myfriends-tabellen blir TRUE i båda databaser, samt skickas
# mina diskussions,						
# mina Posts,									
# mina Comments,									
# mina myfriends till andras myfriendsfriends
)			
Man kan ta bort en vän(och då:						
# Tar bort user i myfriends-tabellen i båda databaser, samt tas följande bort:						
# mina Diskussions,								
# mina Posts,										
# mina Comments,									
# mina personens vänner i myFriendsFriends tas bort.
) (Behåller post & comments, som ej ligger under vännens egna diskussioner)
Man kan Get/Post/Put/Delete discussion			
Man kan Get/Post/Put/Delete post
Man kan Get/Post/Put/Delete comments

///////////////////////////////////////////////////////////
ATT GÖRA: (utan inbördes ordning)

1.   Jag vill kunna hämta ner och installera programmet och köra det ifrån en webbrowser. (bittorrent eller iwantag.nu?)
Skriva instruktion och testa att det går
(Få första vännen och komma in i systemet. FRONTEND ruta som man skriver i mailen på den som man vill bli vän med)
-Bjuda in ny användare - Ny sida som förklarar grejer.. Vad man behöver ta ner och att man skapar mail osv.
-Stegvis förklaring av installationsprocessen.
--Text
--Video

2.    Jag vill kunna se vem som finns i systemet.
Vill vi ha både publika och privata vänner?

<<<<<<< Updated upstream
3.    Jag vill kunna lägga till vänner.
-(FRONTEND)Skapa en process för detta när man har kommit in i systemet.
-(FRONTEND notiser, knapp för accept/denied)

=======
>>>>>>> Stashed changes
6.    Gå igenom källkoden, dokumentera, städa, fundera på standard.

7.    TESTA
--Create, Edit, Delete, (FRONTEND, Read)
--Create, Edit, Delete, (FRONTEND, Read)
---(Lägga till med FRONTEND)

--Nya funktioner
8.    Föreslå nya funktioner 
**Chatt  (LiveChatt i webben..  JS.. Gun.js Chat)
9.    Fixa fel och införa ändringar
-Göra om allt till samma standard. Namnsättning framförallt.(på börjad kanske klar)



10. Resterade.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

**Dokumentera github. Text. Video. Göra en snygg förklaring. Guida Stig
-Få prototyp redo, sen dokumentera V.1

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
????????
 Publik nyckel.

-Gömma sig i nätverken hos vänner. (bli osynlig för deras nätverk, inställning)
//bool var?
- Behöver 2 bool i myFriends (me/youHiding)
- Ändrar man på den så tas man bort/läggs till i friendsfriends
- Se hans posts på väns discussion utan hans namn?

????????

4.    Jag vill ha en sida där jag kan se vilka inställningar som är gjorda och eventuellt ändra valda parametrar.
-API som ändrar inställningar.
--Username
--Darkmode?


5.    Fixa till asymmetrisk kryptering/verifiering.
- Kolla vad Marcus har.
--Vilken information ska krypteras? Mailen? 
- Maillösenord hashas in i databasen? (Secret key?)


-__Testa postman lite hejvilt_

*APIsync för att synca discussion, post, vänner och vännersvänners ifall att nått skulle bli fel.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

----- Slutet av veckan.. En fungerande prototyp ------

///////////////////////////////////////////////////////////
EXTRA-JOBB:
** vi har marcus som extra resurs
** deletea konto
- Foreach tabort vänner. 
- Script för att ta bort databasen
- Try catch block på all sparning i controllers?
- Kunna blocka någon?

///////////////////////////////////////////////////////////
PROBLEM:
Skickar ett extra friendacceptmail tillbaka efter båda blivit vänn. Behöver inte vara.

///////////////////////////////////////////////////////////
BEHÖVS TESTAS
FriendRequest. -hänger alla id:n med på korrekt sätt? Discussion, posts osv..
** Dubbel kolla alla foreach mailutskick loopar att de inte skickar till vänner man inte är vän med "(if (user.isFriend == false) { continue; })"
** Profil.

///////////////////////////////////////////////////////////
KLART:
# Lastupdated ändrar inte tid efter att ett mail har läst in*
# Kombinera flera Mailsender-funktioner*
# Skicka databasen åt motsatt håll.
# Vänner som inte är true skickas över till myfriendsfriends.
# deleteFriend tar bort vännen, diskussioner, poster & vänsVänner. Åt båda hållet.
# När man blir vän med någon. Behöver det gå ut ett mail till mina vänner med en som uppdaterar FriendsFriend 7/1
# Bug #1. Lösning. När någon postar på min discussion. Behöver den posten studsa från min till alla mina vänner som har den diskussionen(alla).
# standardbilder 5st
# Intressen/Tags
# Kunna visa sina favorit intressen på sin profil.
*Vi vill kunna uppdatera myprofile, göra så att userinfon skickas till vänner (11/1)


# Gör om SQl-query till JSON och skickar i mailen för discussion, comment och post. 5/1
# Göra om i Myfriendscontrollern rad 82-88 till metod   Behöver gå ut ett mail till mina vänner att vännen tas bort 5/1
# Fixa scriptet så det matchar den nya DB 7/1
# Rätt information ska skickas ut när man blir vän också

# Posts som tas bort ska ligga kvar men bli deletat.
# Filtrera på tags
# userInfo. Intressen(bool).

///////////////////////////////////////////////////////////
FRÅGOR:
*Scriptet är uppdaterat. Men finns både en scripts-klass samt Global-script? Används båda
*myFriends ersätter users-tabellen
*Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag? Sync-vänner-knapp i efterhand


TILL STIG:
* Vad ska man kunna se?
	# När man blir vänner, ska man kunna se alla varandras diskussioner, inlägg och kommentarer? Men även inlägg & kommentarer från andra, som inte är ens vänner? 
* Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag? Sync-vänner-knapp i efterhand
