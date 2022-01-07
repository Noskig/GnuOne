import "./deleteTopic.css"



const DeleteTopic = (props) => {

    const url = 'https://localhost:5001/api/discussions'

    function fetchData() {
        props.fetchData()
    }

    const close = () => {
        props.close()
    }

    console.log(props.topicid)


    async function deleteTopic(e, id) {
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
        <div className="newTopicWrapper" >
            <section className="makeNewTopic">
                
                <button className="close" onClick={close}>✖️</button>
                <h4>Are you sure you want to delete this topic with all its content</h4>
                <div>
                    <button className="button-close" onClick={close}> Cancel</button>
                    <button className="button-delete" onClick={(e) => deleteTopic(e, props.topicid)}>Confirm</button>
                </div>
            </section>
        </div>
    )
}

export default DeleteTopic