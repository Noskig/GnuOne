import React, { useContext } from 'react';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import PortContext from '../contexts/portContext';
import './MyProfile.css'


//testar
function MyProfile() {

    const port = useContext(PortContext)
    const url = `https://localhost:${port}/api/discussions`

    console.log(port)




    return (
        <section className="profile-wheel-container">
            <div className="profile-wheel-wrapper">
                <ul className='profile-wheel'>
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
                    <div className="profile-wheel-lines-container">
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                    </div>
                </ul>
            </div>
        </section>
    )
}

export default MyProfile;
