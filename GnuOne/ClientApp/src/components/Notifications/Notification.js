import React, { useState, useContext, useEffect } from 'react';
import { Link, Switch, useRouteMatch } from 'react-router-dom';
import PortContext from '../../contexts/portContext';
import MeContext from '../../contexts/meContext'


//testar
function Notifications() {

    const myEmail = useContext(MeContext);
    const port = useContext(PortContext);
    const url = `https://localhost:${port}/api/notifications`;
    const [allNotifications, setAllNotifications] = useState();

    /*const [notifications, setNotifications] = useState()*/

    //const blabla = useCallback(async () => {
    //    console.log(allNotifications)
    //    let posts = allNotifications?.filter(notification => notification.Type === "post")
    //    let discussions = allNotifications?.filter(notification => notification.Type === "discussion")
    //    let theRest = allNotifications?.filter(notification => notification.Type !== "discussion" && notification.Type !== "post")
    //    let newList = [];
    //     posts.forEach(async (post) => {
    //        const responseOne = await fetch(`https://localhost:${port}/api/posts/${post.infoID}`);
    //        const actualPost = await responseOne.json();
    //        post.discussionEmail = actualPost.discussionEmail.substring(0, actualPost.discussionEmail.lastIndexOf("@"));

    //    })
    //     discussions.forEach(async (discussion) => {
    //        const responseTwo = await fetch(`https://localhost:${port}/api/discussions/${discussion.infoID}`);
    //        const actualDiscussion = await responseTwo.json();
    //        discussion.discussionEmail = actualDiscussion.email.substring(0, actualDiscussion.email.lastIndexOf("@"));

    //    })

    //    await newList.push(...posts, ...discussions, ...theRest)
    //    console.log(newList)
    //    setNotifications(newList)
    //}, [allNotifications, port])

    useEffect(() => {
        fetchData();
    }, [])

    async function fetchData() {
        const response = await fetch(url);
        const notifications = await response.json();
        console.log(notifications)

     
       // //Skapar en ny lista "posts" genom att filtrera fram alla notifications av type "post"
       // let posts = notifications.filter(notification => notification.Type ===  "post")
       // console.log(posts)

       // let discussions = notifications.filter(notification => notification.Type === "discussion")

       // //Lägger på "discussionEmail" för varje post i listan "posts"
       //await posts.forEach(async (post) => {
       //     const responseOne = await fetch(`https://localhost:${port}/api/posts/${post.infoID}`);
       //     const actualPost = await responseOne.json();
       //     post.discussionEmail = actualPost.discussionEmail.substring(0, actualPost.discussionEmail.lastIndexOf("@"));
       // })

        //await discussions.forEach(async (discussion) => {
        //    const responseTwo = await fetch(`https://localhost:${port}/api/discussions/${discussion.infoID}`);
        //    const actualDiscussion = await responseTwo.json();
        //    discussion.discussionEmail = actualDiscussion.email.substring(0, actualDiscussion.email.lastIndexOf("@"));
        //})
  
        setAllNotifications(notifications);
        console.log(notifications)
    }

    


    function setId(newEvents) {
        deleteNotification(newEvents)
    }

    async function deleteNotification(notificationId) {
        await fetch(`https://localhost:${port}/api/notifications/${notificationId}`, {
            method: 'DELETE',
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
    }

    return (
        <section className="notification-section">
            {allNotifications?.map(newEvents =>
                newEvents.Type === "FriendRequestRecieved" ?
                    <div key={newEvents.ID}>
                        <p>you have a new friend request from {newEvents.userName}</p>
                        <button onClick={() => setId(newEvents.ID)}>Delete</button>
                    </div>
                    :
                    newEvents.Type === "FriendRequestAccepted" ?
                        <div key={newEvents.ID}>
                            <p>{newEvents.userName} has accepted your friend request</p>
                            <button onClick={() => setId(newEvents.ID)}>Delete</button>
                        </div>
                        :
                        newEvents.Type === "discussion" ?
                            <div key={newEvents.ID}>
                                <Link to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.infoID}`}>
                                    <p>you have {newEvents.Counter} new post on "{newEvents.Headline}"</p>
                                </Link>
                                    <button onClick={() => setId(newEvents.ID)}>Delete</button>
                            </div>
                            :
                            newEvents.Type === "post" ?
                                <div key={newEvents.ID}>
                                    <Link to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}`}>
                                        <p>you have {newEvents.Counter} new comments on your post "{newEvents.Headline}"</p>
                                    </Link>
                                        <button onClick={() => setId(newEvents.ID)}>Delete</button>
                                </div>
                                : <h1>no new notifications</h1>
            )}
        </section>
    )
}

export default Notifications;