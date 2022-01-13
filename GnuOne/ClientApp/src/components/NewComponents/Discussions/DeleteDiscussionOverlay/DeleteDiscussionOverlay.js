import "./deleteDiscussionOverlay.css"
import PortContext from '../../../../contexts/portContext';
import { useContext } from 'react'


const DeleteDiscussionOverlay = (props) => {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/discussions`

    function fetchData() {
        props.fetchData()
    }

    const close = () => {
        props.close()
    }

    console.log(props.discussionId)


    async function deleteDiscussion(e, id) {
        e.preventDefault()
        console.log(id)
        await fetch(url + `/${id}`, {
            method: 'DELETE',
            body: { id: id },
            headers: {
                "Content-type": "application/json",
            }
        });
        fetchData()

    }

    console.log(props)

    return (
        <div className="delete-overlay-background" >
            <section className="delete-overlay-section">

                <button className="close button" onClick={close}>✖️</button>
                <p className="conferm-text">Are you sure you want to delete this topic with all its content?</p>
                <div className="button-container">
                    <button className="button-close button" onClick={close}> Cancel</button>
                    <button className="button-delete button" onClick={(e) => deleteDiscussion(e, props.discussionId)}>Confirm</button>
                </div>
            </section>
        </div>
    )
}

export default DeleteDiscussionOverlay