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

		public static string sql = @"-- --------------------------------------------------------
-- Värd:                         127.0.0.1
-- Serverversion:                8.0.27 - MySQL Community Server - GPL
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
CREATE DATABASE IF NOT EXISTS `gnu` /*!40100 DEFAULT CHARACTER SET latin1 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `gnu`;

-- Dumpar struktur för tabell gnu.comments
CREATE TABLE IF NOT EXISTS `comments` (
  `ID` int NOT NULL,
  `Email` varchar(50) NOT NULL DEFAULT '',
  `userName` varchar(50) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `commentText` varchar(100) NOT NULL,
  `postID` int NOT NULL,
  `postEmail` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`,`Email`),
  KEY `FK_comments_posts` (`postID`,`postEmail`),
  CONSTRAINT `FK_comments_posts` FOREIGN KEY (`postID`, `postEmail`) REFERENCES `posts` (`ID`, `Email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.comments: ~0 rows (ungefär)
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.discussions
CREATE TABLE IF NOT EXISTS `discussions` (
  `ID` int NOT NULL,
  `Email` varchar(50) NOT NULL DEFAULT '',
  `Headline` varchar(50) DEFAULT NULL,
  `discussionText` varchar(500) DEFAULT NULL,
  `userName` varchar(50) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `tagOne` int DEFAULT NULL,
  `tagTwo` int DEFAULT NULL,
  `tagThree` int DEFAULT NULL,
  PRIMARY KEY (`ID`,`Email`),
  KEY `FK_discussions_tags` (`tagOne`),
  KEY `FK_discussions_tags_2` (`tagTwo`),
  KEY `FK_discussions_tags_3` (`tagThree`),
  CONSTRAINT `FK_discussions_tags` FOREIGN KEY (`tagOne`) REFERENCES `tags` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_discussions_tags_2` FOREIGN KEY (`tagTwo`) REFERENCES `tags` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_discussions_tags_3` FOREIGN KEY (`tagThree`) REFERENCES `tags` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.discussions: ~0 rows (ungefär)
/*!40000 ALTER TABLE `discussions` DISABLE KEYS */;
/*!40000 ALTER TABLE `discussions` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.myfriends
CREATE TABLE IF NOT EXISTS `myfriends` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `userName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) NOT NULL,
  `IsFriend` bit(1) DEFAULT NULL,
  `pubKey` varchar(50) DEFAULT NULL,
  `userInfo` varchar(300) DEFAULT NULL,
  `pictureID` int DEFAULT NULL,
  `tagOne` int DEFAULT NULL,
  `tagTwo` int DEFAULT NULL,
  `tagThree` int DEFAULT NULL,
  `hideMe` bit(1) DEFAULT NULL,
  `hideFriend` bit(1) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.myfriends: ~0 rows (ungefär)
/*!40000 ALTER TABLE `myfriends` DISABLE KEYS */;
/*!40000 ALTER TABLE `myfriends` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.myfriendsfriends
CREATE TABLE IF NOT EXISTS `myfriendsfriends` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `myFriendEmail` varchar(50) NOT NULL,
  `userName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_myfriendsfriends_myfriends` (`myFriendEmail`),
  CONSTRAINT `FK_myfriendsfriends_myfriends` FOREIGN KEY (`myFriendEmail`) REFERENCES `myfriends` (`Email`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.myfriendsfriends: ~0 rows (ungefär)
/*!40000 ALTER TABLE `myfriendsfriends` DISABLE KEYS */;
/*!40000 ALTER TABLE `myfriendsfriends` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.myprofile
CREATE TABLE IF NOT EXISTS `myprofile` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(50) DEFAULT NULL,
  `myUserInfo` varchar(50) DEFAULT NULL,
  `pictureID` int DEFAULT NULL,
  `tagOne` int DEFAULT NULL,
  `tagTwo` int DEFAULT NULL,
  `tagThree` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_myprofile_tags` (`tagOne`),
  KEY `FK_myprofile_tags_2` (`tagTwo`),
  KEY `FK_myprofile_tags_3` (`tagThree`),
  KEY `FK_myprofile_standardpictures` (`pictureID`),
  CONSTRAINT `FK_myprofile_standardpictures` FOREIGN KEY (`pictureID`) REFERENCES `standardpictures` (`pictureID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_myprofile_tags` FOREIGN KEY (`tagOne`) REFERENCES `tags` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_myprofile_tags_2` FOREIGN KEY (`tagTwo`) REFERENCES `tags` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_myprofile_tags_3` FOREIGN KEY (`tagThree`) REFERENCES `tags` (`ID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.myprofile: ~0 rows (ungefär)
/*!40000 ALTER TABLE `myprofile` DISABLE KEYS */;
/*!40000 ALTER TABLE `myprofile` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.mysettings
CREATE TABLE IF NOT EXISTS `mysettings` (
  `ID` int DEFAULT NULL,
  `Email` varchar(75) DEFAULT NULL,
  `Password` varchar(75) DEFAULT NULL,
  `userName` varchar(75) DEFAULT NULL,
  `Secret` varchar(75) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.mysettings: ~0 rows (ungefär)
/*!40000 ALTER TABLE `mysettings` DISABLE KEYS */;
/*!40000 ALTER TABLE `mysettings` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.posts
CREATE TABLE IF NOT EXISTS `posts` (
  `ID` int NOT NULL,
  `Email` varchar(50) NOT NULL,
  `userName` varchar(50) NOT NULL DEFAULT '',
  `postText` varchar(1000) NOT NULL DEFAULT '',
  `Date` datetime NOT NULL,
  `discussionID` int NOT NULL,
  `discussionEmail` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`,`Email`),
  KEY `FK_posts_discussions` (`discussionID`,`discussionEmail`),
  CONSTRAINT `FK_posts_discussions` FOREIGN KEY (`discussionID`, `discussionEmail`) REFERENCES `discussions` (`ID`, `Email`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpar data för tabell gnu.posts: ~0 rows (ungefär)
/*!40000 ALTER TABLE `posts` DISABLE KEYS */;
/*!40000 ALTER TABLE `posts` ENABLE KEYS */;

-- Dumpar struktur för tabell gnu.standardpictures
CREATE TABLE IF NOT EXISTS `standardpictures` (
  `pictureID` int NOT NULL AUTO_INCREMENT,
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
  `ID` int NOT NULL,
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
  `ID` int NOT NULL,
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
