Christopher.

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



1.    Jag vill kunna hämta ner och installera programmet och köra det ifrån en webbrowser. (bittorrent eller iwantag.nu?)
Skriva instruktion och testa att det går
(Få första vännen och komma in i systemet. FRONTEND ruta som man skriver i mailen på den som man vill bli vän med)
-Bjuda in ny användare - Ny sida som förklarar grejer.. Vad man behöver ta ner och att man skapar mail osv.
-Stegvis förklaring av installationsprocessen.
--Text
--Video

2.    Jag vill kunna se vem som finns i systemet.
Vill vi ha både publika och privata vänner?

"Gömma" att man är vän med någon. Så att deras vänner inte ser mig.
-Myfreinds tabellen. Extra fält som visar om man ska visas i deras netvärk eller ej? 
--Behöver studsa en extra gång ut till vännerna som kanske redan har en i deras Friendsfriend-listan

3.    Jag vill kunna lägga till vänner.
Skapa en process för detta när man har kommit in i systemet.
-(FRONTEND notiser, knapp för accept/denied)
--Rätt information ska skickas ut när man blir vän också... Testa

4.    Jag vill ha en sida där jag kan se vilka inställningar som är gjorda och eventuellt ändra valda parametrar.
-API som ändrar inställningar.
--Case på mailreader som ändrar i nästa steg.

5.    Fixa till asymmetrisk kryptering/verifiering.
-Kolla vad Marcus har.

6.    Gå igenom källkoden, dokumentera, städa, fundera på standard.
Guida Stig
-Dokumentera github. Text. Video. Göra en snygg förklaring.
-städa. Göra om till methoder med bra Namn som förklarar vad som händer.
-- Gör om SQl-query till JSON och skickar i mailen för discussion, comment och post.
-Göra om allt till samma standard. Namnsättning framförallt.
--Göra om i Myfriendscontrollern rad 82-88 till metod
--Behöver gå ut ett mail till mina vänner att vännen tas bort



7.    TESTA
-Testa discussion	med flera och olika vänner.
--Create, Edit, Delete, (FRONTEND, Read)
-Testa post			-----.------
--Create, Edit, Delete, (FRONTEND, Read)
-Testa vänner funktioner. 
---(Lägga till med FRONTEND)
---Ta bort vänner.
---Gömma sig i nätverken. (bli osynlig för deras nätverk)


--Nya funktioner

8.    Föreslå nya funktioner 
-Chatt


9.    Fixa fel och införa ändringar





Frontend
Skicka friendrequest med mail.

///////////////////////////////////////////////////////////
EXTRA-JOBB:


///////////////////////////////////////////////////////////
PROBLEM:
Skickar ett extra friendacceptmail tillbaka efter båda blivit vänn. Behöver inte vara.

///////////////////////////////////////////////////////////
BEHÖVS TESTAS
FriendRequest. -hänger alla id:n med på korrekt sätt? Discussion, posts osv..

///////////////////////////////////////////////////////////
KLART:
# Lastupdated ändrar inte tid efter att ett mail har läst in*
# Kombinera flera Mailsender-funktioner*
# Skicka databasen åt motsatt håll.
# Vänner som inte är true skickas över till myfriendsfriends.
# deleteFriend tar bort vännen, diskussioner, poster & vänsVänner. Åt båda hållet.

///////////////////////////////////////////////////////////
FRÅGOR:
*Scriptet är uppdaterat. Men finns både en scripts-klass samt Global-script? Används båda
*myFriends ersätter users-tabellen
*Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag? Sync-vänner-knapp i efterhand


TILL STIG:
* Vad ska man kunna se?
	# När man blir vänner, ska man kunna se alla varandras diskussioner, inlägg och kommentarer? Men även inlägg & kommentarer från andra, som inte är ens vänner? 
* Om jag blir vän med Sam, då får jag hans vänner, men om han sen lägger till en vän efteråt, den vännen får inte jag? Sync-vänner-knapp i efterhand


warn: Microsoft.EntityFrameworkCore.Query[10103]
      The query uses the 'First'/'FirstOrDefault' operator without 'OrderBy' and filter operators. This may lead to unpredictable results.