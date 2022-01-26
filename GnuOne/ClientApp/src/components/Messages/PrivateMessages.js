import { useState, useContext, useEffect, useCallback } from "react"
import { useParams, useLocation } from "react-router-dom"
import PortContext from '../../contexts/portContext';
import MeContext from '../../contexts/meContext';
import FriendContext from '../../contexts/friendContext';

const PrivateMessages = () => {
    const [privateMessages, setPrivateMessages] = useState([])
    const [messageText, setMessageText] = useState('')
    const { friendEmail } = useContext(FriendContext)
    const myEmail = useContext(MeContext)
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/messages/`
    const { email } = useParams()
    let location = useLocation();
    if (friendEmail === undefined) {
        let { userName } = location.state
        console.log(userName)
    }
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
        if (seconds < 1) {
            seconds = 1
        }
        console.log(seconds)
        var duration = getDuration(seconds);
        console.log(duration)
        var suffix = (duration.interval > 1 || duration.interval === 0) ? 's' : '';
        return duration.interval + ' ' + duration.epoch + suffix + ' ago';
    }, [getDuration]);


    const fetchPrivateMessages = useCallback(async (friend) => {
        const response = await fetch(url + 'dm', {
            method: 'PATCH',
            body: JSON.stringify(friend),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        const messages = await response.json()
        console.log(messages)
        let newList = messages.map(message => {

            message.timeSince = timeSince(message.Sent)
            return message

        })
        console.log(newList)
        setPrivateMessages(newList)
    }, [setPrivateMessages, url, timeSince]);

    useEffect(() => {
        const interval = setInterval(() => {
            fetchPrivateMessages({ Email: `${email}@gmail.com` })
        }, 2000);
        return () => clearInterval(interval);
    }, [email, fetchPrivateMessages]);

    function handleClick(e) {
        e.preventDefault()
        let newMessage = {
            To: friendEmail || `${email}@gmail.com`,
            From: myEmail,
            messageText: messageText
        }
        console.log(newMessage)
        sendNewMessage(newMessage)
     
    }

    async function sendNewMessage(message) {
        await fetch(url, {
            method: 'POST',
            body: JSON.stringify(message),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        fetchPrivateMessages({ Email: `${email}@gmail.com` });
        setMessageText('')
    }

    return (

        <section className="messages-container">

            <ul>
                {privateMessages.map(message => <li key={message.ID}>
                    <h4>{message.FromUserName}</h4>
                    <p>{message.messageText}</p>
                    <p>{message.timeSince}</p>
                </li>)}
            </ul>

            <div>
                <textarea rows="4" type="text" placeholder="Meddelande..." value={messageText} onChange={e => setMessageText(e.target.value)} />
                <button onClick={(e) => handleClick(e)}>Send message</button>
            </div>

        </section>

    )
}

export default PrivateMessages