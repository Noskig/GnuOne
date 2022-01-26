import { useState, useEffect, useContext, useCallback } from 'react'
import PortContext from '../../contexts/portContext';
import AddDiscussionOverlay from '../Discussions/DiscussionOverlay/AddDiscussionOverlay';
import { Link, useRouteMatch } from 'react-router-dom';
import arrows from '../../icons/arrows.svg';
import Search from '../Search/Search.js';
import MeContext from '../../contexts/meContext';
import bookmark from '../../icons/bookmark.svg'




const Saved = ({ routes }) => {
    const [discussions, setDiscussions] = useState([])
    const port = useContext(PortContext)
    console.log(port)
    const url = `https://localhost:${port}/api/`
    const [showOverlay, setShowOverlay] = useState(false)
    const [readMore, setReadMore] = useState(false);
    const [activeDiscussion, setActiveDiscussion] = useState('')
    //let match = useRouteMatch()

    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    const filteredDiscussions = filterDiscussions(discussions, searchTerm)
    const myEmail = useContext(MeContext)
    console.log(myEmail)

    const fetchData = useCallback(async () => {
        const response = await fetch(url + 'bookmarks/')
        const bookMarks = await response.json()
        console.log(bookMarks.Discussusions)
        setDiscussions(bookMarks.Discussusions)
    }, [url, setDiscussions]);

    useEffect(() => {
        fetchData()
        console.log('i did it again')
    }, [myEmail, fetchData])

    const close = () => {
        setShowOverlay(false)
    }

    function readmoreAndId(discussion) {
        setActiveDiscussion(discussion.ID)
        setReadMore(true)
        if (readMore === true && activeDiscussion === discussion.ID) {
            setReadMore(false)
        }
    }
    //SEARCH 
    function search(s) {
        setSearchTerm(s)
    }

    function filterDiscussions(discussions, searchTerm) {
        return discussions.filter((data) => {
            if (searchTerm === "") {
                return true
            } else if (data.Headline.toLowerCase().includes(searchTerm.toLowerCase())) {
                return data
            } else return false
        })
    }
    // function som tar bort en topic som är sparad


    async function saveToBookmarks(Deleted) {
        await fetch('https://localhost:7261/api/bookmarks', {
            method: 'DELETE',
            body: JSON.stringify(Deleted),
            headers: {
                "Content-type": "application/json",
            }
        })
        console.log(Deleted)
    }


    function sendId(discussion) {
        let Deleted = {
            ID: discussion.ID,
            Email: discussion.Email,
        }
        saveToBookmarks(Deleted);
    }

    return (
        <>
            <Search search={search} />
            <section className="discussions-container">

                    <div className="new-discussion-container">
                    {showOverlay
                        ? <>
                            <input className="new-discussion" type="text" placeholder={"Create new..."} onClick={() => setShowOverlay(true)}/>
                            <AddDiscussionOverlay fetchData={fetchData} close={close} />
                          </>
                        : <input className="new-discussion" type="text" placeholder={"Create new..."} onClick={() => setShowOverlay(true)}/>
                    }
                    </div>


                <div className="discussions-list">
                    {filteredDiscussions ? filteredDiscussions.map(discussion =>
                        <div className="discussion" key={discussion.ID + discussion.userName}>

                          <Link className="discussion-content" to={{
                                    pathname: `/friendprofile/${discussion.Email.substring(0, discussion.Email.lastIndexOf("@"))}/discussions/${discussion.ID}`, state: {
                                        discussionText: discussion.discussionText,
                                        Headline: discussion.Headline,
                                        Date: discussion.Date,
                                        userName: discussion.userName,
                                        numberOfPosts: discussion.numberOfPosts,
                                        Email: discussion.Email
                                    }
                                }}>
                                    <h4 className="headline">{discussion.Headline}</h4>
                                    <div className={readMore && activeDiscussion === discussion.ID ? "" : "hide"}>

                                        <p className="text">{discussion.discussionText}</p>

                                    </div>
                                </Link>
                            
                            <div className={readMore && activeDiscussion === discussion.ID ? "discussion-options" : "discussion-options hide"}>
                                <>
                                    <button onClick={() => sendId(discussion)}>
                                        <img alt="bookmark" src={bookmark} />
                                    </button>
                                </>
                                    
                            </div>

                            <div className="discussion-info">
                                <h4>{discussion.numberOfPosts} posts:</h4>
                                <h4 className="createDate">{discussion.Date.slice(0, 19).replace('T', ' ').slice(0, 16)}</h4>
                                <img className={readMore && activeDiscussion === discussion.ID ? "read-more reverse-icon" : "read-more"} alt="read-more" src={arrows} onClick={() => readmoreAndId(discussion)} />
                                <div className="discussion-tags">{discussion.tags.map(tag => <h4 key={discussion.tags.indexOf(tag)}># {tag} </h4> )} </div>
                            </div>
                        </div>
                    ) : 'oops kan inte nå api'}
                </div>
            </section>
        </>
    )
}

export default Saved;