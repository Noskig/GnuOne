import React, { useState, useContext,useEffect } from 'react';
import { Link, Switch, useRouteMatch } from 'react-router-dom';
import PortContext from '../../contexts/portContext';


//testar
function Notifications() {


    const port = useContext(PortContext);
    const url = `https://localhost:${port}/api/notifications`;
    const [allNotifications, setAllNotifications] = useState([]);

    useEffect(() => {
        fetchData();
    }, [])

    async function fetchData() {
        const response = await fetch(url);
        const notifications = await response.json();
        setAllNotifications(notifications);
    }



    return (
        <section className="notification-section">
            {allNotifications.map(newEvents => 
                newEvents.Type == "FriendRequestRecieved" ?
                    <div>
                        <h1>you have a new friend request from {newEvents.userName}</h1>
                    </div>
                    :
                    newEvents.Type == "FriendRequestAccepted" ?
                        <div>
                            <h1>{newEvents.userName} has accepted your friend request</h1>
                        </div>
                        :
                        newEvents.Type == "discussion" ?
                            <div>
                                <h1>you have {newEvents.Counter} new post on {newEvents.Headline}</h1>
                            </div>
                            :
                            newEvents.Type == "post" ?
                                <div>
                                    <h1>you have {newEvents.Counter} new comments on your post {newEvents.Headline}</h1>
                                </div>
                                : <h1>no new notifications</h1>
            )}
        </section>
    )
}

export default Notifications;