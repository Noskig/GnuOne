
import { useState, useEffect, useContext } from 'react'
import PortContext from '../../contexts/portContext';


const Friends = () => {


    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`
    const [friendsList, setFriendsList] = useState([])

    useEffect(() => {
        fetchData()
    }, [])

    async function fetchData() {
        const response = await fetch(url)
        const friends = await response.json()
        console.log(friends)
        setFriendsList(friends)
    }
    //function handleClick(e) {
    //    e.preventDefault()
    //    let topicData = {
    //        headline: headline,
    //        discussiontext: description,
    //        user: user,
    //    }

    //    if (headline === "" || description === "") {
    //        alert("Fill the missing fields please")
    //    }
    //    else {
    //        addNewPost(topicData)
    //    }

    //}

    async function friendRequest(friendRequestEmail) {
        await fetch(url, {
            method: 'POST',
            body: JSON.stringify(friendRequestEmail),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        fetchData();
    }

  


    return (

        <section className="friends-container">
            <div className="addFriend"> </div>
            <div className="friends-list">
                <ul>
                    {friendsList.map(friend => <li>{friend.Email}</li>)}
                </ul>
            </div>

        </section>

    )
}

export default Friends