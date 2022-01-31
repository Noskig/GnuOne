import { useState, useContext, useEffect, useCallback } from "react"
import { Link, useRouteMatch } from 'react-router-dom';
import PortContext from '../../contexts/portContext';
import MeContext from '../../contexts/meContext';
import FriendContext from '../../contexts/friendContext';
import PrivateMessages from "./PrivateMessages";
import "./messages.css";

const Messages = () => {
    const [messageList, setMessageList] = useState([])
    const { friendEmail } = useContext(FriendContext)
    const myEmail = useContext(MeContext)
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/messages/`
    let match = useRouteMatch()

    const getDuration = useCallback((seconds) => {

        let durationInSeconds = {
            epochs: ['year', 'month', 'day', 'hour', 'minute', 'second'],
            year: 31536000,
            month: 2592000,
            day: 86400,
            hour: 3600,
            minute: 60,
            second: 1
        };

        var epoch, interval;

        for (var i = 0; i < durationInSeconds.epochs.length; i++) {
            epoch = durationInSeconds.epochs[i];
            interval = Math.floor(seconds / durationInSeconds[epoch]);
            if (interval >= 1) {
                return {
                    interval: interval,
                    epoch: epoch
                };
            }
        }

    }, []);


    const timeSince = useCallback((date) => {
        console.log(date)
        var seconds = Math.floor((new Date() - new Date(date)) / 1000);
        console.log(seconds)
        var duration = getDuration(seconds);
        console.log(duration)
        var suffix = (duration.interval > 1 || duration.interval === 0) ? 's' : '';
        return duration.interval + ' ' + duration.epoch + suffix + ' ago';
    }, [getDuration]);

    useEffect(() => {
        async function fetchData() {
            console.log(friendEmail, myEmail)
            if (friendEmail === undefined) {
                //if on my own profile, get all my messages
                const response = await fetch(url)
                const messages = await response.json()
                console.log(messages)
                let newList = messages.filter(message => {
                    if (message.lastmessage) {
                        message.timeSince = timeSince(message.lastmessage.Sent)
                        return message
                    } else return false
                })
                console.log(newList)
                setMessageList(newList)
            }
        }
        fetchData()

    }, [myEmail, setMessageList, url, friendEmail, timeSince])


   





    return (

        <section className="messages-section">
            {friendEmail === undefined
                ?
                <ul>
                    {messageList.map(message =>
                        <li className="messages-container" key={message.lastmessage.ID}>
                            <Link to={{ pathname: `${match.url}/${message.Email.substring(0, message.Email.lastIndexOf("@"))}`, state: { userName: message.userName } }}>
                                <h4>{message.userName}</h4>
                                <p>{message.lastmessage.messageText.substring(0,35)}</p>
                                <p>{message.timeSince}</p>
                            </Link>
                        </li>
                    )}
                </ul>
                : <PrivateMessages/>
            }


        </section>

    )
}

export default Messages