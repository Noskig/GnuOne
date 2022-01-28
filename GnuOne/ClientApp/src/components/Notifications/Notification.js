import React, { useState, useContext, useEffect } from 'react';
import { Link, Switch, useRouteMatch } from 'react-router-dom';
import PortContext from '../../contexts/portContext';
import MeContext from '../../contexts/meContext'
import './notifications.css'

//testar
function Notifications() {
    const myEmail = useContext(MeContext);
    const port = useContext(PortContext);
    const url = `https://localhost:${port}/api/notifications`;
    const [allNotifications, setAllNotifications] = useState();

    useEffect(() => {
        fetchData();
    }, [])

    async function fetchData() {
        const response = await fetch(url);
        const notifications = await response.json();
        console.log(notifications)
  
        setAllNotifications(notifications);
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

    async function markAsRead(notificationId) {
        await fetch(`https://localhost:${port}/api/notifications/${notificationId}`, {
            method: 'PUT',
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
      
    }

    return (
        <section className="notification-section">
            {allNotifications?.map(newEvents =>
                newEvents.Type === "FriendRequestRecieved" ?
                    <div key={newEvents.ID} className={newEvents.hasBeenRead ? "read" : "unread"}>
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
                            <div key={newEvents.ID} className={newEvents.hasBeenRead ? "read" : "unread"} >
                                <Link to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.infoID}`}>
                                    <p>you have {newEvents.Counter} new post on "{newEvents.Headline}"</p>
                                </Link>
                                    <button onClick={() => setId(newEvents.ID)}>Delete</button>
                            </div>
                            :
                            newEvents.Type === "post" ?
                                <div key={newEvents.ID} className={newEvents.hasBeenRead ? "read" : "unread"}>
                                    <Link onClick={()=>markAsRead(newEvents.ID)}to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}`}>
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