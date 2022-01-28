using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welcome_Settings
{
    public static class Global
    {
        public static string ConnectionString;
        public static string CompleteConnectionString;

        public static string MyPrivatekey = @"-----BEGIN RSA PRIVATE KEY-----
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
-----END RSA PRIVATE KEY-----

";


        public static string MyPublickey = @"-----BEGIN PUBLIC KEY-----
MIIBojANBgkqhkiG9w0BAQEFAAOCAY8AMIIBigKCAYEA9k9W+PKIcWEvYshh2rJQ
OxJ4x3k+59+slAj/9840FBeWCM6hADy01tqWWAVatfUhR5Tx4vmCANhDGsRz9nq7
16XmMvMvXLL7fXS9ThAoIWCYYpcSnHwz0YYKRjMHf0tlgMKwP1ory+9/kl9EvJQL
8sk2rLg8OzPoI6OtoXBUIDr3mnMd1EH1UuAbwgZJs8X26kVwnOfj/OQVqd4BzTcj
00CCHYQWSq8hcXCEZ6b617+b3u7T2samjvPzgAiG4knMEF5pw93h0YDR0QGfityq
jLZ6pC0aKiVqmWsjk47aO/Q3j7mrS2zXdr/S9j6q1SsZyNde/9JzDV79Ooz2OEJO
yn/iJ61qXycb0FI3hMNB+Ru0zHtRaZ/rQbYBwDo7c3rnS99vJlsF1PtBmcGTct6T
BDW4HhyTrJ9kIR+sbSzTcWgcAOpk4qGlA/W6afd/huXqleJGL3cPw0Q8plIABSIt
z5eJvz2H8H0tuq4MLTvEr3I5dQtW8KEJtKs3LJ2JJ+T1AgMBAAE=
-----END PUBLIC KEY-----

";

        public static string ericPublicKey = @"-----BEGIN PUBLIC KEY-----
MIIBojANBgkqhkiG9w0BAQEFAAOCAY8AMIIBigKCAYEA2cr3RxOmTGrGhclRQMF+
zel1LJy2sEM1BUzoAne78hX4gfyobknxnSbw9ojjO8l2pdPSEXv3UOiCe/i/Z53/
JUNXUcvxpnsZB1iP3oH0+/2sx1tkJSfgfLK+4ILJY+ZQ2lzAOeZb0fJxkz7MUXde
TMe3nB6uVly6nEY2Je5TfqcgmtU4HFKW9ZU7Be2gzo8Q/zI4pU0pTUbK4qWofdr9
9L8onJVf/EhLdWlwFVAiY7moxQymyILP9lLWIhAlrpz2NqaiwKpphUqaBBd0tJCs
iqcXHxquiZLAGKgKyz53xQR+z4VdhxLOFHRdfwvVsbDDGl8n9TvydhB5kj6BMjM1
DmSIEgvCYKLjl+sZ4ht6TWFATQqu71sEN+V1Y5JBsNgKvr4MzctOE31U6d72ajwP
jN9Vbl5aLDGsmquWIHT3LcwKTk1aCr2+1KLZ8Esci9JPwLf02iIG0E/KsudL/wNK
ERyoP9fmeRf6VUaVyxwj+tSAWjHCK5ZOEm5KZCdOgZqDAgMBAAE=
-----END PUBLIC KEY-----
";


		public static string ericPrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
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
-----END RSA PRIVATE KEY-----
";


		public static string sql = @"-- --------------------------------------------------------
-- Värd:                         127.0.0.1
-- Serverversion:                10.6.5-MariaDB - mariadb.org binary distribution
-- Server-OS:                    Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumpar databasstruktur för gnu
CREATE DATABASE IF NOT EXISTS `gnu` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `gnu`;

-- Dumpar struktur för tabell gnu.bookmarks
CREATE TABLE IF NOT EXISTS `bookmarks` (
  `ID` int(11) NOT NULL,
  `Email` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.bookmarks: ~0 rows (ungefär)
/*!40000 ALTER TABLE `bookmarks` DISABLE KEYS */;
/*!40000 ALTER TABLE `bookmarks` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.comments
CREATE TABLE IF NOT EXISTS `comments` (
  `ID` int(11) NOT NULL,
  `Email` varchar(50) NOT NULL DEFAULT '',
  `userName` varchar(50) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `commentText` varchar(100) NOT NULL,
  `postID` int(11) NOT NULL,
  `postEmail` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`,`Email`),
  KEY `FK_comments_posts` (`postID`,`postEmail`),
  CONSTRAINT `FK_comments_posts` FOREIGN KEY (`postID`, `postEmail`) REFERENCES `posts` (`ID`, `Email`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.comments: ~4 rows (ungefär)
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.discussions
CREATE TABLE IF NOT EXISTS `discussions` (
  `ID` int(11) NOT NULL,
  `Email` varchar(50) NOT NULL DEFAULT '',
  `Headline` varchar(50) DEFAULT NULL,
  `discussionText` varchar(500) DEFAULT NULL,
  `userName` varchar(50) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `tagOne` int(11) DEFAULT NULL,
  `tagTwo` int(11) DEFAULT NULL,
  `tagThree` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`,`Email`),
  KEY `FK_discussions_tags` (`tagOne`),
  KEY `FK_discussions_tags_2` (`tagTwo`),
  KEY `FK_discussions_tags_3` (`tagThree`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.discussions: ~3 rows (ungefär)
/*!40000 ALTER TABLE `discussions` DISABLE KEYS */;
/*!40000 ALTER TABLE `discussions` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.messages
CREATE TABLE IF NOT EXISTS `messages` (
  `ID` int(11) NOT NULL,
  `Sent` datetime DEFAULT NULL,
  `From` varchar(50) DEFAULT NULL,
  `To` varchar(50) DEFAULT NULL,
  `messageText` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.messages: ~0 rows (ungefär)
/*!40000 ALTER TABLE `messages` DISABLE KEYS */;
/*!40000 ALTER TABLE `messages` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.myfriends
CREATE TABLE IF NOT EXISTS `myfriends` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `userName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) NOT NULL,
  `IsFriend` bit(1) DEFAULT NULL,
  `pubKey` varchar(50) DEFAULT NULL,
  `userInfo` varchar(300) DEFAULT NULL,
  `pictureID` int(11) DEFAULT NULL,
  `tagOne` int(11) DEFAULT NULL,
  `tagTwo` int(11) DEFAULT NULL,
  `tagThree` int(11) DEFAULT NULL,
  `hideMe` bit(1) DEFAULT NULL,
  `hideFriend` bit(1) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=121 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.myfriends: ~2 rows (ungefär)
/*!40000 ALTER TABLE `myfriends` DISABLE KEYS */;
/*!40000 ALTER TABLE `myfriends` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.myfriendsfriends
CREATE TABLE IF NOT EXISTS `myfriendsfriends` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `myFriendEmail` varchar(50) NOT NULL,
  `userName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `pictureID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_myfriendsfriends_myfriends` (`myFriendEmail`),
  CONSTRAINT `FK_myfriendsfriends_myfriends` FOREIGN KEY (`myFriendEmail`) REFERENCES `myfriends` (`Email`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.myfriendsfriends: ~4 rows (ungefär)
/*!40000 ALTER TABLE `myfriendsfriends` DISABLE KEYS */;
/*!40000 ALTER TABLE `myfriendsfriends` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.myprofile
CREATE TABLE IF NOT EXISTS `myprofile` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Email` varchar(50) DEFAULT NULL,
  `myUserInfo` varchar(50) DEFAULT NULL,
  `pictureID` int(11) DEFAULT NULL,
  `tagOne` int(11) DEFAULT NULL,
  `tagTwo` int(11) DEFAULT NULL,
  `tagThree` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_myprofile_tags` (`tagOne`),
  KEY `FK_myprofile_tags_2` (`tagTwo`),
  KEY `FK_myprofile_tags_3` (`tagThree`),
  KEY `FK_myprofile_standardpictures` (`pictureID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.myprofile: ~1 rows (ungefär)
/*!40000 ALTER TABLE `myprofile` DISABLE KEYS */;
INSERT INTO `myprofile` (`ID`, `Email`, `myUserInfo`, `pictureID`, `tagOne`, `tagTwo`, `tagThree`) VALUES
	(1, 'albinscodetesting@gmail.com', 'Good vibes only', 2, 17, 7, 13);
/*!40000 ALTER TABLE `myprofile` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.mysettings
CREATE TABLE IF NOT EXISTS `mysettings` (
  `ID` int(11) DEFAULT NULL,
  `Email` varchar(75) DEFAULT NULL,
  `Password` varchar(75) DEFAULT NULL,
  `userName` varchar(75) DEFAULT NULL,
  `Secret` varchar(75) DEFAULT NULL,
  `DarkMode` bit(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.mysettings: ~1 rows (ungefär)
/*!40000 ALTER TABLE `mysettings` DISABLE KEYS */;
/*!40000 ALTER TABLE `mysettings` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.notifications
CREATE TABLE IF NOT EXISTS `notifications` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `hasBeenRead` bit(1) NOT NULL DEFAULT b'0',
  `messageType` varchar(50) NOT NULL DEFAULT '0',
  `info` varchar(50) DEFAULT '',
  `mail` varchar(50) NOT NULL DEFAULT '',
  `counter` int(11) NOT NULL DEFAULT 0,
  `infoID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.notifications: ~1 rows (ungefär)
/*!40000 ALTER TABLE `notifications` DISABLE KEYS */;
/*!40000 ALTER TABLE `notifications` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.posts
CREATE TABLE IF NOT EXISTS `posts` (
  `ID` int(11) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `userName` varchar(50) NOT NULL DEFAULT '',
  `postText` varchar(1000) NOT NULL DEFAULT '',
  `Date` datetime NOT NULL,
  `discussionID` int(11) NOT NULL,
  `discussionEmail` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`,`Email`),
  KEY `FK_posts_discussions` (`discussionID`,`discussionEmail`),
  CONSTRAINT `FK_posts_discussions` FOREIGN KEY (`discussionID`, `discussionEmail`) REFERENCES `discussions` (`ID`, `Email`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.posts: ~4 rows (ungefär)
/*!40000 ALTER TABLE `posts` DISABLE KEYS */;
/*!40000 ALTER TABLE `posts` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.standardpictures
CREATE TABLE IF NOT EXISTS `standardpictures` (
  `pictureID` int(11) NOT NULL AUTO_INCREMENT,
  `PictureName` varchar(50) DEFAULT NULL,
  `PictureSrc` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`pictureID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.standardpictures: ~5 rows (ungefär)
/*!40000 ALTER TABLE `standardpictures` DISABLE KEYS */;
INSERT INTO `standardpictures` (`pictureID`, `PictureName`, `PictureSrc`) VALUES
	(1, 'BeerGuy', '/image/BeerGuy.jpg'),
	(2, 'Flanders', '/image/Flanders.png'),
	(3, 'Nelson', '/image/Nelson.jpg'),
	(4, 'Ralph', '/image/Ralph.jpg'),
	(5, 'SideShow-Bob', '~/image/SideShow-Bob.jpg');
/*!40000 ALTER TABLE `standardpictures` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.tags
CREATE TABLE IF NOT EXISTS `tags` (
  `ID` int(11) NOT NULL,
  `tagName` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.tags: ~20 rows (ungefär)
/*!40000 ALTER TABLE `tags` DISABLE KEYS */;
INSERT INTO `tags` (`ID`, `tagName`) VALUES
	(1, 'Spel'),
	(2, 'Katter'),
	(3, 'Mat'),
	(4, 'Djur'),
	(5, 'Sport'),
	(6, 'Livstil'),
	(7, 'Datorer'),
	(8, 'Foto'),
	(9, 'Musik'),
	(10, 'Film'),
	(11, 'Böcker'),
	(12, 'Dejting'),
	(13, 'Resa'),
	(14, 'Väder'),
	(15, 'Kläder'),
	(16, 'Software'),
	(17, 'Utbildning'),
	(18, 'Porr'),
	(19, 'Fordon'),
	(20, 'Pengar');
/*!40000 ALTER TABLE `tags` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.users
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL,
  `userName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.users: ~9 rows (ungefär)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`ID`, `userName`, `Email`) VALUES
	(1, 'bober', 'bobertestar@gmail.com'),
	(2, 'Sam', 'mintestmail321@gmail.com'),
	(3, 'Albin', 'albinscodetesting@gmail.com'),
	(4, 'Love', 'developertestingcrash@gmail.com'),
	(5, 'Yos', 'mailconsolejonatan@gmail.com'),
	(6, 'Daniel', 'Danielkhoshtest@gmail.com'),
	(7, 'Boris', 'reezlatest@gmail.com'),
	(8, 'Johanna', 'johannastestmail@gmail.com'),
	(9, 'TheStig', 'thestigx937@gmail.com');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
";
    }
}
