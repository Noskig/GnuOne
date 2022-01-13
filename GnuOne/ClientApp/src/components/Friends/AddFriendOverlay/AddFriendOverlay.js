import { useState, useEffect, useContext } from 'react'
import './addFriendOverlay.css'
import PortContext from '../../../contexts/portContext';


const AddFriendOverlay = (props) => {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`
    const newFriendEmail = props.email || null
    const [email, setEmail] = useState(newFriendEmail ? newFriendEmail : '')

    useEffect(() => {
        fetchData()
    }, [])

    function handleClick(e) {
        e.preventDefault()
        let newFriend = {
            Email: email
        }

        if (email === "") {
            alert("Email field cannot be left empty")
        }
        else {
            sendFriendRequest(newFriend)
        }

    }

    async function sendFriendRequest(newFriend) {
        await fetch(url, {
            method: 'POST',
            body: JSON.stringify(newFriend),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        fetchData();
        close()
    }

    function fetchData() {
        props.fetchData()
    }

    const close = () => {
        props.close()
    }
    return (
        <div className="add-friend-overlay">
            <div className="add-friend">
                <button className="close" onClick={close}>✖️</button>
                {
                    newFriendEmail
                        ? <h3>You are about to send a friend request to {props.userName}</h3>
                        : <input type="text" placeholder={"Email..."} onChange={e => setEmail(e.target.value)} />
                }
                <button type="button" onClick={(e) => handleClick(e)}>Send friend request</button>
            </div>
        </div>
    )
}

export default AddFriendOverlay