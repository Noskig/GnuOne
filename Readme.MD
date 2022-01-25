Welcome to the Gnu Project!

Reading through the guided code will help you learn about the project and how to use it.

Visit

http://iwantag.nu/

Database structure

![Gnu Diagram](![Gnu Diagram updaterad](https://user-images.githubusercontent.com/72744280/150971152-f3cdd569-1adc-45b8-8ec2-bf6426dd3ae8.png))


Whats our story behind it?

It begins as an internproject to offer a centralize social platform that focus on being independent from third parties.
 Our customer request was to create a platform and not have information stored in the cloud by the big corperations.

Okay How to get started?
1. 
Create a Gmail-account 

Put "Access for less secure apps" to on.

https://myaccount.google.com/security?gar=1


2.Install Heidi

https://www.heidisql.com/


3.Run

![step1](https://user-images.githubusercontent.com/72744280/149352181-9e9c3c72-e083-40d6-acf0-bf36f5d871e0.JPG)

Login with the same username & password as Heidi

![step2](https://user-images.githubusercontent.com/72744280/149352266-787206cb-60c6-4e3d-badb-8586cded2498.JPG)

Check out the inbox for your Gmail, it crashes when it is not empty or if there are emails that are not related to the program.

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
<b>Post:</b> Bookmarks a discussion.<br>

<h3>Notifications</h3>
<h5>api/notifications</h5>
<b>Get:</b> Returns all notifications.<br>


