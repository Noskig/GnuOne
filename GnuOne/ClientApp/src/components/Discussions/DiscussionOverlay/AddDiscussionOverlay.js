import { useState, useEffect, useContext } from 'react'
import './addDiscussionOverlay.css'
import PortContext from '../../../contexts/portContext';


const AddDiscussionOverlay = (props) => {
    const [discussionText, setDiscussionText] = useState('')
    const [headline, setHeadline] = useState('')
    const [user, setUser] = useState('default user')
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/discussions`


    useEffect(() => {
        fetchData()
    }, [])

    function handleClick(e) {
        e.preventDefault()
        let newDiscussion = {
            headline: headline,
            discussiontext: discussionText,
            user: user,
        }

        if (headline === "" || discussionText === "") {
            alert("Fill the missing fields please")
        }
        else {
            addNewDiscussion(newDiscussion)
        }

    }

    async function addNewDiscussion(discussion) {
        await fetch(url, {
            method: 'POST',
            body: JSON.stringify(discussion),
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
        <div className="new-discussion-overlay" >
            <section className="new-discussion-box">
                <button className="close" onClick={close}>✖️</button>
                <input type="text" placeholder={"Headline..."} onChange={e => setHeadline(e.target.value)} />
                <textarea rows="4" type="text" placeholder="What do you want to say?" onChange={e => setDiscussionText(e.target.value)} />
                <button type="button" onClick={(e) => handleClick(e)}>Post</button>
            </section>
        </div>
    )
}

export default AddDiscussionOverlay