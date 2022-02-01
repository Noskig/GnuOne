import { useState, useEffect, useContext, useCallback } from 'react'
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
import share from '../../icons/share.svg'
import bookmark from '../../icons/bookmark.svg'




const Discussions = ({ routes }) => {
    const [discussions, setDiscussions] = useState([])
    const port = useContext(PortContext)
    console.log(port)
    const url = `https://localhost:${port}/api/`
    const [showOverlay, setShowOverlay] = useState(false)
    const [showDeleteConfirm, setshowDeleteConfirm] = useState(false)
    const [readMore, setReadMore] = useState(false);
    const [discussionText, setDiscussionText] = useState('')
    const [activeDiscussion, setActiveDiscussion] = useState('')
    const [editOpen, setEditOpen] = useState(false)
    let match = useRouteMatch()
    const [hover, setHover] = useState(false)

    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    const filteredDiscussions = filterDiscussions(discussions, searchTerm)
    const myEmail = useContext(MeContext)
    const { friendEmail } = useContext(FriendContext)
    console.log(myEmail)
    console.log(friendEmail)

    const fetchData = useCallback(async () => {
        const response = await fetch(url + 'discussions/')
        const discussions = await response.json()
        console.log(discussions)

        //SHOW ONLY MY OR MY FRIENDS DISCUSSIONS
        let filteredDisc = () => {
            console.log('myEmail: ' + myEmail)
            console.log('friendEmail: ' + friendEmail)
            if (discussions && (friendEmail === undefined)) {
                return discussions.filter((disc) => {
                    if (disc.Email === myEmail) {
                        console.log('displaying MY discussions: ' + disc.Email, myEmail)
                        return disc
                    } else return false
                })
            } else if (discussions && friendEmail) {
                return discussions.filter((disc) => {
                    if (disc.Email === friendEmail) {
                        console.log('displaying FRIENDs discussions: ' + disc.Email, friendEmail)
                        return disc
                    } else return false
                })
            } else return false
        }
        console.log(filteredDisc)
        setDiscussions(filteredDisc)
    }, [setDiscussions, url, myEmail, friendEmail])

    useEffect(() => {
        fetchData()
        console.log('i did it again')
    }, [myEmail, fetchData])

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
            await fetch(url + 'discussions/' + discussion.ID, {
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

    //fucntions to save bookmarks

    async function saveToBookmarks(saved) {
        await fetch('https://localhost:7261/api/bookmarks', {
            method: 'POST',
            body: JSON.stringify(saved),
            headers: {
                "Content-type": "application/json",
            }
        })
        fetchData();
    }

    async function removeFromBookmarks(saved) {
        await fetch('https://localhost:7261/api/bookmarks', {
            method: 'DELETE',
            body: JSON.stringify(saved),
            headers: {
                "Content-type": "application/json",
            }
        })
        console.log(saved)
        fetchData();
    }

    function sendEmailAndId(discussion) {
        let saved = {
            ID: discussion.ID,
            Email: discussion.Email,
        }
        if (!discussion.isBookmarked) {
            saveToBookmarks(saved);
        } else {
            removeFromBookmarks(saved)
        }
       
    }


    //=================================================
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
            } else { return false }

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
                                <input className="new-discussion" type="text" placeholder={"Create new..."} disabled onClick={() => setShowOverlay(true)} />
                                <AddDiscussionOverlay fetchData={fetchData} close={close} />
                            </>
                            : <input className="new-discussion" type="text" placeholder={"Create new..."} onClick={() => setShowOverlay(true)} />
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

                                : <Link className="discussion-content" to={{
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
                                    <div className={readMore && activeDiscussion === discussion.ID ? "" : "hide"}>

                                        <p className="text">{discussion.discussionText}</p>

                                    </div>
                                </Link>


                            }


                            <div className={readMore && activeDiscussion === discussion.ID ? "discussion-options" : "discussion-options hide"}>

                                {friendEmail === undefined
                                    ? <>
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
                                    </>


                                    : <>
                                        <button onMouseEnter={() => setHover(true)} onMouseLeave={() => setHover(false)} className="bookmark-button" onClick={() => sendEmailAndId(discussion)}>

                                            {
                                                discussion.isBookmarked && !hover
                                                    ? <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <path d="M6.19091 21.855C6.07879 21.9354 5.94667 21.9833 5.80909 21.9934C5.67151 22.0036 5.5338 21.9756 5.4111 21.9125C5.2884 21.8495 5.18547 21.7538 5.11363 21.636C5.04179 21.5183 5.00382 21.383 5.00391 21.245V6.25C5.00391 5.38805 5.34632 4.5614 5.95581 3.9519C6.5653 3.34241 7.39195 3 8.25391 3H15.7519C16.6139 3 17.4405 3.34241 18.05 3.9519C18.6595 4.5614 19.0019 5.38805 19.0019 6.25V21.246C19.0019 21.384 18.9639 21.5194 18.8919 21.6372C18.82 21.755 18.7169 21.8506 18.594 21.9136C18.4712 21.9766 18.3334 22.0044 18.1957 21.9941C18.0581 21.9837 17.926 21.9356 17.8139 21.855L12.0039 17.674L6.19191 21.854L6.19091 21.855Z" fill="white" />
                                                    </svg>

                                                    : discussion.isBookmarked && hover
                                                        ?
                                                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                            <path d="M6.19091 21.855C6.07879 21.9354 5.94667 21.9833 5.80909 21.9934C5.67151 22.0036 5.5338 21.9756 5.4111 21.9125C5.2884 21.8495 5.18547 21.7538 5.11363 21.636C5.04179 21.5183 5.00382 21.383 5.00391 21.245V6.25C5.00391 5.38805 5.34632 4.5614 5.95581 3.9519C6.5653 3.34241 7.39195 3 8.25391 3H15.7519C16.6139 3 17.4405 3.34241 18.05 3.9519C18.6595 4.5614 19.0019 5.38805 19.0019 6.25V21.246C19.0019 21.384 18.9639 21.5194 18.8919 21.6372C18.82 21.755 18.7169 21.8506 18.594 21.9136C18.4712 21.9766 18.3334 22.0044 18.1957 21.9941C18.0581 21.9837 17.926 21.9356 17.8139 21.855L12.0039 17.674L6.19191 21.854L6.19091 21.855ZM17.5029 6.25C17.5029 5.78587 17.3185 5.34075 16.9903 5.01256C16.6622 4.68437 16.217 4.5 15.7529 4.5H8.25391C7.78978 4.5 7.34466 4.68437 7.01647 5.01256C6.68828 5.34075 6.50391 5.78587 6.50391 6.25V19.782L11.5649 16.141C11.6925 16.0492 11.8457 15.9998 12.0029 15.9998C12.1601 15.9998 12.3133 16.0492 12.4409 16.141L17.5019 19.782V6.25H17.5029Z" fill="white" />
                                                        </svg>

                                                        : !discussion.isBookmarked && !hover

                                                            ? <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                                <path d="M6.19091 21.855C6.07879 21.9354 5.94667 21.9833 5.80909 21.9934C5.67151 22.0036 5.5338 21.9756 5.4111 21.9125C5.2884 21.8495 5.18547 21.7538 5.11363 21.636C5.04179 21.5183 5.00382 21.383 5.00391 21.245V6.25C5.00391 5.38805 5.34632 4.5614 5.95581 3.9519C6.5653 3.34241 7.39195 3 8.25391 3H15.7519C16.6139 3 17.4405 3.34241 18.05 3.9519C18.6595 4.5614 19.0019 5.38805 19.0019 6.25V21.246C19.0019 21.384 18.9639 21.5194 18.8919 21.6372C18.82 21.755 18.7169 21.8506 18.594 21.9136C18.4712 21.9766 18.3334 22.0044 18.1957 21.9941C18.0581 21.9837 17.926 21.9356 17.8139 21.855L12.0039 17.674L6.19191 21.854L6.19091 21.855ZM17.5029 6.25C17.5029 5.78587 17.3185 5.34075 16.9903 5.01256C16.6622 4.68437 16.217 4.5 15.7529 4.5H8.25391C7.78978 4.5 7.34466 4.68437 7.01647 5.01256C6.68828 5.34075 6.50391 5.78587 6.50391 6.25V19.782L11.5649 16.141C11.6925 16.0492 11.8457 15.9998 12.0029 15.9998C12.1601 15.9998 12.3133 16.0492 12.4409 16.141L17.5019 19.782V6.25H17.5029Z" fill="white" />
                                                            </svg>

                                                            : <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                                <path d="M6.19091 21.855C6.07879 21.9354 5.94667 21.9833 5.80909 21.9934C5.67151 22.0036 5.5338 21.9756 5.4111 21.9125C5.2884 21.8495 5.18547 21.7538 5.11363 21.636C5.04179 21.5183 5.00382 21.383 5.00391 21.245V6.25C5.00391 5.38805 5.34632 4.5614 5.95581 3.9519C6.5653 3.34241 7.39195 3 8.25391 3H15.7519C16.6139 3 17.4405 3.34241 18.05 3.9519C18.6595 4.5614 19.0019 5.38805 19.0019 6.25V21.246C19.0019 21.384 18.9639 21.5194 18.8919 21.6372C18.82 21.755 18.7169 21.8506 18.594 21.9136C18.4712 21.9766 18.3334 22.0044 18.1957 21.9941C18.0581 21.9837 17.926 21.9356 17.8139 21.855L12.0039 17.674L6.19191 21.854L6.19091 21.855Z" fill="white" />
                                                            </svg>
                                            }



                                        </button>

                                    </>
                                }

                            </div>


                            <div className="discussion-info">
                                <h4> Posts: {discussion.numberOfPosts}</h4>
                                <h4 className="createDate">{discussion.Date.slice(0, 19).replace('T', ' ').slice(0, 16)}</h4>
                                <img className={readMore && activeDiscussion === discussion.ID ? "read-more reverse-icon" : "read-more"} alt="read-more" src={arrows} onClick={() => readmoreAndId(discussion)} />
                                <div className="discussion-tags">{discussion.tags.map(tag => <h4 key={discussion.tags.indexOf(tag)}># {tag} </h4>)} </div>
                            </div>
                        </div>
                    ) : 'oops kan inte nå api'}
                </div>
            </section>
        </>

    )
}

export default Discussions