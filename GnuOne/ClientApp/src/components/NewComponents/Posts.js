import PortContext from '../../contexts/portContext';
import { useParams, useLocation } from 'react-router-dom';
import { useEffect, useState, useContext } from 'react';
import trash from '../../icons/trash.svg'
import done from '../../icons/done.svg'
import edit from '../../icons/edit.svg'
import Search from './Search/Search'


const Posts = () => {
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
                <div className="posts-list">
                    {posts ? filteredPosts.map(post =>
                        <div className="post" key={post.id + post.userName}>

                            {editOpen && activePost === post.id
                                ? <textarea maxLength="500" value={postText} className="edit" onChange={(e) => setPostText(e.target.value)} />
                                : <p className="text">{post.postText}</p>
                            }

                            <div className="post-options">

                                {showDeleteConfirm && activePost === post.id
                                    ?
                                    <button onClick={(e) => deletePost(e, post.id)}>
                                        <img alt="done" src={done} />
                                    </button>

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

                                }



                            </div>


                            <div className="post-info">
                                <h4>{post.numberOfPosts} posts on this topic</h4>
                                <h4 className="createDate">{post.date.slice(0, 19).replace('T', ' ').slice(0, 16)}</h4>

                            </div>
                        </div>
                    ) : 'oops kan inte nå api'}
                </div>
                {editOpen
                    ? <p>pls finish editing ur post before writing a new one'</p>
                    : <form>
                        <textarea rows="4" maxLength="500" placeholder="Write something..." value={postText} className="input-text" onChange={e => validateNewPost(e.target.value)} />
                        <p>{charactersLeft}/500</p>
                        <button type="button" className="btn" onClick={(e) => createNewPost(e)}>Post</button>
                    </form>}
            </section>
        </>
    )
}

export default Posts