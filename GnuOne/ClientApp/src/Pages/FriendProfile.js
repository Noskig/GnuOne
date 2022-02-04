import { useParams } from "react-router-dom"
import { useEffect, useState, useContext } from 'react'
import PortContext from '../contexts/portContext';
import FriendContext from '../contexts/friendContext'
import MeContext from '../contexts/meContext'
import ProfileWheel from '../components/MenuWheel/ProfileWheel'
import Navbar from '../components/Navbar/NavBar';

const FriendProfile = ({ routes }) => {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/`
    const { email } = useParams()
    const [friend, setFriend] = useState()
    const [myEmail, setMyEmail] = useState('')
    const [friendsUserName, setFriendsUserName] = useState('')

    useEffect(() => {
        async function fetchFriend() {
            console.log('fetching')
            console.log(email)
            const response = await fetch(url + 'myfriends/' + email + '@gmail.com')
            const friend = await response.json()
            console.log(friend)
            setFriend(friend);
            setFriendsUserName(friend.MyFriend.userName)
        }

        async function fetchMyEmail() {
            console.log('fetching')
            const response = await fetch(url + 'settings')
            const me = await response.json()
            console.log(me)
            const myEmail = me.email
            console.log(myEmail)
            setMyEmail(myEmail);
        }

        fetchFriend()
        fetchMyEmail()
    }, [email, setFriend, setFriendsUserName, setMyEmail, url])

    

    console.log(friend)
   

    return (
        <MeContext.Provider value={myEmail}>
            <FriendContext.Provider value={{ friendEmail: friend?.MyFriend.Email, friendImg: friend?.MyFriend.pictureID }}>
                <main className="main">
                    <Navbar />
                    <h1 className="friends-name">{friendsUserName}</h1>
                    <ProfileWheel routes={routes} />
                    </main>
        </FriendContext.Provider>
        </MeContext.Provider>
    )
}

export default FriendProfile