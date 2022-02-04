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
    const [readNotifications, setReadNotifications] = useState();
    const [unreadNotifications, setUnreadNotifications] = useState();

    useEffect(() => {
        fetchData();
    }, [])

    async function fetchData() {
        const response = await fetch(url);
        const notifications = await response.json();
        console.log(notifications)
        let read = notifications.filter(notification => notification.hasBeenRead === true)
        let unread = notifications.filter(notification => notification.hasBeenRead === false)
        setReadNotifications(read)
        setUnreadNotifications(unread);
    }




    function setId(newEvents) {
        deleteNotification(newEvents)
    }

    async function deleteNotification(notificationId) {
        if (typeof(notificationId) === "object") {
            notificationId.forEach(async (id) => {
                await fetch(`https://localhost:${port}/api/notifications/${id.ID}`, {
                    method: 'DELETE',
                    headers: {
                        "Content-type": "application/json; charset=UTF-8",
                    }
                })
            })
        } else {
            await fetch(`https://localhost:${port}/api/notifications/${notificationId}`, {
                method: 'DELETE',
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                }
            })
        }


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
            <h3>Unread notifications</h3>
            {unreadNotifications?.map(newEvents =>
                newEvents.Type === "FriendRequestRecieved" ?
                    <div key={newEvents.ID} className="unread notification">
                        <Link onClick={() => markAsRead(newEvents.ID)} to={'profile/friends'}>
                            <p>You have a new friend request from {newEvents.userName}</p>
                        </Link>
                        <button className="delete-notification" onClick={() => setId(newEvents.ID)}>Delete notification</button>
                    </div>
                    :
                    newEvents.Type === "FriendRequestAccepted" ?
                        <div key={newEvents.ID} className="unread notification">
                            <Link onClick={() => markAsRead(newEvents.ID)} to={'profile/friends'}>
                                <p>{newEvents.userName} has accepted your friend request</p>
                            </Link>
                            <button className="delete-notification" onClick={() => setId(newEvents.ID)}>Delete notification</button>
                        </div>
                        :
                        newEvents.Type === "discussion" ?
                            <div key={newEvents.ID} className="unread notification" >
                                <Link onClick={() => markAsRead(newEvents.ID)} to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.infoID}`}>
                                    <p>You have {newEvents.Counter} new post(s) on "{newEvents.Headline}"</p>
                                </Link>
                                <button className="delete-notification" onClick={() => setId(newEvents.ID)}>Delete notification</button>
                            </div>
                            :
                            newEvents.Type === "post" ?
                                <div key={newEvents.ID} className="unread notification">
                                    <Link onClick={() => markAsRead(newEvents.ID)} to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}`}>
                                        <p>You have {newEvents.Counter} new comment(s) on your post "{newEvents.Headline}"</p>
                                    </Link>
                                    <button className="delete-notification" onClick={() => setId(newEvents.ID)}>Delete notification</button>
                                </div>
                                : <h1>no new notifications</h1>
            )}
            <div className="notifications-header">
                <h3>Read notifications</h3>
                <h4 onClick={() => deleteNotification(readNotifications)}>Delete all read notifications</h4>
            </div>
            {readNotifications?.map(newEvents =>
                newEvents.Type === "FriendRequestRecieved" ?
                    <div key={newEvents.ID} className="read notification">
                        <Link to={'profile/friends'}>
                            <p>you have a new friend request from {newEvents.userName}</p>
                        </Link>
                        <button className="delete-notification" onClick={() => setId(newEvents.ID)}>Delete notification</button>
                    </div>
                    :
                    newEvents.Type === "FriendRequestAccepted" ?
                        <div key={newEvents.ID} className="read notification">
                            <Link to={'profile/friends'}>
                                <p>{newEvents.userName} has accepted your friend request</p>
                            </Link>
                            <button className="delete-notification" onClick={() => setId(newEvents.ID)}>Delete notification</button>
                        </div>
                        :
                        newEvents.Type === "discussion" ?
                            <div key={newEvents.ID} className="read notification" >
                                <Link to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.infoID}`}>
                                    <p>You have {newEvents.Counter} new posts on "{newEvents.Headline}"</p>
                                </Link>
                                <button className="delete-notification" onClick={() => setId(newEvents.ID)}>Delete notification</button>
                            </div>
                            :
                            newEvents.Type === "post" ?
                                <div key={newEvents.ID} className="read notification">
                                    <Link onClick={() => markAsRead(newEvents.ID)} to={myEmail === newEvents.Email ? `profile/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}` : `friendprofile/${newEvents.Email.substring(0, newEvents.Email.lastIndexOf("@"))}/discussions/${newEvents.DiscussionID}/post/${newEvents.infoID}`}>
                                        <p>You have {newEvents.Counter} new comments on your post "{newEvents.Headline}"</p>
                                    </Link>
                                    <button onClick={() => setId(newEvents.ID)}>Delete</button>
                                </div>
                                : <h1>no new notifications</h1>
            )}
        </section>
    )
}

export default Notifications;