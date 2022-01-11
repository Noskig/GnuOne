
import { useState, useEffect, useContext } from 'react'
import './friends.css'
import PortContext from '../../contexts/portContext';
import AddFriendOverlay from './AddFriendOverlay';
import Search from './Search'
import { Link } from 'react-router-dom';

const Friends = () => {


    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`
    const [friendsList, setFriendsList] = useState([])
    const [showOverlay, setShowOverlay] = useState(false)
    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    const filteredFriends = filterFriends(friendsList, searchTerm)

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

    //SEARCH
    function search(s) {
        setSearchTerm(s)
    }

    function filterFriends(friendsList, searchTerm) {
        return friendsList.filter((data) => {
            if (searchTerm === "") {
                return true
            } else if (data.userName.toLowerCase().includes(searchTerm.toLowerCase())) {
                return data
            }

        })
    }

    return (
        <>
            <Search search={search}/>
        <section className="friends-container">

            {showOverlay
                ? <>
                    <button className="new-friend" disabled="true">Add new friend</button>
                    <AddFriendOverlay fetchData={fetchData} close={close} />
                </>

                : <button className="new-friend" onClick={() => setShowOverlay(true)}> Add new friend </button>
            }

                <ul className="friends-list">
                    {filteredFriends.map(friend => <li key={friend.ID}> <Link to={`/friendprofile/${friend.Email}`} >
                        <img className="friend-avatar" /> {friend.userName} </Link>
                    {friend.isFriend ? null : <button onClick={(e) => handleClick(e, friend)}>Accept friend</button>}
                </li>)}
            </ul>

        </section>
            </>
    )
}

export default Friends