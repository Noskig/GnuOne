import { useParams } from "react-router-dom"
import { useEffect, useState, useContext } from 'react'
import PortContext from '../contexts/portContext';
import FriendContext from '../contexts/friendContext'
import MeContext from '../contexts/meContext'
import ProfileWheel from '../components/NewComponents/ProfileWheel'

const FriendProfile = ({ routes }) => {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/`
    const { email } = useParams()
    const [friend, setFriend] = useState()
    const [myEmail, setMyEmail] = useState('')

    useEffect(() => {
        fetchFriend()
        fetchMyEmail()
    }, [email])

    async function fetchFriend() {
        console.log('fetching')
        console.log(email)
        const response = await fetch(url + 'myfriends', {
            method: 'PATCH',
            body: JSON.stringify(email + '@gmail.com'),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        const friend = await response.json()
        console.log(friend)
        setFriend(friend);

    }

    console.log(friend)
    async function fetchMyEmail() {
        console.log('fetching')
        const response = await fetch(url + 'settings')
        const me = await response.json()
        console.log(me)
        const myEmail = me.email
        console.log(myEmail)
        setMyEmail(myEmail);
    }

    return (
        <MeContext.Provider value={myEmail}>
            <FriendContext.Provider value={friend?.MyFriend.Email}>
                <section className="">
                    <h3>Friend's profile</h3>
                    {friend?.MyFriend.userName}
                    <ProfileWheel routes={routes} />
            </section>
        </FriendContext.Provider>
        </MeContext.Provider>
    )
}

export default FriendProfile