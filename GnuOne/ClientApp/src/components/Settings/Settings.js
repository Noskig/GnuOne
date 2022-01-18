import React, { useState, useEffect, useContext } from 'react';
import PortContext from '../../contexts/portContext';
import "./settings.css";


import Img1 from "../../Image/BeerGuy.jpg";
import Img2 from "../../Image/Flanders.png";
import Img3 from "../../Image/Nelson.jpg";
import Img4 from "../../Image/Ralph.jpg";
import Img5 from "../../Image/SideShow-Bob.jpg";

const Settings = () => {
    //changing user info 
    const [chosenTab, setChosenTab] = useState();
    const [userName, setUsername] = useState('');
    const [userinfo, setUserInfo] = useState();
    //============================================

    // ändra/välja bilder 
    const [pulledTags, setTags] = useState([]);
    const [chosentag1, setchosenTags1] = useState();
    const [chosentag2, setchosenTags2] = useState();
    const [chosentag3, setchosenTags3] = useState();

     
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
        setUserInfo(profile.myUserInfo);
    }

    function handleClick(e) {
        e.preventDefault()
        let newUserName = {
            myUserInfo: userName,
            pictureID: 1,
            tagOne: Number(chosentag1),
            tagTwo: Number(chosentag2),
            tagThree: Number(chosentag3),
        }
        if (userName === "") {
            alert("Fill the missing fields please")
        }
        else {
            addNewUserName(newUserName)
        }
        console.log(userinfo);
    }

    async function addNewUserName(newUserName) {
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
                <textarea type="text" value={userinfo} onChange={e => setUsername(e.target.value)}></textarea>

                <form>
                    <select onChange={(e) => setchosenTags1(e.target.value)} >
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>
                    <select onChange={(e) => setchosenTags2(e.target.value)} >
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>
                    <select onChange={(e) => setchosenTags3(e.target.value)} >
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                    </select>
                    <button type="button" onClick={(e) => handleClick(e)}>change</button>
                </form>
                <div className="change-img-container">
                    <img src={Img1} />
                    <img src={Img2} />
                    <img src={Img3} />
                    <img src={Img4} />
                    <img src={Img5} />
                </div>
            </div>
            <div className={chosenTab === "Safty" ? "Safety " : "Safty hide"}>
            </div>
            <div className={chosenTab === "Notifications" ? "Notifications " : "Notifications hide"}>

            </div>
        </section>
    )
}

export default Settings