
import { useState, useEffect, useContext } from 'react'
import './friends.css'
import PortContext from '../../contexts/portContext';
import AddFriendOverlay from './AddFriendOverlay';


const Friends = () => {


    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`
    const [friendsList, setFriendsList] = useState([])
    const [showOverlay, setShowOverlay] = useState(false)


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
                {friendsList.map(friend => <li> <img className="friend-avatar" /> {friend.userName}</li>)}
            </ul>

        </section>

    )
}

export default Friends