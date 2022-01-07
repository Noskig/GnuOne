import { useState, useContext, useEffect } from "react"
import PortContext from '../../contexts/portContext';
import done from '../../icons/share.svg'
import edit from '../../icons/trash.svg'

//A WHOLE MESS GÖR KLART NÄR BACKEND HAR FIXAT

const Bio = () => {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myfriends`
    const [showTextArea, setShowTextArea] = useState()
    const [activeTextArea, setActiveTextArea] = useState()
    let testInfo = {
        about: "hej jag heter johanna och bor i gamlestaden",
        more: "jag fyller år om 20 dagar"
    }
    const [userInfo, setUserInfo] = useState(testInfo)



    //useEffect(() => {
    //    fetchData()
    //}, [])

    //async function fetchData() {
    //    const response = await fetch(url)
    //    const userInfo = await response.json()
    //    console.log(userInfo)
    //    setUserInfo(userInfo)
    //}

    function handleClick(e, info) {
        e.preventDefault()
        let updatedInfo = {
            about: info.about,
            more: info.more
        }
        updateInfo(updatedInfo)
    }

    async function updateInfo(updatedInfo) {
        console.log(updatedInfo)
        await fetch(url, {
            method: 'PUT',
            body: JSON.stringify(updatedInfo),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        /*fetchData();*/
    }
    function openEditArea(area) {
        setActiveTextArea(area)
        setShowTextArea(area)
    }
    let testArray = ['intresseOne', 'intejag', 'intresseTwo', 'Email'];

    let newArray = testArray.filter(item => { if (item.includes('intresse')) { return item } } )

    console.log(newArray)

    return (

        <section className="bio-container">
            {userInfo ?
                <>
                    <div>
                        <h3>about me</h3>
                        {activeTextArea && showTextArea === userInfo.about ? <img onClick={handleClick} src={done} />
                            : <img onClick={() => openEditArea(userInfo.about)} src={edit} />}
                    </div>
                    {activeTextArea && showTextArea === userInfo.about ? <textarea rows="3"></textarea>
                        : <p>{userInfo.about}</p>}

                    <div>
                        <h3>more info</h3>
                        <img onClick={() => openEditArea(userInfo.more)} src={edit} />
                    </div>
                    {
                        activeTextArea && showTextArea === userInfo.more ?
                        <form>
                            <input type="checkbox" id="interestOne" name="interestOne" value="cats" />
                            <label for="interestOne"> I like cats</label>
                            <input type="checkbox" id="interestTwo" name="interestTwo" value="music" />
                            <label for="interestTwo"> I like music</label>
                            <input type="checkbox" id="interestThree" name="interestThree" value="politics" />
                            <label for="interestThree"> I like politics</label>
                            <input type="submit" value="Submit" />
                            </form>
                            : <p>{newArray.map(interest => <p>{interest}</p>)}</p>
                    }
                   
                </>
                : 'nothing to see here'}



        </section>

    )
}

export default Bio