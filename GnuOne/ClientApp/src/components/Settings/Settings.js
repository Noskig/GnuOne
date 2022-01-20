import React, { useState, useEffect, useContext } from 'react';
import PortContext from '../../contexts/portContext';
import "./settings.css";


import Img1 from "../../Image/1.jpg";
import Img2 from "../../Image/2.png";
import Img3 from "../../Image/3.jpg";
import Img4 from "../../Image/4.jpg";
import Img5 from "../../Image/5.jpg";

const Settings = () => {
    //changing user info 
    const [chosenTab, setChosenTab] = useState();
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
        fetchData();
        fetchTags();
    },[])

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
        setUserInfo(profile[0]);
        setChosenTags1(profile[0].tagOne);
        setChosenTags2(profile[0].tagTwo);
        setChosenTags3(profile[0].tagThree);
    }

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
            addNewUserName(newUserName)
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

    //changing/fetching tags from api tags 

    async function fetchTags() {
        const response = await fetch(`https://localhost:${port}/api/tags`)
        const tags = await response.json()
        console.log(tags);
        setTags(tags);
    }

    //=================================================================

    return (

        <section className="settings-container">

            <h3>Settings</h3>
            <div className="navbar-setting-container">
                <button onClick={() => setChosenTab("Account")}><p>Account</p></button>
                <button onClick={() => setChosenTab("Profile")}><p>Profile</p></button>
                <button onClick={() => setChosenTab("Safety")}><p>Safty and privacy</p></button>
                <button onClick={() => setChosenTab("Notifications")}><p>Notifications</p></button>
            </div>
            <div className={chosenTab === "Account" ? "Account " : "Account hide"}>

            </div>
                
            <div className={chosenTab === "Profile" ? "Profile " : "Profile hide"}>
                    <textarea value={userinfo.myUserInfo} type="text" onChange={e => setUserInfo(e.target.value)}/>
                    <form>

                    <select onChange={(e) => setChosenTags1(e.target.value)} >
                            <option>
                                {userinfo.firstTag}
                            </option>
                            {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                        </select>

                        <select onChange={(e) => setChosenTags2(e.target.value)} >
                            <option>
                                {userinfo.secondTag}
                            </option>
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                        </select>

                        <select onChange={(e) => setChosenTags3(e.target.value)} >
                            <option>
                                {userinfo.thirdTag}
                            </option>
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                        </select>

                    </form>

                    <div className="change-img-container">
                        <img className={chosenImg == 1 ? "markedImage" : ""} onClick={() => setChosenImg(1)} src={Img1} />
                        <img className={chosenImg == 2 ? "markedImage" : ""} onClick={() => setChosenImg(2)} src={Img2} />
                        <img className={chosenImg == 3 ? "markedImage" : ""} onClick={() => setChosenImg(3)} src={Img3} />
                        <img className={chosenImg == 4 ? "markedImage" : ""} onClick={() => setChosenImg(4)} src={Img4} />
                        <img className={chosenImg == 5 ? "markedImage" : ""} onClick={() => setChosenImg(5)} src={Img5} />
                    </div>

                    <button type="button" onClick={(e) => handleClick(e)}>change</button>
            </div>



            <div className={chosenTab === "Safty" ? "Safety " : "Safty hide"}>

            </div>

            <div className={chosenTab === "Notifications" ? "Notifications " : "Notifications hide"}>

            </div>

        </section>
    )
}

export default Settings