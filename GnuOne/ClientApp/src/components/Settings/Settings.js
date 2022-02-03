import React, { useState, useEffect, useContext } from 'react';
import ProfilePicContext from '../../contexts/profilePicContext';
import PortContext from '../../contexts/portContext';
import ThemeContext from '../../contexts/themeContext';

import "./settings.css";


import Img1 from "../../Image/1.jpg";
import Img2 from "../../Image/2.png";
import Img3 from "../../Image/3.jpg";
import Img4 from "../../Image/4.jpg";
import Img5 from "../../Image/5.jpg";

const Settings = () => {
    const { setProfilePic } = useContext(ProfilePicContext);
    const { darkMode, setDarkMode } = useContext(ThemeContext)
    const [DM, setDM] = useState()

    //changing user info 
    const [profile, setProfile] = useState({});
    const [userinfo, setUserInfo] = useState("");
    //============================================

    // ändra/välja tags
    const [pulledTags, setTags] = useState([]);
    const [chosenTag1, setChosenTags1] = useState();
    const [chosenTag2, setChosenTags2] = useState();
    const [chosenTag3, setChosenTags3] = useState();

    // ändra bilder 
    const [chosenImg, setChosenImg] = useState();

    const port = useContext(PortContext);

    const url = `https://localhost:${port}/api/myprofile`;

    useEffect(() => {
        async function fetchData() {
            const response = await fetch(url)
            const profile = await response.json()
            console.log(profile);

            const responseTwo = await fetch('https://localhost:7261/api/tags');
            const tags = await responseTwo.json()
            console.log(tags)

            let tag1 = tags.filter(tag => tag.ID === profile[0].tagOne)
            let tag2 = tags.filter(tag => tag.ID === profile[0].tagTwo)
            let tag3 = tags.filter(tag => tag.ID === profile[0].tagThree)
            console.log(tag1[0], tag2[0], tag3[0]);
            profile[0].firstTag = tag1[0] ? tag1[0].tagName : null
            profile[0].secondTag = tag2[0] ? tag2[0].tagName : null
            profile[0].thirdTag = tag3[0] ? tag3[0].tagName : null
            setProfile(profile[0])
            setUserInfo(profile[0].myUserInfo);
            setChosenTags1(profile[0].tagOne);
            setChosenTags2(profile[0].tagTwo);
            setChosenTags3(profile[0].tagThree);
            setChosenImg(profile[0].pictureID)

            const responseThree = await fetch('https://localhost:7261/api/settings');
            const settings = await responseThree.json()
            console.log(settings)
            setDM(settings.darkMode)

          
        }

        //changing/fetching tags from api tags 

        async function fetchTags() {
            const response = await fetch(`https://localhost:${port}/api/tags`)
            const tags = await response.json()
            console.log(tags);
            setTags(tags);
        }

        //=================================================================

        fetchData();
        fetchTags();

    }, [url, setDM, setChosenTags1, setChosenTags2, setChosenTags3, setChosenImg, port])



    function handleClick(e) {
        e.preventDefault()
        let newUserName = {
            myUserInfo: userinfo,
            pictureID: Number(chosenImg),
            tagOne: Number(chosenTag1),
            tagTwo: Number(chosenTag2),
            tagThree: Number(chosenTag3),
        }

        if (userinfo === "") {
            alert("Fill the missing fields please")
        }
        else {
            console.log(newUserName)
            addNewUserName(newUserName)
            setProfilePic(chosenImg)
            changeTheme(DM)
        }
    }

    async function addNewUserName(newUserName) {
        console.log(newUserName)
        await fetch(url, {

            method: 'PUT',
            body: JSON.stringify(newUserName),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
    }
    //=================================================================


    async function changeTheme(theme) {
        console.log(theme)
        await fetch(`https://localhost:${port}/api/settings/${theme}`, {
            method: 'PUT',
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        if (theme) {
            //document.documentElement.style.setProperty('--darker-turquoise', 'red');
            document.documentElement.style.setProperty('--background-color', '#171717')
            document.documentElement.style.setProperty('--text-color', 'white')
            document.documentElement.style.setProperty('--textarea-color', 'black')
            document.documentElement.style.setProperty('--textarea-text-color', 'white')
            document.documentElement.style.setProperty('--private-messages-blubb', '#171717')


        } else {
            document.documentElement.style.setProperty('--darker-turquoise', '')
            document.documentElement.style.setProperty('--background-color', '')
            document.documentElement.style.setProperty('--text-color', '')
            document.documentElement.style.setProperty('--textarea-color', '')
            document.documentElement.style.setProperty('--private-messages-blubb', '')

        }

        setDarkMode(theme)
    }

   


    return (

        <section className="settings-container">
            <h1>Settings</h1>
            <div className="bio">
                <h2> Something about you </h2>
                <textarea value={userinfo} type="text" onChange={e => setUserInfo(e.target.value)} />
            </div>
                <form>
                <h2>Interests</h2>
                <div className="select-wrapper">
                    <select onChange={(e) => setChosenTags1(e.target.value)}>
                        <option>
                            {profile.firstTag}
                        </option>
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>

                    <select onChange={(e) => setChosenTags2(e.target.value)} >
                        <option>
                            {profile.secondTag}
                        </option>
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>

                    <select onChange={(e) => setChosenTags3(e.target.value)} >
                        <option>
                            {profile.thirdTag}
                        </option>
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>
                </div>
            </form>

            <div className="change-img-container">
                <h2> Profile picture </h2>
                <div className="img-wrapper">
                    <img alt="img1" className={chosenImg === 1 ? "markedImage" : ""} onClick={() => setChosenImg(1)} src={Img1} />
                    <img alt="img1" className={chosenImg === 2 ? "markedImage" : ""} onClick={() => setChosenImg(2)} src={Img2} />
                    <img alt="img1" className={chosenImg === 3 ? "markedImage" : ""} onClick={() => setChosenImg(3)} src={Img3} />
                    <img alt="img1" className={chosenImg === 4 ? "markedImage" : ""} onClick={() => setChosenImg(4)} src={Img4} />
                    <img alt="img1" className={chosenImg === 5 ? "markedImage" : ""} onClick={() => setChosenImg(5)} src={Img5} />
                </div>
            </div>
            <div className="change-theme">
                <h2> Dark mode </h2>
                <label className="switch">
                    <input type="checkbox" defaultChecked={DM} onClick={() => setDM(!DM)} />
                    <span className="slider"></span>
                </label>
            </div>
            <button className="save-changes" type="button" onClick={(e) => handleClick(e)}>Save changes</button>

        </section>
    )
}

export default Settings