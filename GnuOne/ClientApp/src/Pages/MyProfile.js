import React, { useState, useEffect, useContext } from 'react';
import { Link, Switch } from 'react-router-dom';
import MeContext from '../contexts/meContext'
import PortContext from '../contexts/portContext'
import ProfileWheel from '../components/NewComponents/ProfileWheel';
import "./MyProfile.min.css";

import Navbar from '../components/NewComponents/Navbar/NavBar';


//testar
function MyProfile({ routes }) {
    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/settings/`
    const [myEmail, setMyEmail] = useState('')

    useEffect(() => {
        fetchData()
    }, [])

    async function fetchData() {
        console.log('fetching')
        const response = await fetch(url)
        const me = await response.json()
        const myEmail = me.email
        console.log(myEmail)
        setMyEmail(myEmail);


    }
    return (
        <MeContext.Provider value={myEmail}>
            <main className="main">
            <Navbar />
            <ProfileWheel routes={routes}/>
            </main>
        </MeContext.Provider>
    )
}

export default MyProfile;