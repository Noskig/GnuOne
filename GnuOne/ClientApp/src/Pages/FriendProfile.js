import { useParams } from "react-router-dom"
import { useEffect, useState, useContext } from 'react'
import PortContext from '../contexts/portContext';

const FriendProfile = () => {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends/`
    const { email } = useParams()
    const [friends, setFriends] = useState()

 
 

    //useEffect(() => {
    //    fetchData()
    //}, [email])

    //async function fetchData() {
    //    console.log('fetching')
    //    const response = await fetch(url, {
    //        method: 'GET',
    //        body: JSON.stringify({ Email: email }),
    //        headers: {
    //            "Content-type": "application/json; charset=UTF-8",
    //        }
    //    })
    //    const friends = await response.json()
    //    console.log(friends)
    //    setFriends(friends);
       
    //}

 




    return (

        <section className="">

            <h3>Friend's profile</h3>
        

        </section>

    )
}

export default FriendProfile