import PortContext from '../contexts/portContext';
import { useParams, useLocation, Link, useRouteMatch } from 'react-router-dom';
import { useEffect, useState, useContext } from 'react';
import trash from '../icons/trash.svg'
import done from '../icons/done.svg'
import edit from '../icons/edit.svg'
import Search from './Search/Search'
import './posts.css'
import share from '../icons/share.svg'
import cancel from '../icons/x.svg'
import bookmark from '../icons/bookmark.svg'
import MeContext from '../contexts/meContext';


const Posts = () => {
    const myEmail = useContext(MeContext)
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/`
    const [discussion, setDiscussion] = useState({})
    const [posts, setPosts] = useState([])
    const [postText, setPostText] = useState('')
    const [username, setUsername] = useState('me')
    const [activePost, setActivePost] = useState()
    const [editOpen, setEditOpen] = useState(false)
    const [showDeleteConfirm, setShowDeleteConfirm] = useState(false)
    const [charactersLeft, setCharactersLeft] = useState(0)
    const { id } = useParams();
    let location = useLocation();
    let discussionInfo = location.state
    let match = useRouteMatch()

    //SEARCH 
    const [searchTerm, setSearchTerm] = useState('')
    console.log(posts)
    const filteredPosts = filterPosts(posts, searchTerm)

    console.log(discussionInfo)
    console.log(location)

    console.log(id)

    useEffect(() => {
        fetchData()
    }, [id])

    async function fetchData() {
        console.log('fetching')
        const response = await fetch(url + 'discussions/' + id)
        const discussion = await response.json()
        console.log(discussion)
        //GET TAGS 
        const responseTwo = await fetch(url + 'tags')
        const tags = await responseTwo.json()
        console.log(tags)
        let discussionTags = tags.filter(tag => tag.ID === discussion.tagOne || tag.ID === discussion.tagTwo || tag.ID === discussion.tagThree)
        console.log(discussionTags)
        discussion.firstTag = discussionTags[0] ? discussionTags[0].tagName : null
        discussion.secondTag = discussionTags[1] ? discussionTags[1].tagName : null
        discussion.thirdTag = discussionTags[2] ? discussionTags[2].tagName : null
    
    //end
    setDiscussion(discussion);
    setPosts(discussion.posts)

}

//POST
function validateNewPost(postText) {
    if (postText.length <= 500) {
        setPostText(postText)
        setCharactersLeft(postText.length)
    }
}

function createNewPost(e) {
    e.preventDefault()
    let newPost = {
        userName: username,
        postText: postText,
        discussionID: Number(id),
        discussionEmail: discussionInfo.Email
    }
    console.log(newPost)
    addNewPost(newPost)
    setPostText('')
}

async function addNewPost(newPost) {
    await fetch(url + 'posts', {
        method: 'POST',
        body: JSON.stringify(newPost),
        headers: {
            "Content-type": "application/json; charset=UTF-8",
        }
    })

    fetchData();
    setCharactersLeft(0);
}

//EDIT 
function openEditPost(e, post) {
    e.preventDefault()
    console.log(post)
    setActivePost(post.id)
    console.log(post.id)
    console.log(activePost)
    setEditOpen(true)
    setPostText(post.postText)
}




async function confirmEditPost(e, post) {
    e.preventDefault()
    console.log('fetching')
    if (post.postText !== postText) {
        post.postText = postText
        console.log(post)
        await fetch(url + 'posts/' + post.id, {
            method: 'PUT',
            body: JSON.stringify(post),
            headers: {
                "Content-type": "application/json",
            }
        })
    }

    setEditOpen(false)
    setPostText('')
}

//DELETE 
function openDeletePost(e, post) {
    e.preventDefault()
    setActivePost(post.id)
    setShowDeleteConfirm(true)
    }

    function closeOverlay() {
        /*e.preventDefault()*/
        setActivePost(null)
        setShowDeleteConfirm(false)
    }

async function deletePost(e, id) {
    e.preventDefault()
    console.log('fetching')
    console.log(id)
    await fetch(url + 'posts/' + id, {
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

function filterPosts(posts, searchTerm) {
    return posts.filter((data) => {
        if (searchTerm === "") {
            return true
        } else if (data.postText.toLowerCase().includes(searchTerm.toLowerCase())) {
            return data
        }

    })
}

return (
    <>
        <Search search={search} />
        <section className="posts-container">
            <h2>{discussionInfo.Headline}</h2>
            <div className="discussion-tags">{discussion.firstTag ? `#${discussion.firstTag}` : null} {discussion.secondTag ? `#${discussion.secondTag}` : null}  {discussion.thirdTag ? `#${discussion.thirdTag}` : null} </div>

            <ul className="posts-list">
                <li className="post">
                    {/*<div className="posts-wrapper">*/}
                        {editOpen && activePost === discussion.id
                            ? <textarea maxLength="500" value={discussion.discussionText} className="edit" onChange={(e) => setPostText(e.target.value)} />
                            : <p className="text">{discussion.discussionText}</p>
                        }

                        <div className="post-info">
                            <h4>{filteredPosts.length} posts on this topic</h4>
                            {/*<h4 className="createDate">{discussionInfo.Date.slice(0, 16).replace('T', ' ')}</h4>*/}

                        </div>
                    {/*</div>*/}
                </li>
                {posts ? filteredPosts.map(post =>
                    <li className={post.postText === "Deleted post" ? "post deleted-post": "post"} key={post.id + post.userName}>
                        {showDeleteConfirm && activePost === post.id
                            ? <div className="delete-post-overlay">
                                <p> Are you sure you want to delete this post?</p>
                                <div className="confirm-button-wrapper">
                                    <button onClick={(e) => deletePost(e, post.id)}>
                                        <svg width="26" height="22" viewBox="0 0 26 22" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M21.7585 0L8.5106 13.4289L4.21399 9.0736L0 13.3452L4.29661 17.7005L8.53812 22L12.7521 17.7284L26 4.29952L21.7585 0Z" fill="black" />
                                        </svg>
                                    </button>
                                        <button onClick={closeOverlay}> <img alt="cancel" src={cancel} /> </button>
                                </div>
                            </div>
                            : null}
                        <div className="text-option-wrapper">
                        {editOpen && activePost === post.id
                            ? <textarea className="text" maxLength="500" value={postText} className="edit" onChange={(e) => setPostText(e.target.value)} />
                            : <Link className="discussion-content" to={{
                                pathname: `${match.url}/post/${post.id}`, state: {
                                    postText: post.postText,
                                    Date: post.date,
                                    userName: post.userName,
                                    /*numberOfPosts: post.numberOfPosts,*/
                                    Email: post.email
                                }
                            }}>
                                <p className="text">{post.postText}</p>
                                </Link>
                        }

                        <div className="post-options">
                                    
                            {post.email === myEmail
                                ? <div className="edit-delete-wrapper"> {showDeleteConfirm && activePost === post.id
                                    ?<>
                                    <button onClick={(e) => deletePost(e, post.id)}>
                                        <img alt="done" src={done} />
                                    </button>
                                    </>
                                    :
                                    <button onClick={(e) => openDeletePost(e, post)}>
                                        <img alt="delete" src={trash} />
                                    </button>
                                }
                                    {editOpen && activePost === post.id ?
                                        <button onClick={(e) => confirmEditPost(e, post)}>
                                            <img alt="done" src={done} />
                                        </button>
                                        : <button onClick={(e) => openEditPost(e, post)}>
                                            <img alt="edit" src={edit} />
                                        </button>

                                    }</div>


                                : <>
                                    <button>
                                        <img alt="bookmark" src={bookmark} />
                                    </button>
                                    <button>
                                        <img alt="share" src={share} />
                                    </button>
                                </>

                            }</div>
                            </div>


                        <div className="post-info">
                            <div className="post-info-wrapper">
                                <img className="friend-avatar" />
                                <h4> {post.userName} </h4>
                                <h4 className="createDate">{post.date.slice(0, 16).replace('T', ' ')}</h4>
                            </div>
                            <h4>Comments: {post.numberOfComments}</h4>

                        </div>
                    </li>
                ) : 'oops kan inte nå api'}
            </ul>
            {editOpen
                ? <p>pls finish editing ur post before writing a new one'</p>
                :<div className="form-wrapper">
                    <form>
                        <textarea rows="4" maxLength="500" placeholder="Write something..." value={postText} className="input-text" onChange={e => validateNewPost(e.target.value)} />
                        <div className="wrapper">
                            <p>{charactersLeft}/500</p>
                            <button type="button" className="btn" onClick={(e) => createNewPost(e)}>Post</button>
                        </div>
                    </form>
                 </div>}
        </section>
    </>
)
}

export default Posts


// 192 - 212 
 
// {/*<div className="post-options">*/}
//{/*    {showDeleteConfirm && activePost === discussion.id*/ }
//{/*        ?*/ }
//{/*        <button onClick={(e) => deletePost(e, discussion.id)}>*/ }
//{/*            <img alt="done" src={done} />*/ }
//{/*        </button>*/ }

//{/*        :*/ }
//{/*        <button onClick={(e) => openDeletePost(e, post)}>*/ }
//{/*            <img alt="delete" src={trash} />*/ }
//{/*        </button>*/ }
//{/*    }*/ }
//{/*    {editOpen && activePost === post.id ?*/ }
//{/*        <button onClick={(e) => confirmEditPost(e, post)}>*/ }
//{/*            <img alt="done" src={done} />*/ }
//{/*        </button>*/ }
//{/*        : <button onClick={(e) => openEditPost(e, post)}>*/ }
//{/*            <img alt="edit" src={edit} />*/ }
//{/*        </button>*/ }
//{/*    }*/ }
//{/*</div>*/ }
 
