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
    const [userinfo, setUserInfo] = useState("");
    //============================================

    // ändra/välja tags
    const [pulledTags, setTags] = useState([]);
    const [chosenTag1, setChosenTags1] = useState();
    const [chosenTag2, setChosenTags2] = useState();
    const [chosenTag3, setChosenTags3] = useState();

    // ändra bilder 
    const [chosenImg, setChosenImg] = useState();
    const [markedImage, setMarkedImage] = useState();
     
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
        setUserInfo(profile);
        setChosenTags1(profile.tagOne);
        setChosenTags2(profile.tagTwo);
        setChosenTags3(profile.tagThree);
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

            {userinfo ?
                
            <div className={chosenTab === "Profile" ? "Profile " : "Profile hide"}>
                    <textarea value={userinfo.myUserInfo} type="text" onChange={e => setUserInfo(e.target.value)}/>
                    <form>

                    <select onChange={(e) => setChosenTags1(e.target.value)} >
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                        </select>

                    <select onChange={(e) => setChosenTags2(e.target.value)} >
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                        </select>

                    <select onChange={(e) => setChosenTags3(e.target.value)} >
                        {pulledTags.map(tags =>
                            <option key={tags.ID + tags.tagName} value={tags.ID} >
                                {tags.tagName}
                            </option>
                        )}
                        </select>

                </form>
                    <div className="change-img-container">
                        <img className={markedImage == 1 ? "markedImage" : ""} onClick={() => setChosenImg(1), () => setMarkedImage(1)} src={Img1} />
                        <img className={markedImage == 2 ? "markedImage" : ""} onClick={() => setChosenImg(2), () => setMarkedImage(2)} src={Img2} />
                        <img className={markedImage == 3 ? "markedImage" : ""} onClick={() => setChosenImg(3), () => setMarkedImage(3)} src={Img3} />
                        <img className={markedImage == 4 ? "markedImage" : ""} onClick={() => setChosenImg(4), () => setMarkedImage(4)} src={Img4} />
                        <img className={markedImage == 5 ? "markedImage" : ""} onClick={() => setChosenImg(5), () => setMarkedImage(5)} src={Img5} />
                    </div>

                    <button type="button" onClick={(e) => handleClick(e)}>change</button>
            </div>

                :"nothing to see"}


            <div className={chosenTab === "Safty" ? "Safety " : "Safty hide"}>

            </div>

            <div className={chosenTab === "Notifications" ? "Notifications " : "Notifications hide"}>

            </div>

        </section>
    )
}

export default Settings