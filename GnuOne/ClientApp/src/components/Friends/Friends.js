﻿
import { useState, useEffect, useContext } from 'react'
import './friends.css'
import PortContext from '../../contexts/portContext';
import AddFriendOverlay from './AddFriendOverlay/AddFriendOverlay';
import Search from '../Search/Search'
import { Link } from 'react-router-dom';
import FriendContext from '../../contexts/friendContext';
import MeContext from '../../contexts/meContext';


const Friends = () => {
    const friendEmail = useContext(FriendContext)
    const myEmail = useContext(MeContext)
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends/`
    const [friendsList, setFriendsList] = useState([])
    const [showOverlay, setShowOverlay] = useState(false)
    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    const filteredFriends = filterFriends(friendsList, searchTerm)

    useEffect(() => {
        fetchData()
    }, [])

    async function fetchData() {
        if (friendEmail === undefined) {
            const response = await fetch(url)
            const friends = await response.json()
            setFriendsList(friends)
        } else {
            const response = await fetch(url + friendEmail)
            const friend = await response.json()
            const friendsfriends = friend.MyFriendsFriends
            setFriendsList(friendsfriends)
        }
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
                    {filteredFriends.map(friend => <li key={friend.ID}>
                        {friendEmail === undefined && friend.isFriend ? <Link to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} >
                            <img className="friend-avatar" /> {friend.userName} </Link>
                            : friendEmail === undefined && !friend.isFriend
                                ? <><Link to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} >
                                    <img className="friend-avatar" /> {friend.userName} </Link> <button onClick={(e) => handleClick(e, friend)}>Accept friend</button> </>
                                : <> <img className="friend-avatar" /> {friend.userName}
                                    <button> Send friend request</button></>}
                        

                    
                </li>)}
            </ul>

        </section>
            </>
    )
}

export default Friends