
import { useState, useEffect, useContext, useCallback } from 'react'
import './friends.css'
import PortContext from '../../contexts/portContext';
import AddFriendOverlay from './AddFriendOverlay/AddFriendOverlay';
import Search from '../Search/Search'
import { Link } from 'react-router-dom';
import FriendContext from '../../contexts/friendContext';
import MeContext from '../../contexts/meContext';
import WheelContext from '../../contexts/WheelContext'
import images from '../../Image';
import ThemeContext from '../../contexts/themeContext';



const Friends = () => {
    const { friendEmail } = useContext(FriendContext)
    console.log(friendEmail)
    const myEmail = useContext(MeContext)
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends/`
    const [friendsList, setFriendsList] = useState([])
    const [showOverlay, setShowOverlay] = useState(false)
    const [disabled, setDisabled] = useState(false)
    const [activeFriend, setActiveFriend] = useState(null)
    const { setChosenPage, setActive, setDone } = useContext(WheelContext);
    const {darkMode} = useContext(ThemeContext)

    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    const filteredFriends = filterFriends(friendsList, searchTerm)

    const fetchData = useCallback(async () => {
        if (friendEmail === undefined) {
            //get my own friends
            const response = await fetch(url)
            console.log('im here')
            const friends = await response.json()
            console.log(friends)
            setFriendsList(friends)
        } else {
            //get my friend's friends
            const responseOne = await fetch(url + friendEmail)
            console.log('i fucked up')
            const friend = await responseOne.json()
            const friendsfriends = friend.MyFriendsFriends

            //get my own friends for filtering purposes... 
            const responseTwo = await fetch(url)
            const allFriends = await responseTwo.json()

            let filteredFriendsfriends = friendsfriends.filter(friendsfriend => friendsfriend.Email !== myEmail)

            filteredFriendsfriends.forEach(friendsfriend => {
                let newList = allFriends.map(friend => friend.Email)
                console.log(newList)

                if (newList.includes(friendsfriend.Email)) {
                    friendsfriend.alreadyFriend = true
                }
            })
            console.log(friendsfriends, filteredFriendsfriends)
            setFriendsList(filteredFriendsfriends)
        }
    }, [setFriendsList, url, friendEmail, myEmail]);


    useEffect(() => {
        fetchData()
    }, [friendEmail, myEmail, fetchData])

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

    function wheelReset(id) {

        setChosenPage(id);
        setActive(true);
        setTimeout(animationEnd, 1)
    }

    function animationEnd() {

        setActive(false)
        setDone(true)
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

    // hide from friends friends 

    async function hideFromFriendsFriends(email) {
        await fetch(`https://localhost:${port}/api/myfriends/true`, {

            method: 'PATCH',
            body: JSON.stringify(email),
            headers: {
                "Content-type": "application/json",
            }
        })
        console.log(email)
        fetchData()
    }

    async function unHideFromFriendsFriends(email) {
        await fetch(`https://localhost:${port}/api/myfriends/false`, {

            method: 'PATCH',
            body: JSON.stringify(email),
            headers: {
                "Content-type": "application/json",
            }
        })
        console.log(email)
        fetchData()
    }

    // du är så jobbig

    function getOutOnClick(friend) {
        let noMoreNiceGuy = {
            isFriend: false,
            Email: friend.Email,
        }
        goodBye(noMoreNiceGuy)
    }

    async function goodBye(noMoreNiceGuy) {
        await fetch(`https://localhost:${port}/api/myfriends`, {

            method: 'DELETE',
            body: JSON.stringify(noMoreNiceGuy),
            headers: {
                "Content-type": "application/json",
            }
        })
        console.log(noMoreNiceGuy)
        fetchData()
    }

    //SEARCH

    function search(s) {
        setSearchTerm(s)
    }

    function filterFriends(friendsList, searchTerm) {
        return friendsList.filter((data) => {
            if (searchTerm === "") {
                return true
            } else if (data.userName?.toLowerCase().includes(searchTerm.toLowerCase())) {
                return data
            } else return false
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
                <h3> My friends 🤝🏻 </h3>
                <ul className="friends-list" >
                    {filteredFriends.map(friend =>
                        //dina vänner
                        friendEmail === undefined && friend.isFriend
                            ? <li className="friends-list-item" key={friend.ID}>
                                <Link  to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} onClick={() => wheelReset(0)} >
                                    <div className="friend-icon">
                                        <img alt={friend.userName} src={images[`Img${friend.pictureID}`]} />
                                    </div>
                                    <h2 className="userName"> {friend.userName} </h2>
                                </Link>
                                {/*hide friend*/}
                                {friend.hideMe === false ?
                                    <button className="accept-friend" onClick={() => hideFromFriendsFriends(friend.Email)}>
                                        Hide me
                                    </button>
                                    :
                                    <button className="accept-friend" onClick={() => unHideFromFriendsFriends(friend.Email)}>
                                        Show me
                                    </button>
                                }
                                <button className="accept-friend" onClick={() => getOutOnClick(friend)}>
                                    Remove friend
                                </button>
                            </li>
                            //din vänners vänner
                            : friendEmail !== undefined
                                ? <li className="friends-list-item" key={friend.ID}><Link to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} >
                                    <div className="friend-icon">
                                        <img alt={friend.userName} src={images[`Img${friend.pictureID}`]} />
                                    </div>
                                    <h2 className="userName">{friend.userName} </h2>
                                </Link>
                                    <>{
                                        friend.alreadyFriend
                                            ? null
                                            : <>{showOverlay
                                                ? <><button disabled={disabled}>Send friend request</button>
                                                    {activeFriend === friend.ID
                                                        ? <AddFriendOverlay fetchData={fetchData} close={close} email={friend.Email} userName={friend.userName} />
                                                        : null
                                                    }</>
                                                : <button className="accept-friend" onClick={() => openOverlay(friend.ID)}> Send friend request</button>
                                            }</>
                                    }</>
                                </li>
                                : null
                    )} </ul>
                {friendEmail === undefined
                    ? <>
                        <h3>New friend requests 🙍</h3>
                        <ul className="friends-list">
                            {filteredFriends.map(friend =>
                                !friend.isFriend && friend.userName
                                    ? <li className="friends-list-item" key={friend.ID}>
                                        <Link to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} >
                                            <div className="friend-icon">
                                                <img alt={friend.userName} src={images[`Img${friend.pictureID}`]} />
                                            </div>
                                            <h2 className="userName">{friend.userName}</h2>
                                        </Link>
                                        <button className="accept-friend" onClick={(e) => handleClick(e, friend)}>Accept friend</button>
                                    </li>
                                    : null
                            )}

                        </ul>

                        <h3 key="maybe-friends">Sent friend requests 🖅</h3>
                        <ul className="friends-list">
                            {filteredFriends.map(friend =>
                                !friend.isFriend && !friend.userName && friendsList
                                    ? <li className="friends-list-item" key={friend.ID}>
                                        <Link to={`/friendprofile/${friend.Email.substring(0, friend.Email.lastIndexOf("@"))}`} >
                                            <div className="friend-icon">
                                                <img alt={friend.userName} src={images[`Img${friend.pictureID}`]} />
                                            </div>
                                            <h2 className="userName">{friend.Email.substring(0, friend.Email.lastIndexOf("@"))}</h2>
                                        </Link>
                                        <div className="pending">Pending request</div>
                                    </li>
                                    : null
                            )}
                        </ul>
                    </>
                    : null}
            </section>
        </>
    )
}

export default Friends