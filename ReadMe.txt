Christopher.
///////////////////////////////////////////////////////////
ATT GÖRA: (utan inbördes ordning)

1.   Jag vill kunna hämta ner och installera programmet och köra det ifrån en webbrowser. (bittorrent eller iwantag.nu?)
Skriva instruktion och testa att det går
(Få första vännen och komma in i systemet. FRONTEND ruta som man skriver i mailen på den som man vill bli vän med)
-Bjuda in ny användare - Ny sida som förklarar grejer.. Vad man behöver ta ner och att man skapar mail osv.
-Stegvis förklaring av installationsprocessen.
--Text
--Video

**Gå igenom källkoden, dokumentera, städa, fundera på standard.

**Dokumentera github. Text. Video. Göra en snygg förklaring. Guida Stig
-Få prototyp redo, sen dokumentera V.1
--vi har en liten start.

___Agenda__



** Generera nycklar https://www.c-sharpcorner.com/article/generating-publicprivate-keys-in-c-sharp-and-net/
- pubkey i profile
- private key någonstans. (antagligen i en fil.. kan börja med att ha den i DBn)

//generera matchande nycklar. 
Spara en lokalt, settings Secret. Den publika ska följa med till mina vänner på myfriends där.



_____A_____

Frågor Stig
-Email display på sin egen profil? på andras?-
-Varför vill vi visa Pub nyckeln i profilen?

-------------
/////// Allmänt fixing


*Uppdater picture location i MariaDB

*DM
	-Krypteras?
	-Notification?

*Backup
- För när man byter dator. (vi behöver få över den filen till den nya datorn)

///////////////////////////////////////////////////////////
EXTRA-JOBB:
** vi har marcus som extra resurs

*bookmarks borde tas bort när posten eller discussion deletas

** deletea konto
- Foreach tabort vänner. 
- Script för att ta bort databasen

* Kunna blocka någon?
* Try catch block på all sparning i controllers?

* Göra om allt till samma standard. Namnsättning framförallt.(på börjad kanske klar)

* APIsync för att synca discussion, post, vänner och vännersvänners ifall att nått skulle bli fel.

( -API som ändrar inställningar. Vill man kunna ändra gmail?. Säkra upp).

///////////////////////////////////////////////////////////
PROBLEM:


///////////////////////////////////////////////////////////
BEHÖVS TESTAS

** Dubbel kolla alla foreach mailutskick loopar att de inte skickar till vänner man inte är vän med "(if (user.isFriend == false) { continue; })"

///////////////////////////////////////////////////////////
KLART:


17/1
#setting. Darkmode, 
#Commentscontroller, 
#användarnamn. Ändra så att det hänger med hela vägen.

18/1
# Gör nya cases för comments (postedcomment etc) - Testat enkelt
# Bookmarking - En grund (kanske klar)

19/1
# Notification på första sidan
# - Vänförfrågan
  --Fått vänförfrågan
  --Accepterad
  -På sina egna disc, posts.   
  --Mina disc, någon har postat
  --Mina Posts. Någon har kommenterat
  --Delete all
  --Delete Single
  -Räknare

20/1
  *Notification
  *Bookmarks

24/1
#DM
	-Från, meddelande, till, tid.
	-Sorteras på sin vän
	-Sparas i maria.
	-tas bort om man tar bort vännen


# Gör om SQl-query till JSON och skickar i mailen för discussion, comment och post. 5/1
# Göra om i Myfriendscontrollern rad 82-88 till metod   Behöver gå ut ett mail till mina vänner att vännen tas bort 5/1
# Fixa scriptet så det matchar den nya DB 7/1
# Rätt information ska skickas ut när man blir vän också

# Posts som tas bort ska ligga kvar men bli deletat.
# Filtrera på tags
# userInfo. Intressen(bool).
# Gömma sig i nätverken hos vänner.
# Jag vill ha en sida där jag kan se vilka inställningar som är gjorda och eventuellt ändra valda parametrar.
# Jag vill kunna lägga till vänner.
  -(FRONTEND)Skapa en process för detta när man har kommit in i systemet.
  -(FRONTEND notiser, knapp för accept/denied)

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
# Darkmode. 
# CommentsControler
# fetch post, comments hänger med
# Ta marcus -> vårt program.


///////////////////////////////////////////////////////////


///kryptering
alla användare har en private och publik key.
mail krypteras med mottagarens public key.

hur genereras nycklarna?
var lägger man nycklarna?
nycklarna. generea public. Gömma private.

----------------------------------------------------------
semi gammalt

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

---

GnuOne

Welcome to the Gnu Project!

Reading through the guided code will help you learn about the project and how to use it.

Visit http://iwantag.nu/

Story behind this project

It begins as an internproject to offer a different social platform that focus on being independent of third parties.

Okay so where do I get started?
1. 
Create a Gmail-account 
Put "Access for less secure apps" to on.
https://myaccount.google.com/security?gar=1
2.
Install Heidi
https://www.heidisql.com/
3.
Run 
 
Login with the same username & password as Heidi
 
