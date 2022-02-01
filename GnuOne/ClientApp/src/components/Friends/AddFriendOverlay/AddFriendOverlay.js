import { useState, useContext } from 'react'
import './addFriendOverlay.css'
import PortContext from '../../../contexts/portContext';
import FriendContext from '../../../contexts/friendContext';

const AddFriendOverlay = (props) => {
    const [email, setEmail] = useState('')
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`
    const { friendEmail } = useContext(FriendContext)

    //useEffect(() => {
    //    fetchData()
    //}, [])

    function handleClick(e, email) {
        e.preventDefault()
        let newFriend = {
            Email: email
        }

        //if (email === "") {

        //    /*alert("Email field cannot be left empty")*/
        //}
        //else {
            sendFriendRequest(newFriend)
       /* }*/

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
                    friendEmail === undefined
                        ? <>
                            <input type="text" autoFocus placeholder={"Email..."} onChange={e => setEmail(e.target.value)} />
                            <button type="button" onClick={(e) => handleClick(e, email)}>Send friend request</button>

                        </>
                        : <>
                            <h3>You are about to send a friend request to {props.userName} </h3>
                            <button type="button" onClick={(e) => handleClick(e, props.email)}>Send friend request</button>

                            </>
                }
               
            </div>
        </div>
    )
}

export default AddFriendOverlay