import React, { useState, useEffect, useContext } from 'react';
import PortContext from '../../contexts/portContext';

const Settings = () => {
    const [chosenTab, setChosenTab] = useState();
    const [userName, setUsername] = useState('');
    const [userinfo, setUserInfo] = useState();


    // för när jag gör bilder 
    // const [picture, setPic] = useState('')
     
    const port = useContext(PortContext);

    const url = `https://localhost:${port}/api/myprofile`;

    useEffect(() => {
        fetchData()
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
                <textarea type="text" value={userinfo} onChange={e => setUsername(e.target.value)}></textarea>
                <button type="button" onClick={(e) => handleClick(e)}>change</button>
            </div>
            <div className={chosenTab === "Profile" ? "Profile " : "Profile hide"}>

            </div>
            <div className={chosenTab === "Safty" ? "Safety " : "Safty hide"}>

            </div>
            <div className={chosenTab === "Notifications" ? "Notifications " : "Notifications hide"}>

            </div>
        </section>
    )
}

export default Settings