import { useState, useEffect, useContext } from 'react'
import './addDiscussionOverlay.css'
import PortContext from '../../../contexts/portContext';


const AddDiscussionOverlay = (props) => {
    const [discussionText, setDiscussionText] = useState('')
    const [headline, setHeadline] = useState('')
    const [user, setUser] = useState('default user')
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/discussions`

    const [tags, setTags] = useState([]);
    const [chosenTag1, setChosenTags1] = useState();
    const [chosenTag2, setChosenTags2] = useState();
    const [chosenTag3, setChosenTags3] = useState();

    useEffect(() => {
        fetchData()
        fetchTags()
    }, [])

    function handleClick(e) {
        e.preventDefault()
        let newDiscussion = {
            headline: headline,
            discussiontext: discussionText,
            user: user,
            tagOne: chosenTag1,
            tagTwo: chosenTag2,
            tagThree: chosenTag3
        }

        if (headline === "" || discussionText === "") {
            alert("Fill the missing fields please")
        }
        else {
            console.log(newDiscussion)
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

    async function fetchTags() {
        const response = await fetch(`https://localhost:${port}/api/tags`)
        const tags = await response.json()
        console.log(tags);
        setTags(tags);
    }

    return (
        <div className="new-discussion-overlay" >
            <section className="new-discussion-box">
                <button className="close" onClick={close}>✖️</button>
                <input type="text" placeholder={"Headline..."} onChange={e => setHeadline(e.target.value)} />
                <textarea rows="4" type="text" placeholder="What do you want to say?" onChange={e => setDiscussionText(e.target.value)} />

                <form>

                    <select onChange={(e) => setChosenTags1(e.target.value)} >
                        <option>
                            Choose one...
                        </option>
                        {tags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>

                    <select onChange={(e) => setChosenTags2(e.target.value)} >
                        <option>
                            Choose one...
                        </option>
                        {tags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>

                    <select onChange={(e) => setChosenTags3(e.target.value)} >
                        <option>
                            Choose one...
                        </option>
                        {tags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>

                </form>

                <button type="button" onClick={(e) => handleClick(e)}>Post</button>
            </section>


        </div>
    )
}

export default AddDiscussionOverlay