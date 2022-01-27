# Gnu - A Decentrilized Forum

This project was created for a start-up and our customer who wanted a proof of concept for a decentrilzed forum. 


> Demo [_here_](https://www.youtube.com/)

Visit: http://iwantag.nu/ to read more.

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Screenshots](#screenshots)
* [Setup](#setup)
* [Usage](#usage)
* [Project Status](#project-status)
* [Room for Improvement](#room-for-improvement)
* [Acknowledgements](#acknowledgements)
* [Contact](#contact)
<!-- * [License](#license) -->

## General Information
<!-- *- Provide general information about your project here.-->
- It begans as an group-project to offer a decentralize social platform. 
- This is a the Beta version that is have a solid foundation but is still not fully developed.

- What problem does it (intend to) solve?
-  To be independent from third parties.
-  To keep information stored locally.
-  To be decentrilzed and private.
-  Not having any ads

- What is the purpose of your project?
-  To create an interactive application which does not provide a algorithmic feed for you.
-  <b>To learn</b> different technologies, to co-operate in a medium-big team. To get better att problem-solvning. To connect back- to front -end. To see how iterative development can work.  

## Technologies Used
- MariaDB - version 10.7.1
- HeideSQL - version 11.3
- React - version xx
- Node.js - version xx
- .Net - version 6
- EntityFrameworkCore - version xx
- Impt-client - version xx
- Postman - version 9
- 

## Features
List the ready features here:
- Starting forum threads (We call them Discussions)
- Responding to Discussion. (We call them Posts)
- Responding to Posts. (We call them Comments)
- Adding friends.
- Direct messages to friends.
- Bookmarking of interesting Discussions/Posts
- Notifications
- Interactive usage. <p>App based on discovery among your friends by clicking. Instead of a feed.</p>
- Tag a interest on a Discussions and on your Profile.
- 
- Awesome feature 2
- Awesome feature 3

## Screenshots
<b>Database structure</b>

![Gnu Diagram - Diagram updated 27 jan](https://user-images.githubusercontent.com/72744280/151323920-75f7a9fa-7cc2-4c3f-b7a0-c055b83ae942.png)


<b>Early state </b>

![consolemessage](https://user-images.githubusercontent.com/72744280/151310340-df5b1005-0353-4298-ba64-2bf46f1e1929.jpg)
This is when the project was from the beginning.

<!-- Api Endpoint Bild -->
<b>Api Endpoint overview </b>
![Gnu Diagram - Endpoint updated 27 jan](https://user-images.githubusercontent.com/72744280/151323669-eeb9f50c-cc2f-4e82-b7cf-dce843105517.png)



<!-- Inloggningen -->

<!-- 3-4 mindre bilder på sidorna -->

<b>Discuss</b>

![owndiscuss](https://user-images.githubusercontent.com/72744280/151330235-95ec33fb-9ca8-4cb1-b729-758d27316925.JPG)


<b>Friends view
  
![friendsview](https://user-images.githubusercontent.com/72744280/151329279-204995c7-e9d4-497b-bb6c-6ef96de61b0c.JPG)



# Setup
What are the project requirements/dependencies? Where are they listed? A requirements.txt or a Pipfile.lock file perhaps? Where is it located?

1. Create a Gmail-account 

Put "Access for less secure apps" to on.

https://myaccount.google.com/security?gar=1

2.Install MariaDb

LINK

3.Install Heidi

https://www.heidisql.com/

4.Run

![step1](https://user-images.githubusercontent.com/72744280/149352181-9e9c3c72-e083-40d6-acf0-bf36f5d871e0.JPG)

Login with the same username & password as Heidi

![step2](https://user-images.githubusercontent.com/72744280/149352266-787206cb-60c6-4e3d-badb-8586cded2498.JPG)

Check out the inbox for your Gmail, it crashes when it is not empty or if there are emails that are not related to the program.
--------------
## Usage
How does one go about using it?
Provide various use cases and code examples here.

`write-your-code-here`
------------------------
# Project Status
Project is: _in progress_ / _complete_ / _no longer being worked on_. If you are no longer working on it, provide reasons why.


## Room for Improvement
Include areas you believe need improvement / could be improved. Also add TODOs for future development.

Room for improvement:
- Mobile/tablet-app
- Improvement to be done 2

To do:
- The ability to share & upload files between the friends/user.
- Use a different mail-account than Gmail
- Run the project without HeidiSQL



## Acknowledgements
Give credit here.
- This project was inspired by bubbles


## Contact
Created by the GNU squad (http://iwantag.nu/) 


-------------------------
<h1>Endpoints</h1>

<h3>Comments</h3>
<h5>api/comments</h5>
<b>Get:</b> Returns all comments.<br>
<b>Post:</b> Posts a comment and sends it to all relevant users.<br>

<h5>api/comments/{id}</h5>
<b>Get:</b> Returns a specific comment through an Id.<br>
<b>Put:</b> Edits a specific comment.<br>
<b>Delete:</b> Deletes a specific comment through an Id.<br>



<h3>Discussions</h3>
<h5>/api/discussions</h5>
<b>Get:</b> Returns all discussions.<br>
<b>Post:</b> Posts new discussion.<br>

<h5>/api/discussions/{id}</h5>
<b>Get:</b> Returns a specific discussion.<br>
<b>Put:</b> Edits a discussion through an Id<br>
<b>Delete:</b> Deletes a discussion through an Id.<br>



<h3>MyFriends</h3>
<h5>/api/myFriends</h5>
<b>Get:</b> Returns all the users friends in JSON format.<br>
<b>Get:</b> Takes an email as a parameter and returns the friend and his friends.<br>
<b>Post:</b> Takes an email as an input and sends a friend request.<br>
<b>Put:</b> This is used for replying to an incoming friend request. Send the mail you want to answer to and isFriend: true/false. If you accept the request your information and relevant data will be sent to your new friend who’ll in turn send back his relevant data to you.
In addition to that, your friend list will be updated so as to be seen by your other friends and the new friend will be added to their myFriendsFriends list.<br>
<b>Delete:</b> This is used to unfriend somebody. Each part will be deleted from each others friend list. Both of your friends lists of myFriendsFriends will be updated.

<h5>/api/myFriends/true</h5>
<b>Patch:</b> This is used as a setting for managing your visibility in a friends network. Pass in your friends email and true or false in the web address to determine if you want to be hidden in the friends network or not.<br>



<h3>MyProfile</h3>
<h5>api/myProfile</h5>
<b>Put:</b> Is used for editing your profile information.<br>

<h5>api/myProfile/{id}</h5>
<b>Get:</b> Returns a picture if you pass in its Id in the web address. This is currently used so that the frontend can render your profile picture after reading its Id from your profile.<br>



<h3>Posts</h3>
<h5>api/posts</h5>
<b>Get:</b> Gets all posts from the DB. Converts them to JSON format and returns it.<br>
<b>Post:</b> Send a post.<br>

<h5>api/posts/{id}</h5>
<b>Get:</b> Returns a specific post through an Id.<br>
<b>Put:</b> Edits a specific post through an Id.<br>
<b>Delete:</b> Deletes a specific post through an Id. The post will be deleted for everybody involved.<br>



<h3>Settings</h3>
<h5>api/settings</h5>
<b>Get:</b> Returns a mySettings object.<br>
<b>Put:</b> Under construction. Could be updated so that you can edit your mySettings data.<br>
<b>Put:</b> Under construction. If you only want to change your username, pass in a string with your new username.<br>
<b>Delete:</b> Under construction. If you want to delete your account.<br>



<h3>Tags</h3>
<h5>api/tags</h5>
<b>Get:</b> Returns all the tags.<br>
<b>Post:</b> Adds a new tag.<br>

<h5>api/tags/{id}</h5>
<b>Get:</b> Searches for discussions with a specific tagId and returns those objects.<br>

<h3>Messages</h3>
<h5>api/messages</h5>
<b>Get:</b> Returns all the messages.<br>
<b>Post:</b> To write a new message.<br>

<h3>Bookmarks</h3>
<h5>api/bookmarks</h5>
<b>Post:</b> Bookmarks a discussion.<br>

<h3>Notifications</h3>
<h5>api/notifications</h5>
<b>Get:</b> Returns all notifications.<br>


