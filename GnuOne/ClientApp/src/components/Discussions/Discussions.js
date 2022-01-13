﻿import { useState, useEffect, useContext } from 'react'
import PortContext from '../../contexts/portContext';
import AddDiscussionOverlay from './DiscussionOverlay/AddDiscussionOverlay';
import { Link, useRouteMatch } from 'react-router-dom';
import arrows from '../../icons/arrows.svg';
import trash from '../../icons/trash.svg'
import done from '../../icons/done.svg'
import edit from '../../icons/edit.svg'
import DeleteDiscussionOverlay from './DeleteDiscussionOverlay/DeleteDiscussionOverlay'
import Search from '../Search/Search.js';
import MeContext from '../../contexts/meContext';
import FriendContext from '../../contexts/friendContext';
import "./discussion.css";




const Discussions = ({ routes }) => {
    const [discussions, setDiscussions] = useState([])
    const port = useContext(PortContext)
    console.log(port)
    const url = `https://localhost:${port}/api/discussions/`
    const [showOverlay, setShowOverlay] = useState(false)
    const [showDeleteConfirm, setshowDeleteConfirm] = useState(false)
    const [readMore, setReadMore] = useState(false);
    const [discussionText, setDiscussionText] = useState('')
    const [activeDiscussion, setActiveDiscussion] = useState('')
    const [editOpen, setEditOpen] = useState(false)
    let match = useRouteMatch()
    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    const filteredDiscussions = filterDiscussions(discussions, searchTerm)
    const myEmail = useContext(MeContext)
    const friendEmail = useContext(FriendContext)
    console.log(myEmail)
    console.log(friendEmail)

    useEffect(() => {
        fetchData()
        console.log('i did it again')
    }, [myEmail])

    async function fetchData() {
        const response = await fetch(url)
        const discussions = await response.json()
        console.log(discussions)

        let filteredDisc = () => {

            console.log('myEmail: ' + myEmail)
            console.log('friendEmail: ' + friendEmail)
            if (discussions && (friendEmail === undefined)) {
                return discussions.filter((disc) => {

                    if (disc.Email === myEmail) {
                        console.log('displaying MY posts: ' + disc.Email, myEmail)
                        return disc
                    }

                })
            } else if (discussions && friendEmail) {
                return discussions.filter((disc) => {

                    if (disc.Email === friendEmail) {
                        console.log('displaying FRIENDs posts: ' + disc.Email, friendEmail)
                        return disc
                    }

                })
            }
        }
        console.log(filteredDisc)
        setDiscussions(filteredDisc)
    }

    const close = () => {
        setShowOverlay(false)
    }

    //EDIT 
    function openEditDiscussion(e, discussion) {
        e.preventDefault()
        setActiveDiscussion(discussion.ID)
        setEditOpen(true)
        setDiscussionText(discussion.discussionText)
    }

    async function confirmEditDiscussion(e, discussion) {
        e.preventDefault()

        if (discussion.discussionText !== discussionText) {
            discussion.discussionText = discussionText
            console.log(discussion)
            await fetch(url + discussion.ID, {
                method: 'PUT',
                body: JSON.stringify(discussion),
                headers: {
                    "Content-type": "application/json",
                }
            })
        }

        setEditOpen(false)
        setDiscussionText('')
    }

    //DELETE 
    const closeDeletion = () => {
        setshowDeleteConfirm(false)
    }

    function openDeleteOverlay(discussion) {
        setActiveDiscussion(discussion.ID)
        setshowDeleteConfirm(true)
    }
    function readmoreAndId(discussion) {
        setActiveDiscussion(discussion.ID)
        setReadMore(true)
        if (readMore == true && activeDiscussion == discussion.ID) {
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
            }

        })
    }

    return (
        <>
            <Search search={search} />
            <section className="discussions-container">



                {friendEmail === undefined
                    ? <div className="new-discussion-container">
                    {showOverlay
                        ? <>
                            <input className="new-discussion" type="text" placeholder={"Create new..."} onClick={() => setShowOverlay(true)}/>
                            <AddDiscussionOverlay fetchData={fetchData} close={close} />
                          </>
                        : <input className="new-discussion" type="text" placeholder={"Create new..."} onClick={() => setShowOverlay(true)}/>
                    }
                      </div>
                    : null}
                


                <div className="discussions-list">
                    {filteredDiscussions ? filteredDiscussions.map(discussion =>
                        <div className="discussion" key={discussion.ID + discussion.userName}>

                            {editOpen && activeDiscussion === discussion.ID
                                ? <div className="discussion-content">
                                    <h4 className="headline">{discussion.Headline}</h4>
                                    <div className={readMore ? "" : "hide"}>

                                        <textarea maxLength="500" value={discussionText} className="edit" onChange={(e) => setDiscussionText(e.target.value)} />

                                    </div>
                                </div>

                                : < Link className="discussion-content" to={{
                                    pathname: `${match.url}/${discussion.ID}`, state: {
                                        discussionText: discussion.discussionText,
                                        Headline: discussion.Headline,
                                        Date: discussion.Date,
                                        userName: discussion.userName,
                                        numberOfPosts: discussion.numberOfPosts,
                                        Email: discussion.Email
                                    }
                                }}>
                                    <h4 className="headline">{discussion.Headline}</h4>
                                    <div className={readMore && activeDiscussion == discussion.ID ? "" : "hide"}>

                                        <p className="text">{discussion.discussionText}</p>

                                    </div>
                                </Link>


                            }


                            <div className={readMore && activeDiscussion == discussion.ID ? "discussion-options" : "discussion-options hide"}>


                                {showDeleteConfirm && activeDiscussion === discussion.ID
                                    ? <>
                                        <button>
                                            <img alt="delete" src={trash} />
                                        </button>
                                        <DeleteDiscussionOverlay fetchData={fetchData} close={closeDeletion} discussionId={activeDiscussion} />
                                    </>
                                    :
                                    <button onClick={() => openDeleteOverlay(discussion)}>
                                        <img alt="delete" src={trash} />
                                    </button>
                                }
                                {editOpen && activeDiscussion === discussion.ID ?
                                    <button onClick={(e) => confirmEditDiscussion(e, discussion)}>
                                        <img alt="done" src={done} />
                                    </button>
                                    : <button onClick={(e) => openEditDiscussion(e, discussion)}>
                                        <img alt="edit" src={edit} />
                                    </button>

                                }



                            </div>


                            <div className="discussion-info">
                                <h4>{discussion.numberOfPosts} posts:</h4>
                                <h4 className="createDate">{discussion.Date.slice(0, 19).replace('T', ' ').slice(0, 16)}</h4>
                                <img className={readMore && activeDiscussion == discussion.ID ? "read-more reverse-icon" : "read-more"} alt="read-more" src={arrows} onClick={() => readmoreAndId(discussion)} />
                            </div>
                        </div>
                    ) : 'oops kan inte nå api'}
                </div>
            </section>
        </>

    )
}

export default Discussions