import { useEffect, useState } from "react"
import "../styles/comments.css";



function Comments(props) {
  const [commentsList, setCommentsList] = useState([]);
  const id = props.id
  const [username, setUsername] = useState('')
  const [messageText, setMessageText] = useState('')
    const baseUrl = 'https://localhost:7261/'
  const [editActive, setEditActive] = useState(false)
  const [commentActive, setCommentActive] = useState('')
  const [deletecommentActive, setDeleteCommentActive] = useState('')
  const [deleteActive, setDeleteActive] = useState(false)
  const [showForm, setShowForm] = useState(false)
  const [charactersLeft, setCharactersLeft] = useState(0)
  const [disabled, setDisabled] = useState(false)

  useEffect(() => {
    getComments(id)
  }, [id])

  async function getComments(id) {
    const response = await fetch(baseUrl + 'api/comments/' + id)
    const data = await response.json()
    console.log(data)
    setCommentsList(data)
  }
  
  function maxCharacters(messageText) {
    if (messageText.length <= 500) {
      setMessageText(messageText)
      setCharactersLeft(messageText.length)
    }
  }

    function handleClick(e) {
      e.preventDefault()
      let message = {
        user: username,
        comment_text: messageText,
        postid: id
      }
      console.log(id)
      addNewComment(message)
      setMessageText("");
      setUsername("");
      setCharactersLeft(0)
    }

    function cancel() {
      setShowForm(false)
      setMessageText("");
      setUsername("");
    }

    function handleEdit(e, comment) {
      e.preventDefault()
      setCommentActive(comment.commentid)
      setEditActive(true)
      setMessageText(comment.comment_text)
      setShowForm(false)
      setDisabled(true)
    }

    async function SendEdit(e, changedComment) {
      e.preventDefault()

      if (changedComment.comment_text !== messageText) {
        changedComment.comment_text = messageText
        console.log(changedComment)
        await fetch(baseUrl + 'api/comments/' + changedComment.commentid, {
          method: 'PUT',
          body: JSON.stringify(changedComment),
          headers: {
            "Content-type": "application/json",
          }
        })
      }
      setCharactersLeft(0)
      setMessageText('')
      setEditActive(false)
      setDisabled(false)
    }



    async function addNewComment(message) {
      console.log(message)
      await fetch(baseUrl + 'api/comments', {
        method: 'POST',
        body: JSON.stringify(message),
        headers: {
          "Content-type": "application/json",
        }
      })
      getComments(id);
    }

    async function deleteComment(e, Cid) {
      e.preventDefault()
      await fetch(baseUrl + 'api/comments/' + Cid, {
        method: 'DELETE',
        body: { id: Cid },
        headers: {
          "Content-type": "application/json",
        }
      })
      getComments(id)
    }

    function setActiveDelete(e, comment) {
      e.preventDefault()
      setDeleteActive(true)
      setDeleteCommentActive(comment.commentid)
    }

    function cancelDelete(e, comment) {
      e.preventDefault()
      setDeleteActive(false)
      setCommentActive(comment.commentid)
    }

    return (

      <section className="comments">
        <section className="existing-comments">
          {commentsList.length>0 ? commentsList.map(comment =>

            <article key={comment.commentid}><div className="border-test">
              <div>
                <div className="border-node"></div>
                <div className="content">

                    <div className="user-info">
                      <figure>
                        <img />
                      </figure>
                      <h5 className="user-name">{comment.user}</h5>
                    </div>

                    <h4>{comment.date.slice(0, 19).replace('T', ' ').slice(0, 16)}</h4>

                  {editActive && commentActive === comment.commentid
                    ? <textarea maxlength="500" value={messageText} className="edit" onChange={e=> maxCharacters(e.target.value)} />
                    : <p maxlength="500">{comment.comment_text}</p>}
                  <div className="footer text-end">
                    {editActive && commentActive === comment.commentid
                      ? <button type="button" onClick={(e) => SendEdit(e, comment)}>Done</button>
                      : <><button type="button" onClick={(e) => handleEdit(e, comment)}>Edit</button>
                        {deleteActive && deletecommentActive === comment.commentid
                          ?  <> <button onClick={(e) => deleteComment(e, comment.commentid)}>Confirm delete</button>
                          <button onClick={(e) => cancelDelete(e, comment)}>Cancel</button> </>
                          : <button type="button" onClick={(e) => setActiveDelete(e, comment)}>Delete</button>
                         
                        
                        }</>}
                  </div>
                </div>
              </div>
            </div>
            </article>
          ) : 'no comments for this post'}
        </section>

        {showForm
        ? <form className={!showForm ? "hide" : "newComment-form"}>
       

          <input maxlength="500" placeholder="Username" value={username} type="text" id="userName" onChange={e => setUsername(e.target.value)} />
            <textarea maxlength="500" placeholder="Write your comment..." value={messageText} id="commentArea" rows="2" onChange={e=> maxCharacters(e.target.value)}></textarea>
            <p>{charactersLeft}/500</p>
          <div className="new-comment-btns">
          <button type="button"  onClick={(e) => handleClick(e)}>Send message</button>
          <button type="button"  onClick={() => cancel()}>Cancel</button>
          </div>
        </form>
      :<form className="newComment-form">
      <input placeholder="Reply..."  type="text" disabled={disabled} onClick={(e) => setShowForm(true)} />
      </form> }
        

        
      </section>
    )
  }
  export default Comments
