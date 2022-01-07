import { useState, useEffect, useContext } from 'react'
import PortContext from '../../contexts/portContext';
import AddDiscussionOverlay from './AddDiscussionOverlay';
import { Link } from 'react-router-dom';
import arrows from '../../icons/arrows.svg'
import trash from '../../icons/trash.svg'
import done from '../../icons/done.svg'
import edit from '../../icons/edit.svg'
import DeleteDiscussionOverlay from './DeleteDiscussionOverlay'


const Discussions = () => {
    const [discussions, setDiscussions] = useState([])
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/discussions/`
    const [showOverlay, setShowOverlay] = useState(false)
    const [showDeleteConfirm, setshowDeleteConfirm] = useState(false)
    const [readMore, setReadMore] = useState(false);
    const [discussionText, setDiscussionText] = useState('')
    const [activeDiscussion, setActiveDiscussion] = useState('')
    const [editOpen, setEditOpen] = useState(false)


    useEffect(() => {
        fetchData()
    }, [])

    async function fetchData() {
        const response = await fetch(url)
        const discussions = await response.json()
        console.log(discussions)
        setDiscussions(discussions)
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

        return (

            <section className="discussions-container">




                <div className="new-discussion-container">
                    {showOverlay
                        ? <>
                            <input className="new-discussion" type="text" placeholder={"Create new..."} onClick={() => setShowOverlay(true)} />

                            <AddDiscussionOverlay fetchData={fetchData} close={close} />
                        </>
                        :
                        <input className="new-discussion" type="text" placeholder={"Create new..."} onClick={() => setShowOverlay(true)} />
                    }
                </div>


                <div className="discussions-list">
                    {discussions ? discussions.map(discussion =>
                        <div className="discussion" key={discussion.ID + discussion.userName}>

                            {editOpen && activeDiscussion === discussion.ID
                                ?<>
                            <h4 className="">{discussion.Headline}</h4>
                            <div className={readMore ? "" : "hide"}>

                                 <textarea maxLength="500" value={discussionText} className="edit" onChange={(e) => setDiscussionText(e.target.value)} />

                            </div>
                                </>
                                
                                : < Link className="dicussion-content" to={{
                                    pathname: `/discussions/${discussion.ID}`, state: {
                                        discussionText: discussion.discussionText,
                                        Headline: discussion.Headline,
                                        Date: discussion.Date,
                                        userName: discussion.userName,
                                        numberOfPosts: discussion.numberOfPosts,
                                        Email: discussion.Email
                                    }
                                }}>
                                    <h4 className="">{discussion.Headline}</h4>
                                    <div className={readMore ? "" : "hide"}>

                                        <p className="text">{discussion.discussionText}</p>

                                    </div>
                                </Link>
                                
                                
                                }


                            <div className="discussion-options">


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
                                <h4>{discussion.numberOfPosts} posts on this topic</h4>
                                <h4 className="createDate">{discussion.Date.slice(0, 19).replace('T', ' ').slice(0, 16)}</h4>
                                <img alt="read-more" src={arrows} onClick={() => setReadMore(!readMore)} />
                            </div>
                        </div>
                    ) : 'oops kan inte nå api'}
                </div>
            </section>


        )
    }

    export default Discussions