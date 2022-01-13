
import { useState, useEffect, useContext } from 'react'
import './friends.css'
import PortContext from '../../contexts/portContext';
import AddFriendOverlay from './AddFriendOverlay';
import Search from './Search'
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
    const [disabled, setDisabled] = useState(false)
    const [activeFriend, setActiveFriend] = useState(null)
    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    const filteredFriends = filterFriends(friendsList, searchTerm)

    useEffect(() => {
        fetchData()
    }, [friendEmail])

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
        setDisabled(false)
    }

    const openOverlay = (id) => {
        setShowOverlay(true)
        setDisabled(true)
        setActiveFriend(id)
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
            <Search search={search} />
            <section className="friends-container">
                {friendEmail === undefined
                    ?
                    <>{showOverlay
                        ? <>
                            <button className="new-friend" disabled={disabled}>Add new friend</button>
                            <AddFriendOverlay fetchData={fetchData} close={close} />
                        </>

                        : <button className="new-friend" onClick={openOverlay}> Add new friend </button>
                    }</>
                    : null
                }


                <ul className="friends-list">
                    {filteredFriends.map(friend => <li key={friend.ID}>
                        {friendEmail === undefined && friend.isFriend ? <Link to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} >
                            <img className="friend-avatar" /> {friend.userName} </Link>
                            : friendEmail === undefined && !friend.isFriend
                                ? <><Link to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} >
                                    <img className="friend-avatar" /> {friend.userName} </Link> <button onClick={(e) => handleClick(e, friend)}>Accept friend</button> </>
                                : <> <img className="friend-avatar" /> {friend.userName}
                                    {showOverlay
                                        ? <>
                                            <button disabled={disabled}>Send friend request</button>
                                            {activeFriend === friend.ID
                                                ? <AddFriendOverlay fetchData={fetchData} close={close} email={friend.Email} userName={friend.userName} />
                                                : null
                                            }
                                        </>

                                        : <button onClick={()=>openOverlay(friend.ID)}> Send friend request</button>
                                    }</>}



                    </li>)}
                </ul>

            </section>
        </>
    )
}

export default Friends