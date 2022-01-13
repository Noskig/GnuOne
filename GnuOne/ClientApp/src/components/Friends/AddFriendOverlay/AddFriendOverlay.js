import { useState, useEffect, useContext } from 'react'
import './addFriendOverlay.css'
import PortContext from '../../../contexts/portContext';


const AddFriendOverlay = (props) => {
    const [email, setEmail] = useState('')
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`


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
                <input type="text" placeholder={"Email..."} onChange={e => setEmail(e.target.value)} />
                <button type="button" onClick={(e) => handleClick(e)}>Send friend request</button>
            </div>
        </div>
    )
}

export default AddFriendOverlay