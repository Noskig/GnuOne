import PortContext from '../contexts/portContext';
import { useParams, useLocation } from 'react-router-dom';
import { useEffect, useState, useContext } from 'react';
import trash from '../icons/trash.svg'
import done from '../icons/done.svg'
import edit from '../icons/edit.svg'
import Search from './Search/Search'
import './comments.css'
import share from '../icons/share.svg'
import cancel from '../icons/x.svg'
import bookmark from '../icons/bookmark.svg'
import MeContext from '../contexts/meContext';

const Comments = () => {
    const myEmail = useContext(MeContext)
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/`
    const [post, setPost] = useState({})
    const [comments, setComments] = useState([])
    const [commentText, setCommentText] = useState('')
    const [activeComment, setActiveComment] = useState()
    const [editOpen, setEditOpen] = useState(false)
    const [showDeleteConfirm, setShowDeleteConfirm] = useState(false)
    const [charactersLeft, setCharactersLeft] = useState(0)
    const { id } = useParams();
    let location = useLocation();
    let postInfo = location.state
    console.log(postInfo)
    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    console.log(comments)
    const filteredComments = filterComments(comments, searchTerm)

    console.log(postInfo)
    console.log(location)

    console.log(id)

    useEffect(() => {
        fetchData()
    }, [id])

    async function fetchData() {
        console.log('fetching')
        const response = await fetch(url + 'posts/' + id)
        const post = await response.json()
        console.log(post)
    //    //GET TAGS 
    //    const responseTwo = await fetch(url + 'tags')
    //    const tags = await responseTwo.json()
    //    console.log(tags)
    //    let discussionTags = tags.filter(tag => tag.ID === discussion.tagOne || tag.ID === discussion.tagTwo || tag.ID === discussion.tagThree)
    //    console.log(discussionTags)
    //    discussion.firstTag = discussionTags[0] ? discussionTags[0].tagName : null
    //    discussion.secondTag = discussionTags[1] ? discussionTags[1].tagName : null
    //    discussion.thirdTag = discussionTags[2] ? discussionTags[2].tagName : null
    ////end

    setPost(post);
    setComments(post.comments)

}

//COMMENT
function validateNewComment(commentText) {
    if (commentText.length <= 500) {
        setCommentText(commentText)
        setCharactersLeft(commentText.length)
    }
}

function createNewComment(e) {
    e.preventDefault()
    let newComment = {
        userName: postInfo.userName,
        commentText: commentText,
        postID: Number(id),
        postEmail: postInfo.Email
    }
    console.log(newComment)
    addNewComment(newComment)
    setCommentText('')
}

async function addNewComment(newComment) {
    await fetch(url + 'comments', {
        method: 'POST',
        body: JSON.stringify(newComment),
        headers: {
            "Content-type": "application/json; charset=UTF-8",
        }
    })

    fetchData();
    setCharactersLeft(0);
}

//EDIT 
function openEditComment(e, comment) {
    e.preventDefault()
    console.log(comment)
    setActiveComment(comment.id)
    console.log(comment.id)
    console.log(activeComment)
    setEditOpen(true)
    setCommentText(comment.commentText)
}

async function confirmEditComment(e, comment) {
    e.preventDefault()
    console.log('fetching')
    if (comment.commentText !== commentText) {
        comment.commentText = commentText
        console.log(comment)
        await fetch(url + 'comments/' + comment.id, {
            method: 'PUT',
            body: JSON.stringify(comment),
            headers: {
                "Content-type": "application/json",
            }
        })
    }

    setEditOpen(false)
    setCommentText('')
}

//DELETE 
function openDeleteComment(e, comment) {
    e.preventDefault()
    setActiveComment(comment.id)
    setShowDeleteConfirm(true)
    }

    function closeOverlay() {
        /*e.preventDefault()*/
        setActiveComment(null)
        setShowDeleteConfirm(false)
    }

async function deleteComment(e, id) {
    e.preventDefault()
    console.log('fetching')
    console.log(id)
    await fetch(url + 'comments/' + id, {
        method: 'DELETE',
        body: { id: id },
        headers: {
            "Content-type": "application/json",
        }
    });
    fetchData()
    setShowDeleteConfirm(false)
}

//SEARCH 
function search(s) {
    setSearchTerm(s)
}

function filterComments(comments, searchTerm) {
    return comments.filter((data) => {
        if (searchTerm === "") {
            return true
        } else if (data.commentText.toLowerCase().includes(searchTerm.toLowerCase())) {
            return data
        }

    })
}

return (
    <>
        <Search search={search} />
        <section className="comments-container">
            <h2>{post.headline}</h2>
                <div className="comment">
                    {editOpen && activeComment === post.id
                        ? <textarea maxLength="500" value={post.postText} className="edit" onChange={(e) => setCommentText(e.target.value)} />
                        : <p className="text">{post.postText}</p>
                    }

                    <div className="comment-info">
                        <h4>{filteredComments.length} comment(s) on this post</h4>
                        <h4 className="createDate">{post.date?.slice(0, 16).replace('T',' ')}</h4>

                    </div>

            </div>
            <div className="comments-list">
            <ul >
                {comments ? filteredComments.map(comment =>
                    <li className={comment.commentText === "Deleted comment" ? "comment deleted-comment": "comment"} key={comment.id + comment.userName}>
                        {showDeleteConfirm && activeComment === comment.id
                            ? <div className="delete-comment-overlay">
                                <p> Are you sure you want to delete this comment?</p>
                                <button onClick={(e) => deleteComment(e, comment.id)}>
                                    <svg width="26" height="22" viewBox="0 0 26 22" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M21.7585 0L8.5106 13.4289L4.21399 9.0736L0 13.3452L4.29661 17.7005L8.53812 22L12.7521 17.7284L26 4.29952L21.7585 0Z" fill="black" />
                                    </svg>
                                </button>
                                <button onClick={closeOverlay}> <img alt="cancel" src={cancel}/> </button>
                            </div>
                            : null}
                        {editOpen && activeComment === comment.id
                            ? <textarea className="text edit" maxLength="500" value={commentText} onChange={(e) => setCommentText(e.target.value)} />
                            : <p className="text">{comment.commentText}</p>
                        }

                        <div className="comment-options">
                                    
                            {comment.email === myEmail
                                ? <> {showDeleteConfirm && activeComment === comment.id
                                    ?<>
                                    <button onClick={(e) => deleteComment(e, comment.id)}>
                                        <img alt="done" src={done} />
                                    </button>
                                    </>
                                    :
                                    <button onClick={(e) => openDeleteComment(e, comment)}>
                                        <img alt="delete" src={trash} />
                                    </button>
                                }
                                    {editOpen && activeComment === comment.id ?
                                        <button onClick={(e) => confirmEditComment(e, comment)}>
                                            <img alt="done" src={done} />
                                        </button>
                                        : 
                                        <button onClick={(e) => openEditComment(e, comment)}>
                                            <img alt="edit" src={edit} />
                                        </button>

                                    }</>


                                : <>
                                    <button>
                                        <img alt="bookmark" src={bookmark} />
                                    </button>
                                    <button>
                                        <img alt="share" src={share} />
                                    </button>
                                </>

                            }</div>



                        <div className="comment-info">
                            {/*<img className="friend-avatar" alt={comment.pictureID} />*/} <h4> {comment.userName} </h4>
                            <h4 className="createDate">{comment.date.slice(0, 16).replace('T', ' ')}</h4>
                         

                        </div>
                    </li>
                ) : 'oops kan inte nå api'}
                </ul>

            {editOpen
                ? <p>pls finish editing ur comment before writing a new one'</p>
                : <form>
                    <textarea rows="4" maxLength="500" placeholder="Write something..." value={commentText} className="input-text" onChange={e => validateNewComment(e.target.value)} />
                    <p>{charactersLeft}/500</p>
                    <button type="button" className="btn" onClick={(e) => createNewComment(e)}>Comment</button>
                </form>}
                </div>
        </section>
    </>
)
}

export default Comments