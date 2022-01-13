import { useState, useContext, useEffect } from "react"
import PortContext from '../../contexts/portContext';
import done from '../../icons/share.svg'
import edit from '../../icons/trash.svg'

//A WHOLE MESS GÖR KLART NÄR BACKEND HAR FIXAT

const Bio = () => {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/myprofile`
    const [showTextArea, setShowTextArea] = useState()
    const [activeTextArea, setActiveTextArea] = useState()
    const [profile, setProfile] = useState()
    const [myUserInfo, setMyUserInfo] = useState('')
    const [myProfilePic, setMyProfilePic] = useState('')
    const [interests, setInterests] = useState([])
    const [interestOne, setInterestOne] = useState('')
    const [interestOTwo, setInterestTwo] = useState('')
    const [interestThree, setInterestThree] = useState('')

    console.log(myUserInfo)

    useEffect(() => {
        fetchData()
    }, [])

    async function fetchData() {
        const response = await fetch(url)
        const profile = await response.json()
        console.log(profile)
        setProfile(profile[0])
        setInterestOne(profile.tagOne)
        setInterestTwo(profile.tagTwo)
        setInterestTwo(profile.tagThree)
        setInterests([profile.tagOne, profile.tagTwo, profile.tagThree])
    }

    function handleClick(e, info) {
        e.preventDefault()
        let updatedProfile = {
            myUserInfo: myUserInfo,
            pictureID: myProfilePic
        }
        updateInfo(updatedProfile)
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
        fetchData()
        setShowTextArea(false)
    }
    function openEditArea(e, area, profile) {
        e.preventDefault()
        setMyUserInfo(profile.myUserInfo)
        setMyProfilePic(profile.pictureID)
        setActiveTextArea(area)
        setShowTextArea(true)
        
    }



    let testArray = ['intresseOne', 'intejag', 'intresseTwo', 'Email'];

    let newArray = testArray.filter(item => { if (item.includes('intresse')) { return item } } )

    console.log(newArray)

    return (

        <section className="bio-container">
            {profile ?
                <>
                    <div>
                        <h3>about me</h3>
                        { showTextArea && activeTextArea === 'myUserInfo'
                            ? <button><img onClick={handleClick} src={done} /></button>
                            : <img onClick={(e) => openEditArea(e, 'myUserInfo', profile)} src={edit} />}
                    </div>
                    { showTextArea &&  activeTextArea === 'myUserInfo'
                        ? <textarea rows="3" value={myUserInfo} onChange={(e) => setMyUserInfo(e.target.value)}></textarea>
                        : <p>{profile.myUserInfo}</p>}

                    <div>
                        <h3>look at me</h3>
                        <button><img onClick={(e) => openEditArea(e, 'pic', profile)} src={edit} /></button>
                        <img alt="me" src="" />
                    </div>

                    <div>
                        <h3>interests</h3>
                        <button><img onClick={(e) => openEditArea(e, 'interests', profile)} src={edit} /></button>
                       
                    </div>
                    {
                        showTextArea && activeTextArea === 'interests' ?
                        <form>
                            <input type="checkbox" id="interestOne" name="interestOne" value="cats" />
                            <label for="interestOne"> I like cats</label>
                            <input type="checkbox" id="interestTwo" name="interestTwo" value="music" />
                            <label for="interestTwo"> I like music</label>
                            <input type="checkbox" id="interestThree" name="interestThree" value="politics" />
                            <label for="interestThree"> I like politics</label>
                            <input type="submit" value="Submit" />
                            </form>
                            : <div>hej</div>
                    }
                   
                </>
                : 'nothing to see here'}



        </section>

    )
}

export default Bio