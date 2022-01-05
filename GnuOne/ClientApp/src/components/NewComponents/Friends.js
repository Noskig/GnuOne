
import { useState, useEffect, useContext } from 'react'
import './friends.css'
import PortContext from '../../contexts/portContext';
import AddFriendOverlay from './AddFriendOverlay';


const Friends = () => {


    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`
    const [friendsList, setFriendsList] = useState([])
    const [showOverlay, setShowOverlay] = useState(false)
    const [isFriend, setIsFriend] = useState(false)


    useEffect(() => {
        fetchData()
    }, [])

    async function fetchData() {
        const response = await fetch(url)
        const friends = await response.json()
        console.log(friends)
        setFriendsList(friends)
    }

    const close = () => {
        setShowOverlay(false)
    }

    function handleClick(e, friend) {
        e.preventDefault()
        let newFriend = {
            Email: friend.Email,
            IsFriend: true
        }
        acceptRequest(newFriend)


    }

    async function acceptRequest(newFriend) {
        console.log(newFriend)
        await fetch(url, {
            method: 'PUT',
            body: JSON.stringify(newFriend),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        fetchData();
    }

    return (

        <section className="friends-container">

            {showOverlay
                ? <>
                    <button className="new-friend" disabled="true">Add new friend</button>
                    <AddFriendOverlay fetchData={fetchData} close={close} />
                </>

                : <button className="new-friend" onClick={() => setShowOverlay(true)}> Add new friend </button>
            }

            <ul className="friends-list">
                {friendsList.map(friend => <li key={friend.ID}> <img className="friend-avatar" /> {friend.userName}
                    <button onClick={(e) => handleClick(e, friend)}>Accept friend</button>
                </li>)}
            </ul>

        </section>

    )
}

export default Friends