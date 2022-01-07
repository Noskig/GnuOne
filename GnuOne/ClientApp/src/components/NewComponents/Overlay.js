import { useState, useEffect, useContext } from 'react'
import './overlay.css'
import PortContext from '../../contexts/portContext';


const Overlay = (props) => {

    // const [topic, setmakeNewTopic] = useState('bananer')
    // const [description, setdiscriptionOfTopic] = useState('i love mi bannanas')
    // const [topic, setmakeNewTopic] = useState('')
    const [description, setdiscriptionOfTopic] = useState('')
    const [headline, setHeadline] = useState('')
    const [user, setUser] = useState('default user')
 
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/discussions`
    

    useEffect(() => {
        fetchData()
    }, [])

    function handleClick(e) {
        e.preventDefault()
        let topicData = {
            headline: headline,
            discussiontext: description,
            user: user,
        }

        if ( headline === "" || description === "" ) {
            alert("Fill the missing fields please")
        } 
        else {
            addNewPost(topicData)
        }

    }

    async function addNewPost(topicData) {
        await fetch(url, {
            method: 'POST',
            body: JSON.stringify(topicData),
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
        <div className="newTopicWrapper" >
            <section className="makeNewTopic">
                <button className="close" onClick={close}>✖️</button>
                <input type="text" placeholder={"Headline"} onChange={e => setHeadline(e.target.value)} />
                <textarea rows="4" type="text" placeholder="Ditt meddelande..." onChange={e => setdiscriptionOfTopic(e.target.value)} />
                <button type="button" onClick={(e) => handleClick(e)}>create new topic</button>
            </section>
        </div>
    )
}

export default Overlay